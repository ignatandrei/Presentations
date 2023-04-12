using AsyncAwait;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoBlocking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //private void btnBlock_Click(object sender, EventArgs e)
        private async void btnBlock_Click(object sender, EventArgs e)
        {
            var t = new TwoTasks();
            //var result = t.Await2Task().GetAwaiter().GetResult();
            var result = await t.Await2Task();
            MessageBox.Show($"result :{result}");
        }
    }
}
