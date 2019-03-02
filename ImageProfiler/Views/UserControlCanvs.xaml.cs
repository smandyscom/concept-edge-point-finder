using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Core.Arch;
using OpenCvSharp;
using Presentation.ViewModels;


namespace ImageProfiler
{
	/// <summary>
	/// UserControlCanvs.xaml 的互動邏輯
	/// </summary>
	public partial class UserControlCanvs : UserControl
	{
		private ElementBaseCollection lines = new ElementBaseCollection();
		//private Circles circles = new Circles();

		public UserControlCanvs()
		{

			InitializeComponent();


			this.DataContext = lines;
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

	public class ElementBaseCollection : ObservableCollection<ViewModelBase>
	{

		PointBase p2;
		public ElementBaseCollection()
		{
			CoordinateBase c1 = new CoordinateBase();
			PointBase p1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
			p2 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });

			p1.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 0, 0, 1 });
			p2.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 512, 480, 1 });


			Add(ViewModelFactory.CreateViewModel(c1));
			Add(ViewModelFactory.CreateViewModel(p1));
			Add(ViewModelFactory.CreateViewModel(p2));
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
			var mat = new Mat(3, 1, MatType.CV_64FC1, new double[] { -10, -10, 0 });
			p2.Point +=mat;
		}
	}

}
