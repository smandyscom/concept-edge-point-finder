using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Interface
{
    public interface Idraw
    {
        void draw(Graphics graphics);
        SnapPoint[] GetSnapPoints();
    }
}
