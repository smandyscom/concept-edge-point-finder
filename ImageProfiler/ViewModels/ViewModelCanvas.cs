using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using Core.Arch;
using Core.Derived;
using OpenCvSharp;
using Presentation.ViewModels;

namespace ImageProfiler.ViewModels
{
	public class ViewModelCanvas : INotifyPropertyChanged
	{

		/// <summary>
		/// hold all ElementBase in bag
		/// </summary>
		public ElementBaseCollection Elements { get; set; } = new ElementBaseCollection();

		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}



	public class ElementBaseCollection : ObservableCollection<ViewModelBase>
	{

		public void AddRange(IList<ViewModelBase> list)
		{
			foreach (var item in list)
				Add(item);
		}

		public void EnvelopElements(IList<ElementBase> list)
		{
			foreach (var item in list)
			{
				Add(ViewModelFactory.CreateViewModel(item));
			}
		}

		public void ForEach(Action<ViewModelBase> action)
		{
			foreach (var item in this)
			{
				action(item);
			}
		}

		#region TestonView

		PointBase p2;
		public ElementBaseCollection()
		{
			CoordinateBase c1 = new CoordinateBase();
			PointBase p1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
			p2 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });

			p1.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 0, 0, 1 });
			p2.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 512, 480, 1 });
			LineBase line = new LineBase(new List<ElementBase>() { p1, p2 });

			GrayImage img = new GrayImage(new List<ElementBase>() { c1 });
			Cv2.CvtColor(Cv2.ImRead(@"..\..\lenna.png"), img.Image, ColorConversionCodes.BGR2GRAY);

			PointEdge pointEdge = new PointEdge(new List<ElementBase> { img, line });

			// items sequence affact ZIndex in itemsControl
			EnvelopElements(new List<ElementBase>() { img,c1, p1, p2, line, pointEdge });
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
			p2.Point += mat;
		}

		#endregion



	}




}
