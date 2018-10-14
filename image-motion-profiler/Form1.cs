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


    public partial class Form1 : Form
    {
        PictureBoxIpl __box;



        IDisplay DspInteraction = new DisplayInteraction();

        OpenCvSharp.Mat __gray = new OpenCvSharp.Mat();
        bool __isLoaded = false;


        Graphics __graphics = null; //current Graphics to drasw

  

        /// <summary>
        /// Forehand of fitting
        /// </summary>
        private List<SnapBase> __selectedPoints = new List<SnapBase>();
        bool __isMultiSelection;

        public Form1()
        {
            InitializeComponent();

            int rows = 5;
            int cols = 3;
            OpenCvSharp.Mat __input = OpenCvSharp.Mat.Ones(rows, cols, OpenCvSharp.MatType.CV_32FC1);
            OpenCvSharp.Mat __output = Fitting.Fitting.RightSingularVector(__input);
        }
     
        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (__isLoaded && e.X <= __gray.Cols && e.Y <= __gray.Rows)
                Text = String.Format("{0},{1},{2}",
                    e.X, e.Y,
               __gray.At<byte>(e.X, e.Y));

            DspInteraction.HandleMouseMove(sender, e);
        
        }

        private void MouseClickHandler(object sender, MouseEventArgs e)
        {

            if (e.X > __gray.Cols || e.Y > __gray.Rows || __graphics == null)
                return;

            DspInteraction.HandleMouseClick(sender, e);

                //toolStripStatusLabel1.Text = lineEngaged.__start.ToString() + lineEngaged.__end.ToString();
            }
        


        private void MouseDownHandler(object sender, MouseEventArgs e)
        {

            PictureBoxIpl __control = (PictureBoxIpl)sender;

            DspInteraction.HandleMouseDown(sender, e);

            //!multi selection
            if (__isMultiSelection)
            {
                if (DspInteraction.SelectedObject is SnapBase)
                {
                    __selectedPoints.Add((SnapBase)DspInteraction.SelectedObject);
                }
            }

            textBoxSelectionCounter.Text = __selectedPoints.Count.ToString();
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            DspInteraction.HandleMouseUp(sender, e);
        }
        private void PaintEventHandler(object sender, PaintEventArgs e)
        {
            DspInteraction.HandlePaintEvent(sender, e);
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


            DspInteraction.Graphics = __graphics;
            DspInteraction.Gray = __gray;
            DspInteraction.DoInvalid += delegate { __box.Invalidate(false); };
            DspInteraction.StatusChange += delegate { ClearAndDraw(); };
            

            ///mouse coordinate not match with image coordinate
            __box.MouseMove += MouseMoveHandler;
            __box.MouseClick += MouseClickHandler;
            __box.MouseDown += MouseDownHandler;
            __box.MouseUp += MouseUpHandler;

            __box.Paint += PaintEventHandler;
            btnNewLayer.Click += btnNewLayser_Click;
            btnLine.Click += btnTask_Click;
            btnSelect.Click += btnTask_Click;
            __isLoaded = true;
        }

      
        private void btnNewLayser_Click(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = DspInteraction.DataModel.CreateNewLayer();
            numericUpDown1.Value = numericUpDown1.Maximum;
        }


        private void btnTask_Click(object sender, EventArgs e)
        {

            if (sender == btnLine)
                DspInteraction.Task = TaskType.SearchEdge;
            else if (sender == btnSelect)
                DspInteraction.Task = TaskType.Select;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           DspInteraction.DataModel.IndexofActiveLayer = (int)numericUpDown1.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DspInteraction.DataModel.SetLayerVisible((int)numericUpDown1.Value, checkBox1.Checked);
        }

        private void ClearAndDraw()
        {
            __graphics.Clear(Color.White);
            __graphics.DrawImage(__gray.ToBitmap(), new Point(0, 0));
            DspInteraction.DataModel.DrawAllLayersObjects(__graphics);
            __box.Invalidate(false);
        }
     
        private void fittingFeatureClick(object sender, EventArgs e)
        {
            if (sender == buttonFittingLine)
            {
                DspInteraction.DataModel.FitLine(__selectedPoints, true).draw(__graphics);
            }
            else if (sender == buttonCircle)
            {
                DspInteraction.DataModel.FitCircle(__selectedPoints, true).draw(__graphics);
            }
            else if (sender == buttonSelectionClear)
            {
                __selectedPoints.Clear();
                textBoxSelectionCounter.Text = Convert.ToString(0);
            }
        }

        private void multiSelectHandler(object sender, EventArgs e)
        {
            __isMultiSelection = checkBoxMulti.Checked;
        }
    }

}
