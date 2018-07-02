using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using OpenCvSharp.UserInterface;

using WindowsFormsApp2.Interface;
using WindowsFormsApp2.DrawObjects;

using WindowsFormsApp2.Fitting;


namespace WindowsFormsApp2
{
    enum TaskEnum
    {
        line,
        select
    }

    public partial class Form1 : Form
    {
        PictureBoxIpl __box;

        Point __current;    //record MouseMove or snapPoint location

        bool __engaged; //engaged line drawing



        OpenCvSharp.Mat __gray = new OpenCvSharp.Mat();
        bool __isLoaded = false;


        Graphics __graphics = null; //current Graphics to drasw

        Line lineEngaged = null;    //the line ready to draw


        Model dataModel = new Model();


        SnapPoint snapPoint = null;


        TaskEnum taskType = TaskEnum.select;

        public Form1()
        {
            InitializeComponent();

            int rows = 5;
            int cols = 3;
            OpenCvSharp.Mat __input =OpenCvSharp.Mat.Ones(rows,cols,OpenCvSharp.MatType.CV_32FC1);
            OpenCvSharp.Mat __output = Fitting.Fitting.RightSingularVector(__input);
        }

        private void MouseMoveHandler(Object sender, MouseEventArgs e)
        {

            Control __control = (Control)sender;

            if (__isLoaded && e.X <= __gray.Cols && e.Y <= __gray.Rows)
                Text = String.Format("{0},{1},{2}",
                    e.X, e.Y,
               __gray.At<byte>(e.X, e.Y));

            __current = e.Location;

            bool repaint = false;
            SnapPoint newsnapPoint = dataModel.FindSnapPoint(e.Location);
            if (snapPoint == null && newsnapPoint == null)  // not close to any snapPoint
                repaint = false;
            else if (newsnapPoint != null && !newsnapPoint.Equals(snapPoint) // approach new snapPoint
                || (newsnapPoint == null && snapPoint != null)) // away from old  snapPoint need to clear
                repaint = true;
            if ((snapPoint = newsnapPoint) != null)
                __current = Point.Round(snapPoint.Location);




            if (__engaged || repaint)
            {
                __control.Invalidate(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClickHandler(Object sender, MouseEventArgs e)
        {

            if (e.X > __gray.Cols || e.Y > __gray.Rows || __graphics == null)
                return;

            PictureBoxIpl __control = (PictureBoxIpl)sender;

            Idraw selectedObject;

            if (taskType == TaskEnum.select)
            {
                if ((selectedObject = dataModel.GetHitObject(e.Location)) != null)
                    selectedObject.draw(__graphics);
                __control.Invalidate(false);
            }
            else if (taskType == TaskEnum.line)
            {
                if (!__engaged)
                {
                    lineEngaged = new Line();
                    lineEngaged.__start.Location = __current;
                    __engaged = true;
                }
                else
                {
                    lineEngaged.__end.Location = __current;
                    __engaged = false;


                    //point out edge point


                    dataModel.ActiveLayer.Add(lineEngaged);
                    lineEngaged.draw(__graphics, __gray);

                    __control.Invalidate(false);
                }

                toolStripStatusLabel1.Text = lineEngaged.__start.ToString() + lineEngaged.__end.ToString();
            }
        }

        private void PaintEventHandler(Object sender, PaintEventArgs e)
        {
            if (snapPoint != null)
                snapPoint.draw(e.Graphics);

            if (__engaged)
            {
                lineEngaged.__end.Location = __current;  //attract to snapPoint
                lineEngaged.draw(e.Graphics, __gray);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            __box = new PictureBoxIpl();
            Controls.Add(__box);
            __box.Dock = DockStyle.Fill;

            OpenCvSharp.Mat __raw = OpenCvSharp.Cv2.ImRead(@"../../lenna.png");

            OpenCvSharp.Cv2.CvtColor(__raw, __gray, OpenCvSharp.ColorConversionCodes.BGR2GRAY);



            Bitmap bitmap = new Bitmap(__gray.Width, __gray.Height);
            __box.Image = bitmap;
            __graphics = Graphics.FromImage(bitmap);
            __graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            __graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            __graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            __graphics.DrawImage(__gray.ToBitmap(), new Point(0, 0));


            ///mouse coordinate not match with image coordinate
            __box.MouseMove += MouseMoveHandler;
            __box.MouseClick += MouseClickHandler;
            __box.Paint += PaintEventHandler;
            btnNewLayer.Click += btnNewLayser_Click;
            btnLine.Click += btnTask_Click;
            btnSelect.Click += btnTask_Click;
            __isLoaded = true;
        }

        private void btnNewLayser_Click(object sender, EventArgs e)
        {

            numericUpDown1.Maximum = dataModel.CreateNewLayer();
            numericUpDown1.Value = numericUpDown1.Maximum;
        }


        private void btnTask_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btnLine.Name)
                taskType = TaskEnum.line;
            else if (btn.Name == btnSelect.Name)
                taskType = TaskEnum.select;

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            dataModel.IndexofActiveLayer = (int)numericUpDown1.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            __graphics.Clear(Color.White);
            dataModel.SetLayerVisible((int)numericUpDown1.Value, checkBox1.Checked);
            __graphics.DrawImage(__gray.ToBitmap(), new Point(0, 0));
            dataModel.DrawAllLayersObjects(__graphics);

            __box.Invalidate(false);
        }
    }

}
