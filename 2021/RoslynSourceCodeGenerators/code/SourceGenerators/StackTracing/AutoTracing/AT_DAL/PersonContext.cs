using AOPMethodsCommon;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AT_DAL
{
    [AutoMethods(template = TemplateMethod.CustomTemplateFile, CustomTemplateFileName = "AutoMethods.txt", MethodPrefix = "auto")]

    public partial class PersonContext
    {
        private string autoTryOpenSql()
        {
            using (var con =new SqlConnection("Data Source=MyDataSource;Initial Catalog=tests;Integrated Security=False;User ID=sa;Password=yourStrong(!)Password"))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = "select";
                    cmd.ExecuteNonQuery();
                }
            }
            return "asdasd";
        }
        static string NameAss = ThisAssembly.Project.AssemblyName;
        //put [auto] prefix
        public async Task<Person[]> autoSearchAfterFullName(string SearchName)
        {
            try
            {
                TryOpenSql();
            }
            catch
            {

            }
            if (string.IsNullOrEmpty(SearchName))
                SearchName = "Ignat";

            var ret = new List<Person>();
            await Task.Delay(100 * new Random().Next(1, (SearchName.Length / 2)));
            for (int i = 0; i < SearchName.Length / 10 + 2; i++)
            {

                ret.Add(new Person()
                {
                    ID = i + 1,
                    Name = $"Person with {SearchName}"
                });

            }
            return ret.ToArray();
        }
    }
}
