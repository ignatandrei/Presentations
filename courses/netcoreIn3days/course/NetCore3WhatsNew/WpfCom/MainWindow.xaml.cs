using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using inter = Microsoft.Office.Interop.Excel;
namespace WpfCom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ///properties: embed interop types
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            var obj = new inter.Application();

            obj.Visible = true;
            var wk= obj.Workbooks;
            //do not do this! it will keep excel in memory
            //var doc = obj.Workbooks.Add();
            var doc = wk.Add();
            var ws = doc.Worksheets;
            var sheet = ws[1] as inter.Worksheet;
            var range = sheet.Range["A1"] as inter.Range;
            range.Value = "http://msprogrammer.serviciipeweb.ro";
            MessageBox.Show("See excel ...");
            doc.Close(SaveChanges: false);
            obj.Quit();
            MessageBox.Show("See excel in task manager");
            Marshal.ReleaseComObject(range);
            Marshal.ReleaseComObject(sheet);
            Marshal.ReleaseComObject(ws);
            Marshal.ReleaseComObject(doc);
            Marshal.ReleaseComObject(wk);
            Marshal.ReleaseComObject(obj);


        }
    }
}
