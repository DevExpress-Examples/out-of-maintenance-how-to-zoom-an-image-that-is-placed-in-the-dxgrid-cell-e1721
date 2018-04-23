using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpf.Grid;
using System.Data.OleDb;

namespace WpfApplication1 {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        OleDbDataAdapter oleDbDataAdapter;

       public static readonly RoutedCommand ShowImage = new RoutedCommand("ShowImage", typeof(Window1));

        private void image_MouseLeave(object sender, MouseEventArgs e) {
            image.Visibility = Visibility.Hidden;
            image.DataContext = null;
        }
        void OnShowImage(object sender, ExecutedRoutedEventArgs e) {
            int rowHandle = RowData.GetRowData((DependencyObject)e.OriginalSource).RowHandle.Value;
            image.DataContext = gridControl1.GetCellValue(rowHandle, "Picture");
            image.Visibility = Visibility.Visible;            
            image.Width = 700;
            image.Height = 700;
        }

        public Window1() {
            InitializeComponent();
            InitSQLDataAdapter();
            gridControl1.ItemsSource = GetData();
            
            CommandBindings.Add(new CommandBinding(ShowImage, new ExecutedRoutedEventHandler(OnShowImage)));
       }
        private void InitSQLDataAdapter() {

           OleDbConnection conn = new OleDbConnection( "Provider=Microsoft.Jet.OLEDB.4.0; " + 
            "Data Source=nwind.mdb");
           string sql = "SELECT [CategoryID], [CategoryName], [Picture] FROM [Categories]";

           oleDbDataAdapter = new OleDbDataAdapter();
           oleDbDataAdapter.SelectCommand = new OleDbCommand(sql, conn);

        }
        private DataView GetData() {
            DataSet ds = new DataSet();
            oleDbDataAdapter.Fill(ds);

            return ds.Tables[0].DefaultView;
        }


    }
}
