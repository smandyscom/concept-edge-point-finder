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
        Graphics Graphics { set; }    //current Graphics to drasw
        OpenCvSharp.Mat Gray { set; }
        TaskType Task { set; }
        Model DataModel { get; }
        Idraw SelectedObject { get; }
        void HandleMouseDown(object sender, MouseEventArgs e);
        void HandleMouseUp(object sender, MouseEventArgs e);
        void HandleMouseMove(object sender, MouseEventArgs e);
        void HandleMouseClick(object sender, MouseEventArgs e);
        void DrawAllLayersObjects();
        void HandlePaintEvent(object sender, PaintEventArgs e);
        event EventHandler DoInvalid;
        event EventHandler StatusChange;
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
