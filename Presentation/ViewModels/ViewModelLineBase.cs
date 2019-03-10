using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using Core.Derived;
using OpenCvSharp;
using Core.Arch;

namespace Presentation.ViewModels
{
	public class ViewModelLineBase : ViewModelBase
	{
		public ViewModelLineBase(LineBase Element) : base(Element) { }

		/// <summary>
		/// The model as LineBase
		/// </summary>
		internal LineBase Line
		{
			get { return m_element as LineBase; }
		}

		public double X1 { get { return Line.End1.Point.At<double>(0); } }
		public double Y1 { get { return Line.End1.Point.At<double>(1); } }
		public double X2 { get { return Line.End2.Point.At<double>(0); } }
		public double Y2 { get { return Line.End2.Point.At<double>(1); } }
	}
}
