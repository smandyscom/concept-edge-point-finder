using ImageProfiler.ViewModels;
using System.Windows.Controls;



namespace ImageProfiler
{
	/// <summary>
	/// UserControlCanvs.xaml 的互動邏輯
	/// </summary>
	public partial class UserControlCanvs : UserControl
	{
		private ViewModelCanvas elements = new ViewModelCanvas();
		public UserControlCanvs()
		{

			InitializeComponent();
			this.DataContext = elements;


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

}
