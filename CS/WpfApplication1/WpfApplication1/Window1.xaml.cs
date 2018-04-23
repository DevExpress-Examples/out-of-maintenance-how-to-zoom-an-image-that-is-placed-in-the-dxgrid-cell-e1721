// Developer Express Code Central Example:
// How to zoom an image that is placed in the DXGrid cell
// 
// You should create the standard Image control on the window, hide it, and show
// this control when the user clicks the image. The image in a grid cell should be
// wrapped in the button. This allows you easy handle the click event.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E1721

using System.Data;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;

namespace WpfApplication1
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		OleDbDataAdapter oleDbDataAdapter;

		public static readonly RoutedCommand ShowImage = new RoutedCommand("ShowImage", typeof(Window1));

		private void image_MouseLeave(object sender, MouseEventArgs e)
		{
			image.Visibility = Visibility.Hidden;
			image.DataContext = null;
		}

		void OnShowImage(object sender, ExecutedRoutedEventArgs e)
		{
			int rowHandle = RowData.GetRowData((DependencyObject)e.OriginalSource).RowHandle.Value;
			image.DataContext = gridControl1.GetCellValue(rowHandle, "Picture");
			image.Visibility = Visibility.Visible;
			image.Width = 700;
			image.Height = 700;
		}

		public Window1()
		{
            InitializeComponent();

            gridControl1.ItemsSource =
                new NwindDataSetTableAdapters.CategoriesTableAdapter().GetData();

			CommandBindings.Add(new CommandBinding(ShowImage, new ExecutedRoutedEventHandler(OnShowImage)));
		}
	}
}
