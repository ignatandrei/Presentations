using AOPMethodsCommon;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AT_DAL
{
    [AutoMethods(template = TemplateMethod.CustomTemplateFile, CustomTemplateFileName = "AutoMethods.txt", MethodPrefix = "auto")]

    public partial class PersonContext
    {
        static string NameAss = ThisAssembly.Project.AssemblyName;

        public async Task<Person[]> autoSearchAfterFullName(string SearchName)
        {
            if (string.IsNullOrEmpty(SearchName))
                SearchName = "Ignat";
            var ret = new List<Person>();
            await Task.Delay(1000);
            for (int i = 0; i < SearchName.Length / 4 + 1; i++)
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
