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
        protected Graphics __graphics;
        public Graphics Graphics { set { __graphics = value; } }

        protected OpenCvSharp.Mat __gray;
        public OpenCvSharp.Mat Gray { set { __gray = value; } }

        protected Model __dataModel = new Model();
        public Model DataModel { get { return __dataModel; } }

        protected TaskType __taskType;
        public TaskType Task
        {
            set
            {
                __taskType = value;
                __selectedObject = null;
            }
        }

        public Idraw SelectedObject { get { return __selectedObject; } }


        public DisplayInteraction()
        {
            __dataModel.VisibleChanged += delegate { Reorganize(); };
        }
        private MouseLocation mouseLocation = new MouseLocation();


        public event EventHandler DoInvalid;
        public event EventHandler StatusChange;

        protected SnapBase __snap;
        protected Idraw __selectedObject;
        protected bool isDragging = false;

        protected LineEdgePoint lineEngaged;
        protected bool __engaged = false;   //engaged line drawing

        public void DrawAllLayersObjects()
        {
            __dataModel.DrawAllLayersObjects(__graphics);
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
                if (__selectedObject != null) __selectedObject.isSelected = false;
                __selectedObject = DataModel.GetHitObject(e.Location);
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

            if (isDragging || isSnaped || __engaged)
                DoInvalid(this, EventArgs.Empty);
        }

        protected bool isSnapChanged(Point e)
        {
            bool repaint = false;
            SnapBase newsnap = __dataModel.FindSnapPoint(e);
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
            if (e.Button == MouseButtons.Left && __selectedObject != null)
            {
                LineEdgePoint line = __selectedObject as LineEdgePoint;
                SnapBase p = __selectedObject as SnapBase;
                if (__selectedObject is LineEdgePoint)
                {
                    Point diff = mouseLocation.current - new Size(mouseLocation.last);
                    line.__start.Location += new Size(diff);
                    line.__end.Location += new Size(diff);
                }
                else if (__selectedObject is SnapBase && (p.Type == PointType.start || p.Type == PointType.end || p.Type == PointType.center))
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
            if (isDragging && __selectedObject != null && dis > 5)
                Reorganize();

            isDragging = false;
        }



        public void HandlePaintEvent(object sender, PaintEventArgs e)
        {
            if (isDragging)
            {
                __dataModel.UpdateAllObjects( __gray);   
                __dataModel.DrawAllLayersObjects(e.Graphics);   //preview status
            }
            if (__selectedObject != null)
                __selectedObject.draw(e.Graphics);
            if (__snap != null)
                __snap.draw(e.Graphics);
            if (__engaged)
            {
                lineEngaged.__end.Location = mouseLocation.current;  //attract to snapPoint
                lineEngaged.Update(__gray);
                lineEngaged.draw(e.Graphics);
            }
        }

        /// <summary>
        /// Idraw objects status has changed, info graphics create new
        /// </summary>
        private void Reorganize()
        {
            if (__selectedObject != null) __selectedObject.isSelected = false;
            __selectedObject = null;
            StatusChange?.Invoke(this, EventArgs.Empty);
        }

    }
}
