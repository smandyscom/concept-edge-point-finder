using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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


        public SnapPoint FindSnapPoint(Point hit)
        {
            List<SnapPoint> candidates = new List<SnapPoint>();
            foreach (Layer la in LayerCollection.FindAll(la => la.visible))
            {
                candidates.AddRange(la.snapPoints.FindAll(p => p.IsNearBy(hit)));
            }
            if (candidates.Count > 0)
                return candidates.OrderBy(p => p.Distance2(hit)).First();

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

        public void SetLayerVisible(int index, bool visible)
        {
            LayerCollection[index].visible = visible;
        }

        public void DrawAllLayersObjects(Graphics graphics)
        {
            LayerCollection.ForEach(layer =>
            {
                if (layer.visible)
                    layer.DrawAllObject(graphics);
            });
        }


        public Idraw GetHitObject(Point hit)
        {
            return null;
        }

    }
}
