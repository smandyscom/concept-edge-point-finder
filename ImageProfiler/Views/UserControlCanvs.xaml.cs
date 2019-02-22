using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using WindowsFormsApp2.DrawObjects;
using WindowsFormsApp2.Interface;
using WindowsFormsApp2.Extensions;
using Presentation;

namespace WindowsFormsApp2.WPF
{
	/// <summary>
	/// UserControlCanvs.xaml 的互動邏輯
	/// </summary>
	public partial class UserControlCanvs : UserControl
	{
		public Model model = new Model();
		private Lines lines = new Lines();
		private Circles circles = new Circles();

		public UserControlCanvs()
		{

			InitializeComponent();

			model.ActiveLayer.drawObjects.AddRange(lines);
			model.ActiveLayer.drawObjects.AddRange(circles);
			this.DataContext = model.LayerCollection;
			Border.MouseMove += Border_MouseMove;
		}

		private void Border_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//var point = new PointF((float)Border.PrimaryX, (float)Border.PrimaryY);
			//Idraw obj = model.FindSnapPoint(point);
			//if (obj != null) obj.isSelected = true;
		}

		private void Shape_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			//Line obj = (Line)sender;
			//var data = obj.DataContext;
		}
	}

	public class LayerCollection : ObservableCollection<Layer>
	{
		private Layer layer1 = new Layer();
		private Layer layer2 = new Layer();

		private Lines lines = new Lines();
		private Circles circles = new Circles();

		public LayerCollection()
		{
			Add(layer1);
			Add(layer2);

			layer1.drawObjects.AddRange(circles);
			layer2.drawObjects.AddRange(lines);
		}
	}

	public class Lines : ObservableCollection<IDraw>
	{
		//private LineEdgePoint line = new LineEdgePoint();
		public Lines()
		{
			var temp = new PointF(0, 0);
			line.__start.Location = temp;

			temp = new PointF(512, 480);
			line.__end.Location = temp;

			Add(line);
			CreateTimer();
		}
		void CreateTimer()
		{
			var timer1 = new Timer
			{
				Enabled = true,
				Interval = 2000
			};
			timer1.Elapsed += Timer1_Elapsed;
		}

		private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
		{
			var temp = line.__end.Location;
			temp.X -= 10;
			temp.Y -= 10;
			line.__end.Location = temp;
			line.isSelected = !line.isSelected;
		}
	}

	public class Circles : ObservableCollection<IDraw>
	{
		private CircleFitted circle = new CircleFitted();
		public Circles()
		{
			var temp = new PointF(256, 240);
			circle.__radius = 10;
			circle.__center.Location = temp;
			Add(circle);
			CreateTimer();
		}
		void CreateTimer()
		{
			var timer1 = new Timer
			{
				Enabled = true,
				Interval = 2000
			};
			timer1.Elapsed += Timer1_Elapsed;
		}

		private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
		{
			var temp = circle.__center.Location;
			temp.X += 10;
			temp.Y += 10;
			circle.__center.Location = temp;
			circle.__radius += 1;
		}
	}


}
