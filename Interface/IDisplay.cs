using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp2.Interface
{
    interface IDisplay
    {
        TaskType Task { set; }
        Model DataModel { get; }
        MouseLocation mouseLocation { get; set; }
        void HandleMouseDown(object sender, MouseEventArgs e);
        void HandleMouseUp(object sender, MouseEventArgs e);
        void HandleMouseMove(object sender, MouseEventArgs e);
        void HandleMouseClick(object sender, MouseEventArgs e);
        void UpdateAllObjects(Graphics graphics, OpenCvSharp.Mat gray);
        void DrawAllLayersObjects(Graphics graphics);
        event EventHandler DoInvalid;
    }

    enum TaskType
    {
        Select,
        SearchEdge
    }

    class MouseLocation
    {
        public Point last = Point.Empty;
        public Point current = Point.Empty;
        public Point down = Point.Empty;
    }

}
