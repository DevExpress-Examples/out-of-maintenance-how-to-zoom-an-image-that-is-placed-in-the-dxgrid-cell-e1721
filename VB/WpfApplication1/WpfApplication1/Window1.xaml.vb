' Developer Express Code Central Example:
' How to zoom an image that is placed in the DXGrid cell
' 
' You should create the standard Image control on the window, hide it, and show
' this control when the user clicks the image. The image in a grid cell should be
' wrapped in the button. This allows you easy handle the click event.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E1721

Imports System.Data
Imports System.Data.OleDb
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Grid

Namespace WpfApplication1
    ''' <summary>
    ''' Interaction logic for Window1.xaml
    ''' </summary>
    Partial Public Class Window1
        Inherits Window

        Private oleDbDataAdapter As OleDbDataAdapter

        Public Shared ReadOnly ShowImage As New RoutedCommand("ShowImage", GetType(Window1))

        Private Sub image_MouseLeave(ByVal sender As Object, ByVal e As MouseEventArgs)
            image.Visibility = Visibility.Hidden
            image.DataContext = Nothing
        End Sub

        Private Sub OnShowImage(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            Dim rowHandle As Integer = RowData.GetRowData(DirectCast(e.OriginalSource, DependencyObject)).RowHandle.Value
            image.DataContext = gridControl1.GetCellValue(rowHandle, "Picture")
            image.Visibility = Visibility.Visible
            image.Width = 700
            image.Height = 700
        End Sub

        Public Sub New()
            InitializeComponent()

            gridControl1.ItemsSource = (New NwindDataSetTableAdapters.CategoriesTableAdapter()).GetData()

            CommandBindings.Add(New CommandBinding(ShowImage, New ExecutedRoutedEventHandler(AddressOf OnShowImage)))
        End Sub
    End Class
End Namespace
