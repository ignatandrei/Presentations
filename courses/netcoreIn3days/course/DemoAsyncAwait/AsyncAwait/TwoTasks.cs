using System;
using System.IO;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class TwoTasks
    {
        public async Task<bool> Await2TaskSimple()
        {
            var file = WriteToFile();
            var console = ReadConsole();
            await Task.WhenAll(file, console);
            return file.Result & console.Result;

        }

            public async Task<bool> Await2Task()
        {
            var file = WriteToFile();
            var console =ReadConsole();
            Task t= Task.WhenAll(file, console); 
            try
            {
                //await file;
                //await console;
                await Task.WhenAll(file, console);
                await t;
                
                return file.Result & console.Result;
            }
            catch(Exception)
            {
                //Console.WriteLine("exception:" + ex.Message);   
                if (file.IsFaulted)
                {
                    Console.WriteLine("file is faulted exceptions :" + file.Exception.InnerExceptions.Count);

                }
                if (console.IsFaulted)
                {
                    Console.WriteLine("console is faulted exceptions :" + console.Exception.InnerExceptions.Count);
                }
                throw;

            }
        }
        public async Task<bool> WriteToFile()
        {
            await Task.Delay(5000);

            using (StreamWriter writer = File.CreateText("newfile.txt"))
            {
                await writer.WriteAsync('a');
            }
            throw new ArgumentException("exception from WriteToFile");
            //return false;

        }
        public async Task<bool> ReadConsole()
        {
            await Task.Delay(3000);
            throw new ArgumentException("exception from WriteToConsole");
            return true;
            
        }
    }
}
