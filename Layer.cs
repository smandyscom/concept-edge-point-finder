using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WindowsFormsApp2.DrawObjects;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2
{
    public class Layer
    {
        public bool visible { get; set; } = true;
        public List<Idraw> drawObjects = new List<Idraw>();
      
        public void Add(Idraw obj)
        {
            drawObjects.Add(obj);
            
            PointF intersection;
            foreach (Idraw shape in drawObjects)
            {
                if ((intersection = Utils.GetIntersectPoint(obj, shape)) != PointF.Empty)
                    drawObjects.Add(new InterSectPoint(intersection, obj,shape));
            }
        }
        public void DrawAllObject(Graphics graphics)
        {
            drawObjects.ForEach(instance => instance.draw(graphics));
        }

        public Idraw GetHitObject(PointF hit)
        {
            List<SnapBase> candidates = new List<SnapBase>();

            drawObjects.ForEach(obj => candidates.AddRange(obj.GetSnapPoints().FindAll(p => p.isHitObject(hit))));
            if (candidates.Count > 0)
            {
                candidates.OrderBy(p => p.Distance2(hit));
                double dis = candidates.First().Distance2(hit);

                // find the min distance and no upstream snapPoint
                List<SnapBase> min = candidates.FindAll(p => p.Distance2(hit) == dis);
                List<SnapBase> isolate = min.FindAll(p => (p as SnapPoint).upstream == null);
                return min.Union(isolate).First();
            }

            return drawObjects.Find(obj => obj.isHitObject(hit));
        }

    }
}
