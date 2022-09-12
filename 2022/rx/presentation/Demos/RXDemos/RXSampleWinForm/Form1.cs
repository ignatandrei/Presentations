using static System.Net.Mime.MediaTypeNames;
using System.Reactive.Linq;
using System.Reactive;

namespace RXSampleWinForm
{
    public partial class Form1 : Form
    {
        IObservable<string> input;
        IDisposable observer;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            input = 
                Observable.FromEventPattern(textBox1, "TextChanged")
                .Select(it=> (it.Sender as TextBox)!.Text);

            observer= input
                .Do(it=> label1.Text = "alwasy intercept: "+ it)
                .Throttle(TimeSpan.FromSeconds(3))
                .DistinctUntilChanged()
                .Subscribe(it => label1.Text = "Latest Type : " + it);
                

        }
    }
}