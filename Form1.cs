using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using OpenCvSharp.UserInterface;
using System.Drawing.Imaging;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Point __start;
        Point __end;
        Point __current;
        Pen __penBlack = new Pen(Color.Black, 3);
        Pen __penGreen = new Pen(Color.Green, 3);

        bool __engaged; //engaged line drawing

        Dictionary<PointF, OpenCvSharp.MatType> __mattypeTable = new Dictionary<PointF, OpenCvSharp.MatType>();
        Dictionary<PointF, byte> __grayValueTable = new Dictionary<PointF, byte>();

        OpenCvSharp.Mat __gray = new OpenCvSharp.Mat();
        bool __isLoaded = false;

        List<Bitmap> __laysers = new List<Bitmap>();
        Graphics __graphics = null; //current Graphics to drasw

        public Form1()
        {
            InitializeComponent();

            //MouseMove += MouseMoveHandler;
            //MouseClick += MouseClickHandler;
            //Paint += PaintEventHandler;
        }

        private void MouseMoveHandler(Object sender, MouseEventArgs e)
        { 
            
            Control __control = (Control)sender;

            __current.X = e.X;
            __current.Y = e.Y;

            if (__isLoaded)
                Text = String.Format("{0},{1},{2}",
               __current.X,
               __current.Y,
               __gray.At<byte>(e.X, e.Y));

            __end = __current;

            if (__engaged)
                __control.Invalidate(false);
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

            if (!__engaged)
            {
                __start = __current;
                __engaged = true;

                __penBlack = new Pen(Color.Black, 3);
            }
            else
            {
                __end = __current;
                __engaged = false;

                // clear table
                // interpolate start-end , load into table
                // access each point's gray value
                // post handling
                __mattypeTable.Clear();
                __grayValueTable.Clear();

                System.Drawing.PointF __vector = __end - new System.Drawing.Size(__start);

                float __distance = (float)Math.Sqrt(Math.Pow(__vector.X, 2) + Math.Pow(__vector.Y, 2));
                ///turns into unit vector
                __vector.X = __vector.X / __distance;
                __vector.Y = __vector.Y / __distance;

                PointF __accumulation = __start;

                double __acuumulatedLength = 0;
                while (__acuumulatedLength <= __distance)
                {
                    __accumulation += new SizeF(__vector);

                    //establish pair of coordinate , gray value
                    __mattypeTable.Add(__accumulation,
                        __gray.At<OpenCvSharp.MatType>(
                            Convert.ToInt32(__accumulation.Y),
                            Convert.ToInt32(__accumulation.X)
                            )
                            );
                    __grayValueTable.Add(__accumulation,
                        __gray.At<byte>(
                            Convert.ToInt32(__accumulation.Y),
                            Convert.ToInt32(__accumulation.X)
                            )
                            );


                    //unit length added one
                    __acuumulatedLength++;
                }

                //diff and find maximum
                List<byte> __grayValueList = __grayValueTable.Values.ToList();
                List<int> __diffValueList = new List<int>();
                for (int i = 0; i < __grayValueList.Count - 1; i++)
                {
                    __diffValueList.Add(Math.Abs(__grayValueList[i + 1] - __grayValueList[i]));
                }
                int edgeIndex = __diffValueList.IndexOf(__diffValueList.Max());

                PointF __edgeCoordinate = __grayValueTable.Keys.ElementAt(edgeIndex);

                //point out central point
                __graphics.DrawEllipse(__penGreen,
                    __edgeCoordinate.X,
                    __edgeCoordinate.Y,
                    10,
                    10);

                //point out central point
                __graphics.DrawEllipse(__penBlack,
                    __mattypeTable.Keys.ElementAt(__mattypeTable.Count / 2).X,
                    __mattypeTable.Keys.ElementAt(__mattypeTable.Count / 2).Y,
                    2,
                    2);

                __graphics.DrawLine(__penBlack, __start, __end);
                __control.Invalidate(false);
            }

            toolStripStatusLabel1.Text = __start.ToString() + __end.ToString();
        }

        private void PaintEventHandler(Object sender, PaintEventArgs e)
        {
            if (__engaged)
                e.Graphics.DrawLine(__penBlack, __start, __end);
            //sCreateGraphics().DrawLine(__pen, __start, __end);
        }
        PictureBoxIpl __box;
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
            
            __box.Image = __gray.ToBitmap(); //show base image

            ///mouse coordinate not match with image coordinate
            __box.MouseMove += MouseMoveHandler;
            __box.MouseClick += MouseClickHandler;
            __box.Paint += PaintEventHandler;
            btnNewLayer.Click += buttonNewLayser_Click;
            __isLoaded = true;
        }

        private void buttonNewLayser_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(__gray.Width, __gray.Height);

            Graphics.FromImage(bitmap).DrawImage(__gray.ToBitmap(), new Point(0, 0));

            __laysers.Add(bitmap);

            numericUpDown1.Maximum = __laysers.Count;
            numericUpDown1.Value = __laysers.Count;
            numericUpDown1.Minimum = 1;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            changeLayser((int)numericUpDown1.Value);
        }

        void changeLayser(int num)
        {
            //get bitmap of layer and show
            Bitmap bitmap = __laysers[num - 1];

            // update current graphics
            __graphics = Graphics.FromImage(bitmap); 
            __box.Image =bitmap;
        }
    }
}
