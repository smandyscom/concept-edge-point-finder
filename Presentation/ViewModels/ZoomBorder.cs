using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;

namespace Presentation.ViewModels
{
    public class ZoomBorder : Border
    {

		public double PrimaryX
		{
			get { return (double)GetValue(PrimaryXProperty); }
		}
		// Using a DependencyProperty as the backing store for ZoomX.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PrimaryXProperty =
			DependencyProperty.Register(nameof(PrimaryX), typeof(double), typeof(ZoomBorder), new PropertyMetadata(double.NaN));


		public double PrimaryY
		{
			get { return (double)GetValue(PrimaryYProperty); }
		}
		// Using a DependencyProperty as the backing store for ZoomY.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PrimaryYProperty =
			DependencyProperty.Register(nameof(PrimaryY), typeof(double), typeof(ZoomBorder), new PropertyMetadata(double.NaN));

		private UIElement child = null;
        private Point origin;
        private Point start;

        public TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        public ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }
        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != this.Child)
                    this.Initialize(value);
                base.Child = value;
            }
        }

        public void Initialize(UIElement element)
        {
            this.child = element;
            if (child != null)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);
                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                this.MouseWheel += child_MouseWheel;
                this.MouseLeftButtonDown += child_MouseLeftButtonDown;
                this.MouseLeftButtonUp += child_MouseLeftButtonUp;
                this.MouseMove += child_MouseMove;
                this.PreviewMouseRightButtonDown += new MouseButtonEventHandler(
                  child_PreviewMouseRightButtonDown);
            }
        }

        public void Reset()
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;

                // reset pan
                var tt = GetTranslateTransform(child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        #region Child Events

        private void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null)
            {
                var st = GetScaleTransform(child);
                var tt = GetTranslateTransform(child);

                double zoom = e.Delta > 0 ? .2 : -.2;
                if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                    return;

                Point relative = e.GetPosition(child);
                double abosuluteX;
                double abosuluteY;

                abosuluteX = relative.X * st.ScaleX + tt.X;
                abosuluteY = relative.Y * st.ScaleY + tt.Y;

                st.ScaleX += zoom;
                st.ScaleY += zoom;

                tt.X = abosuluteX - relative.X * st.ScaleX;
                tt.Y = abosuluteY - relative.Y * st.ScaleY;
            }
        }

        private void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                this.Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }

        private void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                this.Cursor = Cursors.Arrow;
            }
        }

        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Reset();
        }

        private void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
				var click = e.GetPosition(this);
				var tt = GetTranslateTransform(child);
				var st = GetScaleTransform(child);

				if (child.IsMouseCaptured)
                {
                  
					
					Vector v = start - click;
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                }


				double zoom1x = (click.X - tt.X) / st.ScaleX;
				double zoom1y = (click.Y - tt.Y) / st.ScaleY;
				SetValue(PrimaryXProperty, zoom1x);
				SetValue(PrimaryYProperty, zoom1y);
			
			}
        }

        #endregion
    }
}