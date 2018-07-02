using System;
using System.Collections.Generic;
using System.Drawing;
using OpenCvSharp;

namespace WindowsFormsApp2.Interface
{
    public interface Idraw
    {
        void draw(Graphics graphics);
        SnapPoint[] GetSnapPoints();
        bool isSelected { get; set; }
        bool isHitObject(PointF hit);
    }
}
