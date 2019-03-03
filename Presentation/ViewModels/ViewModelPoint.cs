using OpenCvSharp;

using Core.Arch;

namespace Presentation.ViewModels 
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModelPoint : ViewModelBase
    {
		public ViewModelPoint(PointBase Element) : base(Element) { }

		/// <summary>
		/// The model as point base
		/// </summary>
		public PointBase Point
		{
			get { return m_element as PointBase; }
		}

		public Mat Location { get { return Point.Point; } }

		

		
	}
}
