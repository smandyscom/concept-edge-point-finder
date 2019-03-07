using OpenCvSharp.Extensions;
using Core.Derived;
using System.Windows.Media.Imaging;

namespace Presentation.ViewModels
{
	public class ViewModelGrayImage : ViewModelBase
	{
		public ViewModelGrayImage(GrayImage Element) : base(Element)
		{
			Zindex = 0;	// base map
		}

		/// <summary>
		/// The model as GrayImage
		/// </summary>
		internal GrayImage ImageElement
		{
			get { return m_element as GrayImage; }
		}

		public BitmapSource Bitmap { get { return ImageElement.Image.ToBitmapSource(); } }
	}
}
