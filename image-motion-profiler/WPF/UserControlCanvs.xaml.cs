using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using WindowsFormsApp2.DrawObjects;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2.WPF
{
	/// <summary>
	/// UserControlCanvs.xaml 的互動邏輯
	/// </summary>
	public partial class UserControlCanvs : UserControl
	{
		public UserControlCanvs()
		{

			InitializeComponent();
			this.DataContext = lines;

		}

		public Lines lines { get; private set; } = new Lines();
	}

	public class Lines : ObservableCollection<Idraw>
	{
		private LineEdgePoint line = new LineEdgePoint();
		private CircleFitted circle = new CircleFitted();
		public Lines()
		{
			var temp = new PointF(0, 0);
			line.__start.Location = temp;

			temp = new PointF(512, 480);
			line.__end.Location = temp;
			Add(line);

			temp = new PointF(256, 240);
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
			var temp = line.__end.Location;
			temp.X -= 10;
			temp.Y -= 10;
			line.__end.Location = temp;
			line.isSelected = !line.isSelected;


		}
	}
}
