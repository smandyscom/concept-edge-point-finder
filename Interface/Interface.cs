using System;
using System.Collections.Generic;
using System.Drawing;
using OpenCvSharp;

namespace WindowsFormsApp2.Interface
{
    public interface Idraw
    {
        void draw(Graphics graphics);
        List<SnapPoint> GetSnapPoints();
        bool isSelected { get; set; }
        bool isHitObject(PointF hit);
        Idraw Update(object data = null);
    }

    public interface ICoeffcient
    {
        Mat Coefficient { get; set; }
    }
}
