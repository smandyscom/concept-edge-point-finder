using Core.Arch;

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

			return vm;
		}
	}
}
