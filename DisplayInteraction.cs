using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using WindowsFormsApp2.DrawObjects;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2
{
    class DisplayInteraction : IDisplay
    {
        public Graphics __graphics;
        protected Model __dataModel = new Model();
        public Model DataModel { get { return __dataModel; } }


        public MouseLocation mouseLocation { get; set; }

        protected TaskType __taskType;
        public TaskType Task { set { __taskType = value; } }

        public event EventHandler DoInvalid;
        public event EventHandler ClearAndDraw;


        protected SnapPoint __snap;
        protected Idraw selectedObject;
        protected bool isDragging = false;

        protected LineEdgePoint lineEngaged;
        protected bool __engaged = false;

        public void DrawAllLayersObjects(Graphics graphics)
        {
            __dataModel.DrawAllLayersObjects(graphics);
        }

        public void HandleMouseClick(object sender, MouseEventArgs e)
        {
            bool isdoInvalid = false;
            if (__taskType == TaskType.Select)
            {
            }
            else if (__taskType == TaskType.SearchEdge)
            {
                isdoInvalid = isDrawingObject();
            }

            if (isdoInvalid)
                DoInvalid(this, EventArgs.Empty);

        }

        private bool isDrawingObject()
        {
            if (!__engaged)
            {
                lineEngaged = new LineEdgePoint();
                lineEngaged.__start.Location = mouseLocation.current;
                lineEngaged.__start.upstream = __snap;
                __engaged = true;
            }
            else
            {
                lineEngaged.__end.Location = mouseLocation.current;
                lineEngaged.__end.upstream = __snap;
                __engaged = false;

                DataModel.ActiveLayer.Add(lineEngaged);
                lineEngaged.draw(__graphics);
                return true;
            }
            return false;
        }


        public void HandleMouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation.down = e.Location;
            if (__taskType == TaskType.Select)
            {
                if (selectedObject != null) selectedObject.isSelected = false;
                selectedObject = DataModel.GetHitObject(e.Location);
                DoInvalid(this, EventArgs.Empty);
            }
        }




        public void HandleMouseMove(object sender, MouseEventArgs e)
        {
            mouseLocation.current = e.Location;

            bool isSnaped = isSnapChanged(e.Location);  //current mouse location maybe changed by snapPoint

            if (__taskType == TaskType.Select)
            {
                isDragging = isDraggingObject(e);
            }

            mouseLocation.last = mouseLocation.current;

            if (isDragging || isSnaped)
                DoInvalid(this, EventArgs.Empty);
        }

        protected bool isSnapChanged(Point e)
        {
            bool repaint = false;
            SnapPoint newsnap = __dataModel.FindSnapPoint(e);
            if (__snap == null && newsnap == null)  // not close to any snapPoint
                repaint = false;
            else if (newsnap != null && !newsnap.Equals(__snap) // approach new snapPoint
                || (newsnap == null && __snap != null)) // away from old  snapPoint need to clear
                repaint = true;

            //update
            if ((__snap = newsnap) != null)
                mouseLocation.current = Point.Round(__snap.Location);

            return repaint;
        }

        private bool isDraggingObject(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && selectedObject != null)
            {
                LineEdgePoint line = selectedObject as LineEdgePoint;
                SnapPoint p = selectedObject as SnapPoint;
                if (selectedObject is LineEdgePoint)
                {

                    Point diff = mouseLocation.current - new Size(mouseLocation.last);
                    line.__start.Location += new Size(diff);
                    line.__end.Location += new Size(diff);
                }
                else if (selectedObject is SnapPoint && (p.Type == PointType.start || p.Type == PointType.end || p.Type == PointType.center) && p.upstream == null)
                {
                    p.Location = mouseLocation.current;
                }
                return true;
            }

            return false;
        }

        public void HandleMouseUp(object sender, MouseEventArgs e)
        {
            Point diff = e.Location - new Size(mouseLocation.down);
            double dis = Math.Abs(diff.X) + Math.Abs(diff.Y);
            if (isDragging && selectedObject != null && dis > 10)
            {
                selectedObject.isSelected = false;
                ClearAndDraw(this, EventArgs.Empty);
            }
            isDragging = false;
        }

        public void UpdateAllObjects(Graphics graphics, OpenCvSharp.Mat gray)
        {
            __dataModel.Rework(graphics, gray);
        }



    }
}
