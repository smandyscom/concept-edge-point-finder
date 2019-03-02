using System;
using System.Collections.Generic;
using System.Linq;
using OpenCvSharp;

using Core.Arch;

namespace Presentation.ViewModels
{
	/// <summary>
	/// 
	/// </summary>
	public class ViewModelCoordinate : ViewModelBase
	{
		public ViewModelCoordinate(CoordinateBase Element) : base(Element) { }

		/// <summary>
		/// The model asCoordinateBase
		/// </summary>
		internal CoordinateBase Coordinate
		{
			get { return m_element as CoordinateBase; }
		}

		public double OriginX { get { return Coordinate.Origin.At<double>(0); } }
		public double OriginY { get { return Coordinate.Origin.At<double>(1); } }

	}
}
