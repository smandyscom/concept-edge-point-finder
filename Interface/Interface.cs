using System;
using System.Collections.Generic;
using System.Drawing;
using OpenCvSharp;

namespace WindowsFormsApp2.Interface
{
    public interface Idraw
    {
        void draw(Graphics graphics, Mat gray = null);
        SnapPoint[] GetSnapPoints();
    }
}
