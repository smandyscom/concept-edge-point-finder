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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        PictureBoxIpl __box;

        Point __current;    //record MouseMove or snapPoint location

        bool __engaged; //engaged line drawing

        Dictionary<PointF, OpenCvSharp.MatType> __mattypeTable = new Dictionary<PointF, OpenCvSharp.MatType>();
        Dictionary<PointF, byte> __grayValueTable = new Dictionary<PointF, byte>();

        OpenCvSharp.Mat __gray = new OpenCvSharp.Mat();
        bool __isLoaded = false;


        Graphics __graphics = null; //current Graphics to drasw

        Line lineEngaged = null;    //the line ready to draw
        List<Layer> layers = new List<Layer>();
        int indexofLayer = 0;

        SnapPoint snapPoint = null;

        public Form1()
        {
            InitializeComponent();
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
            SnapPoint newsnapPoint = findSnapPoint(e.Location);
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

            if (!__engaged)
            {
                lineEngaged = new Line();
                lineEngaged.__start = __current;
                __engaged = true;
            }
            else
            {
                lineEngaged.__end = __current;
                __engaged = false;


                // clear table
                // interpolate start-end , load into table
                // access each point's gray value
                // post handling
                __mattypeTable.Clear();
                __grayValueTable.Clear();

                PointF __vector = lineEngaged.__end - new Size(lineEngaged.__start);

                float __distance = (float)Math.Sqrt(Math.Pow(__vector.X, 2) + Math.Pow(__vector.Y, 2));
                ///turns into unit vector
                __vector.X = __vector.X / __distance;
                __vector.Y = __vector.Y / __distance;

                PointF __accumulation = lineEngaged.__start;

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


                //point out edge point
                Ellipse ellipse = new Ellipse();
                ellipse.__center = __edgeCoordinate;
                layers[indexofLayer].Add(ellipse);
                ellipse.draw(__graphics);

                layers[indexofLayer].Add(lineEngaged);
                lineEngaged.draw(__graphics);

                __control.Invalidate(false);
            }

            toolStripStatusLabel1.Text = lineEngaged.__start.ToString() + lineEngaged.__end.ToString();
        }

        private void PaintEventHandler(Object sender, PaintEventArgs e)
        {
            if (snapPoint != null)
                snapPoint.draw(e.Graphics);

            if (__engaged)
            {
                lineEngaged.__end = __current;  //attract to snapPoint
                lineEngaged.draw(e.Graphics);
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

            layers.Add(new Layer());

            Bitmap bitmap = new Bitmap(__gray.Width, __gray.Height);
            __box.Image = bitmap;
            __graphics = Graphics.FromImage(bitmap);
            __graphics.DrawImage(__gray.ToBitmap(), new Point(0, 0));


            ///mouse coordinate not match with image coordinate
            __box.MouseMove += MouseMoveHandler;
            __box.MouseClick += MouseClickHandler;
            __box.Paint += PaintEventHandler;
            btnNewLayer.Click += btnNewLayser_Click;
            __isLoaded = true;
        }

        private void btnNewLayser_Click(object sender, EventArgs e)
        {
            layers.Add(new Layer());
            numericUpDown1.Maximum = layers.Count() - 1;
            numericUpDown1.Value = layers.Count - 1;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            indexofLayer = (int)numericUpDown1.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            __graphics.Clear(Color.White);
            layers[indexofLayer].visible = checkBox1.Checked;
            __graphics.DrawImage(__gray.ToBitmap(), new Point(0, 0));
            layers.ForEach(delegate (Layer la)
            {
                if (la.visible)
                    la.DrawAllObject(__graphics);
            });

            __box.Invalidate(false);
        }


        SnapPoint findSnapPoint(Point hit)
        {
            List<SnapPoint> candidate = new List<SnapPoint>();
            foreach (Layer la in layers)
            {
                candidate.AddRange(la.snapPoints.FindAll(p => p.IsNearBy(hit)));
            }
            if (candidate.Count > 0)
                return candidate.OrderBy(p => p.Distance2(hit)).First();

            return null;
        }

    }





    public class SnapPoint
    {
        Idraw owner = null;
        PointF location;
        public PointF Location { get { return location; } }
        public SnapPoint(PointF Location, Idraw Owner)
        {
            location = Location;
            owner = Owner;
        }

        public bool IsNearBy(PointF p)
        {
            double threshold = 10;
            double leftPoint = location.X - threshold;
            double rightPoint = location.X + threshold;
            if (p.X < leftPoint || p.X > rightPoint)
                return false;

            double bottomPoint = location.Y - threshold;
            double topPoint = location.Y + threshold;
            if (p.Y < bottomPoint || p.Y > topPoint)
                return false;

            return true;
        }

        public double Distance2(PointF p)
        {
            return Math.Pow(location.X - p.X, 2) + Math.Pow(location.Y - p.Y, 2);
        }


        public void draw(Graphics g)
        {
            int with = 10;
            g.DrawRectangle(new Pen(Color.Cyan), location.X - with / 2, location.Y - with / 2, 10, 10);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;

            SnapPoint right = (SnapPoint)obj;
            return (location.X == right.location.X) && (location.Y == right.location.Y);
        }

        public override int GetHashCode()
        {
            return (int)Math.Pow(location.X, location.Y);
        }
    }


    public class Layer
    {
        public bool visible = true;
        List<Idraw> drawObjects = new List<Idraw>();
        public List<SnapPoint> snapPoints = new List<SnapPoint>();
        public void Add(Idraw obj)
        {
            drawObjects.Add(obj);
            snapPoints.AddRange(obj.GetSnapPoints());
        }
        public void DrawAllObject(Graphics graphics)
        {
            drawObjects.ForEach(instance => instance.draw(graphics));
        }
    }
}
