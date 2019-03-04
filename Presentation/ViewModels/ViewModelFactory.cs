using Core.Arch;
using Core.Derived;

namespace Presentation.ViewModels
{
	public class ViewModelFactory
	{
		public static ViewModelBase CreateViewModel(ElementBase element)
		{
			ViewModelBase vm = null;
			if (element is PointBase)
			{
				vm = new ViewModelPoint(element as PointBase);
			}
			else if (element is CoordinateBase)
			{
				vm = new ViewModelCoordinate(element as CoordinateBase);
			}
			else if (element is LineBase)
			{
				vm = new ViewModelLineBase(element as LineBase);
			}
			else if (element is GrayImage)
			{
				vm = new ViewModelGrayImage(element as GrayImage);
			}
			return vm;
		}
	}
}
