using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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

		}

		public ObservableCollection<Line> Lines { get; private set; }
	}

	public class Lines : ObservableCollection<Line>
	{
		public Lines()
		{
			Add(new Line { From = new Point(0, 0), To = new Point(512, 480) });
			Add(new Line { From = new Point(180, 180), To = new Point(20, 180) });
			Add(new Line { From = new Point(20, 180), To = new Point(100, 20) });
			Add(new Line { From = new Point(20, 50), To = new Point(180, 150) });
		}
	}

	public class Line
	{
		public Point From { get; set; }

		public Point To { get; set; }
	}
}
