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


        public SnapPoint FindSnapPoint(Point hit)
        {
            List<SnapPoint> candidates = new List<SnapPoint>();
            foreach (Layer la in LayerCollection.FindAll(la => la.visible))
            {
                candidates.AddRange(la.snapPoints.FindAll(p => p.isHitObject(hit)));
            }
            if (candidates.Count > 0)
            {
                candidates.OrderBy(p => p.Distance2(hit));
                double dis = candidates.First().Distance2(hit);

                // find the min distance and no upstream snapPoint
                List<SnapPoint> min = candidates.FindAll(p => p.Distance2(hit) == dis);
                List<SnapPoint> isolate = min.FindAll(p => p.upstream == null);
                return min.Union(isolate).First();
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


        public void Rework(Graphics graphics , OpenCvSharp.Mat gray)
        {
            //update snapPoint
            foreach (Layer layer in LayerCollection)
            {
                foreach (Idraw obj in layer.drawObjects)
                {
                    foreach (SnapPoint p in obj.GetSnapPoints())
                    {
                        if (p.upstream != null)
                            p.Location = p.upstream.Location;
                    }
                    //temp assume obj is line
                    Type type = obj.GetType();
                    if (type == typeof(LineEdgePoint))
                        (obj as LineEdgePoint).draw(graphics, gray);
                    else if (type ==typeof(LineFitted))
                    {
                        (obj as LineFitted).Fit();
                        (obj as LineFitted).draw(graphics);
                    }

                }
            }
        }


        public Idraw GetHitObject(Point hit)
        {
            foreach (Layer la in LayerCollection)
            {
                Idraw obj = la.GetHitObject(hit);
                if (obj != null)
                    return obj;
            }
            return null;
        }

    }   //Modal
}
