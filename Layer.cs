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
        public List<SnapPoint> snapPoints = new List<SnapPoint>();
        public void Add(Idraw obj)
        {
            drawObjects.Add(obj);
            snapPoints.AddRange(obj.GetSnapPoints());
            PointF intersection;
            foreach (Idraw shape in drawObjects)
            {
                if ((intersection = Utils.GetIntersectPoint(obj, shape)) != PointF.Empty)
                    snapPoints.Add(new SnapPoint(intersection, obj, PointType.intersection));
            }

        }
        public void DrawAllObject(Graphics graphics)
        {
            drawObjects.ForEach(instance => instance.draw(graphics));
        }

        public Idraw GetHitObject(PointF hit)
        {
            List<SnapPoint> candidates = snapPoints.FindAll(p => p.isHitObject(hit));
            if (candidates.Count > 0)
                return candidates.OrderBy(p => p.Distance2(hit)).First();

            return drawObjects.Find(obj => obj.isHitObject(hit));
        }

    }
}
