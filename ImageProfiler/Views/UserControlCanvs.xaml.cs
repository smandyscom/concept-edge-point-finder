﻿using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Presentation;

namespace ImageProfiler
{
	/// <summary>
	/// UserControlCanvs.xaml 的互動邏輯
	/// </summary>
	public partial class UserControlCanvs : UserControl
	{
		//private Lines lines = new Lines();
		//private Circles circles = new Circles();

		public UserControlCanvs()
		{

			InitializeComponent();

			
			//this.DataContext = model.LayerCollection;
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

	//public class Lines : ObservableCollection<IDraw>
	//{
	//	//private LineEdgePoint line = new LineEdgePoint();
	//	public Lines()
	//	{
	//		var temp = new PointF(0, 0);
	//		line.__start.Location = temp;

	//		temp = new PointF(512, 480);
	//		line.__end.Location = temp;

	//		Add(line);
	//		CreateTimer();
	//	}
	//	void CreateTimer()
	//	{
	//		var timer1 = new Timer
	//		{
	//			Enabled = true,
	//			Interval = 2000
	//		};
	//		timer1.Elapsed += Timer1_Elapsed;
	//	}

	//	private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
	//	{
	//		var temp = line.__end.Location;
	//		temp.X -= 10;
	//		temp.Y -= 10;
	//		line.__end.Location = temp;
	//		line.isSelected = !line.isSelected;
	//	}
	//}

	//public class Circles : ObservableCollection<IDraw>
	//{
	//	private CircleFitted circle = new CircleFitted();
	//	public Circles()
	//	{
	//		var temp = new PointF(256, 240);
	//		circle.__radius = 10;
	//		circle.__center.Location = temp;
	//		Add(circle);
	//		CreateTimer();
	//	}
	//	void CreateTimer()
	//	{
	//		var timer1 = new Timer
	//		{
	//			Enabled = true,
	//			Interval = 2000
	//		};
	//		timer1.Elapsed += Timer1_Elapsed;
	//	}

	//	private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
	//	{
	//		var temp = circle.__center.Location;
	//		temp.X += 10;
	//		temp.Y += 10;
	//		circle.__center.Location = temp;
	//		circle.__radius += 1;
	//	}
	//}


}
