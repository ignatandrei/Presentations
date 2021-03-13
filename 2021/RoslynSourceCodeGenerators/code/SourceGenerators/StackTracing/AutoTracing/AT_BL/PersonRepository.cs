using AOPMethodsCommon;
using AT_DAL;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AT_BL
{

    [AutoMethods(template = TemplateMethod.CustomTemplateFile, CustomTemplateFileName = "AutoMethods.txt", MethodPrefix = "auto")]

    public partial class PersonRepository
    {
        static string NameAss = ThisAssembly.Project.AssemblyName;
        private readonly PersonContext pc;

        public PersonRepository(PersonContext pc )
        {
            var a = Activity.Current;
            var s = a?.Source;
            
            this.pc = pc;
        }

    
    //put [auto] prefix
    async Task<bool> LoadDetails(Person p)
        {
            
            var nr = new Random().Next(1, p.ID * 1000);
            await Task.Delay(nr);
            return true;
        }
        //put [auto] prefix
        async Task<Person[]> SearchFullName(string SearchName)
        {
            await Task.Delay(1000 );
            return await pc.SearchAfterFullName(SearchName);
        }
        //put [auto] prefix
        public async Task<Person[]> SearchAndLoadData(string name)
        {
            var personsFound = await SearchFullName(name);
            
            foreach (var item in personsFound)
            {
                if( !await LoadDetails(item))
                    break;

            }
            
            var tasks = personsFound.Select(it => LoadDetails(it));
            await Task.WhenAll(tasks);
            foreach (var item in personsFound)
            {
                try
                {
                    await FindDebts(item);
                }
                catch (Exception)
                {

                    //maybe log? 
                }
            }
            var c = new HttpClient();
            var s = c.GetStringAsync("http://www.bing.com");
            return personsFound;
        }
        //put [auto] prefix
        async Task<int> FindDebts(Person p)
        {
            await Task.Delay(1000);
            if(p.ID % 2== 0)
                throw new ArgumentException("a test exception");
            return p.ID;
        }
    }

}

