using AOPMethodsCommon;
using AT_DAL;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AT_BL
{

    [AutoMethods(template = TemplateMethod.CustomTemplateFile, CustomTemplateFileName = "AutoMethods.txt", MethodPrefix = "auto")]

    public partial class PersonRepository
    {
        static string NameAss = ThisAssembly.Project.AssemblyName;
        private readonly PersonContext pc;

        public PersonRepository(PersonContext pc)
        {
            var a = Activity.Current;
            var s = a?.Source;
            
            this.pc = pc;
        }

    
    //TODO: put auto
    async Task<bool> autoLoadDetails(Person p)
        {
            
            var nr = new Random().Next(1, p.ID * 1000);
            await Task.Delay(nr);
            return true;
        }
        //todo: put auto
        async Task<Person[]> autoSearchFullName(string SearchName)
        {
            await Task.Delay(500);
            return await pc.SearchAfterFullName(SearchName);
        }
        //TODO: put auto
        public async Task<Person[]> autoSearchAndLoadData(string name)
        {
            var personsFound = await SearchFullName(name);
            
            foreach (var item in personsFound)
            {
                if( !await LoadDetails(item))
                    break;

            }
            
            var tasks = personsFound.Select(it => LoadDetails(it));
            await Task.WhenAll(tasks);

            return personsFound;
        }
    }

}

