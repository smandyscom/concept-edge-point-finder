﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2
{
    public class Layer
    {
        public bool visible = true;
        List<Idraw> drawObjects = new List<Idraw>();
        public List<SnapPoint> snapPoints = new List<SnapPoint>();
        public void Add(Idraw obj)
        {
            drawObjects.Add(obj);
            snapPoints.AddRange(obj.GetSnapPoints());
            PointF intersection;
            foreach (Idraw shape in drawObjects)
            {
                if ((intersection = Utils.GetIntersectPoint(obj, shape)) != PointF.Empty)
                    snapPoints.Add(new SnapPoint(intersection, obj));
            }

        }
        public void DrawAllObject(Graphics graphics)
        {
            drawObjects.ForEach(instance => instance.draw(graphics));
        }
    }
}
