namespace DiDemoASPNETCore
{
    public interface IMyImportantClass
    {
        string MyMessage { get;  }
    }
    public class MyImportantClass: IMyImportantClass
    {
        public MyImportantClass()
        {
            this.MyMessage = "welcome to DI by Controller constructor";
        }
        public string MyMessage { get; set; }
    }
}