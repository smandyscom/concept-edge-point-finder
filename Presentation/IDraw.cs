using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public interface IDraw
    {
        //void draw(Graphics graphics);
        //List<PointF> GetSnapPoints();

        bool isSelected { get; set; }
        bool isHitObject(PointF hit);

        IDraw Update(object data = null);
    }
}
