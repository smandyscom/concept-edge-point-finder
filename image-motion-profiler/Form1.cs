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

        /// <summary>
        /// TODO , these three things may be combined as one single object? would be better
        /// </summary>
        PictureBoxIpl mainBox;
        IDisplay mainDisplay = new DisplayInteraction();
        Graphics mainGraphics = null; //current Graphics to drasw
        OpenCvSharp.Mat mainGrayImage = new OpenCvSharp.Mat();

        bool __isLoaded = false;


      
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
            if (__isLoaded && e.X <= mainGrayImage.Cols && e.Y <= mainGrayImage.Rows)
                Text = String.Format("{0},{1},{2}",
                    e.X, e.Y,
               mainGrayImage.At<byte>(e.X, e.Y));

            mainDisplay.HandleMouseMove(sender, e);
        
        }

        private void MouseClickHandler(object sender, MouseEventArgs e)
        {

            if (e.X > mainGrayImage.Cols || e.Y > mainGrayImage.Rows || mainGraphics == null)
                return;

            mainDisplay.HandleMouseClick(sender, e);

                //toolStripStatusLabel1.Text = lineEngaged.__start.ToString() + lineEngaged.__end.ToString();
            }
        


        private void MouseDownHandler(object sender, MouseEventArgs e)
        {

            PictureBoxIpl __control = (PictureBoxIpl)sender;

            mainDisplay.HandleMouseDown(sender, e);

            //!multi selection
            if (__isMultiSelection)
            {
                if (mainDisplay.SelectedObject is SnapBase)
                {
                    __selectedPoints.Add((SnapBase)mainDisplay.SelectedObject);
                }
            }

            textBoxSelectionCounter.Text = __selectedPoints.Count.ToString();
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            mainDisplay.HandleMouseUp(sender, e);
        }
        private void PaintEventHandler(object sender, PaintEventArgs e)
        {
            mainDisplay.HandlePaintEvent(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            mainBox = new PictureBoxIpl();
            Controls.Add(mainBox);
            mainBox.Dock = DockStyle.Fill;

            

           
            mainDisplay.DoInvalid += delegate { mainBox.Invalidate(false); };
            mainDisplay.StatusChange += delegate { ClearAndDraw(); }; //TODO , take as internal handler?
            

            ///mouse coordinate not match with image coordinate
            mainBox.MouseMove += MouseMoveHandler;
            mainBox.MouseClick += MouseClickHandler;
            mainBox.MouseDown += MouseDownHandler;
            mainBox.MouseUp += MouseUpHandler;

            mainBox.Paint += PaintEventHandler;
            btnNewLayer.Click += btnNewLayser_Click;
            btnLine.Click += btnTask_Click;
            btnSelect.Click += btnTask_Click;

            loadPicture(@"../../lenna.png");

            __isLoaded = true;
        }

      
        private void btnNewLayser_Click(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = mainDisplay.DataModel.CreateNewLayer();
            numericUpDown1.Value = numericUpDown1.Maximum;
        }


        private void btnTask_Click(object sender, EventArgs e)
        {

            if (sender == btnLine)
                mainDisplay.Task = TaskType.SearchEdge;
            else if (sender == btnSelect)
                mainDisplay.Task = TaskType.Select;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           mainDisplay.DataModel.IndexofActiveLayer = (int)numericUpDown1.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mainDisplay.DataModel.SetLayerVisible((int)numericUpDown1.Value, checkBox1.Checked);
        }

        private void ClearAndDraw()
        {
            mainGraphics.Clear(Color.White);
            mainGraphics.DrawImage(mainGrayImage.ToBitmap(), new Point(0, 0));
            mainDisplay.DataModel.DrawAllLayersObjects(mainGraphics);
            mainBox.Invalidate(false);
        }
     
        private void fittingFeatureClick(object sender, EventArgs e)
        {
            if (sender == buttonFittingLine)
            {
                mainDisplay.DataModel.FitLine(__selectedPoints, true).draw(mainGraphics);
            }
            else if (sender == buttonCircle)
            {
                mainDisplay.DataModel.FitCircle(__selectedPoints, true).draw(mainGraphics);
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

        /// <summary>
        /// Handling menu action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuActionHandler(object sender, EventArgs e)
        {
            if (sender == openToolStripMenuItem)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    loadPicture(openFileDialog1.FileName);
                }
            }
        }

        private void loadPicture(String filename)
        {
            //@"../../lenna.png"
            OpenCvSharp.Mat m_raw = OpenCvSharp.Cv2.ImRead(filename);
            OpenCvSharp.Cv2.CvtColor(m_raw, mainGrayImage, OpenCvSharp.ColorConversionCodes.BGR2GRAY);

            Bitmap bitmap = new Bitmap(mainGrayImage.Width, mainGrayImage.Height);
            mainBox.Image = bitmap;

            mainGraphics = Graphics.FromImage(bitmap);
            mainGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            mainGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            mainGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

            mainDisplay.Graphics = mainGraphics;
            mainDisplay.Gray = mainGrayImage;
            mainGraphics.DrawImage(mainGrayImage.ToBitmap(), new Point(0, 0));
        }
    }

}
