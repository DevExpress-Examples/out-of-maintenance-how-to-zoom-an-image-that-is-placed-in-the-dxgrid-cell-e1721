Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpo
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Xpf.Grid
Imports System.Data.OleDb

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
			Dim rowHandle As Integer = RowData.GetRowData(CType(e.OriginalSource, DependencyObject)).RowHandle.Value
			image.DataContext = gridControl1.GetCellValue(rowHandle, "Picture")
			image.Visibility = Visibility.Visible
			image.Width = 700
			image.Height = 700
		End Sub

		Public Sub New()
			InitializeComponent()
			InitSQLDataAdapter()
			gridControl1.ItemsSource = GetData()

			CommandBindings.Add(New CommandBinding(ShowImage, New ExecutedRoutedEventHandler(AddressOf OnShowImage)))
		End Sub
		Private Sub InitSQLDataAdapter()

		   Dim conn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; " & "Data Source=nwind.mdb")
		   Dim sql As String = "SELECT [CategoryID], [CategoryName], [Picture] FROM [Categories]"

		   oleDbDataAdapter = New OleDbDataAdapter()
		   oleDbDataAdapter.SelectCommand = New OleDbCommand(sql, conn)

		End Sub
		Private Function GetData() As DataView
			Dim ds As New DataSet()
			oleDbDataAdapter.Fill(ds)

			Return ds.Tables(0).DefaultView
		End Function


	End Class
End Namespace
