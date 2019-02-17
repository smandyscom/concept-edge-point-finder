using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WindowsFormsApp2.DrawObjects;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2
{
    /// <summary>
    /// provide function and information to interact with graphics
    /// </summary>
    public partial class Model
    {

        public List<Layer> LayerCollection { get; set; } = new List<Layer>();

        Layer activeLayer;
        public Layer ActiveLayer { get { return activeLayer; } }    // user is drawing on

        public int IndexofActiveLayer   //0 index
        { get { return LayerCollection.IndexOf(activeLayer); } set { activeLayer = LayerCollection[value]; } }

        public Model()
        {
            LayerCollection.Add(new Layer());
            activeLayer = LayerCollection[0];
        }


        public SnapBase FindSnapPoint(Point hit)
        {
            List<SnapBase> candidates = new List<SnapBase>();
            foreach (Layer la in LayerCollection.FindAll(la => la.Visible))
            {
                SnapBase obj = la.GetHitObject(hit) as SnapBase;
                if (obj != null) candidates.Add(obj);
                if (candidates.Count > 0) return candidates.OrderBy(p => p.Distance2(hit)).First();
            }
            return null;
        }

        public void AddObject(Idraw obj)
        {
            CreateNewLayer();
        }

        public void RemoveObject(Idraw obj)
        {

        }

        /// <summary>
        /// return IndexofActiveLayer
        /// </summary>
        /// <returns></returns>
        public int CreateNewLayer()
        {
            activeLayer = new Layer();
            LayerCollection.Add(activeLayer);
            return IndexofActiveLayer;
        }
        public EventHandler VisibleChanged;
        public void SetLayerVisible(int index, bool visible)
        {
            LayerCollection[index].Visible = visible;
            VisibleChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DrawAllLayersObjects(Graphics graphics)
        {
            LayerCollection.ForEach(layer =>
            {
                if (layer.Visible)
                    layer.DrawAllObject(graphics);
            });
        }

        /// <summary>
        /// re-calculate all Idraw object status
        /// </summary>
        /// <param name="gray"></param>
        public void UpdateAllObjects(OpenCvSharp.Mat gray)
        {
            //update snapPoint
            foreach (Layer layer in LayerCollection)
            {
                foreach (Idraw obj in layer.drawObjects)
                {
                    obj.Update(gray);
                }
            }
        }


        public Idraw GetHitObject(Point hit)
        {
            foreach (Layer la in LayerCollection)
            {
                if (!la.Visible) continue;
                Idraw obj = la.GetHitObject(hit);
                if (obj != null)
                    return obj;
            }
            return null;
        }

        /// <summary>
        /// Improvement , use Factory pattern as generized interface
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public LineFitted FitLine(List<SnapBase> selection, bool add)
        {
            LineFitted line = new LineFitted();
            line.__selectedPoints = selection;
            line.Update();
            if (add) activeLayer.Add(line);

            return line;
        }

        public CircleFitted FitCircle(List<SnapBase> selection, bool add)
        {
            CircleFitted __circle = new CircleFitted();
            __circle.__selectedPoints = selection;
            __circle.Update();
            if (add)
                activeLayer.Add(__circle);

            return __circle;
        }

    }   //Modal
}
