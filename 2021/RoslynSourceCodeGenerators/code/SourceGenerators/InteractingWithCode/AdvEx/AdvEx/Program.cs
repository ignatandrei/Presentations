using AOPMethodsCommon;
using System;


namespace AdvEx
{
    [AutoEnum(template = EnumMethod.CustomTemplateFile, CustomTemplateFileName ="enum.txt")]
    public enum State
    {
        None = 0,
        Active = 1,
        Wait=2
    }
    class Program
    {
        static void Main(string[] args)
        {
            ParseEnum();
            CreateFromInterface();
            CopyConstructor();
        }

        private static void CopyConstructor()
        {
            var p1 = new Person();
            p1.ID = 7;
            p1.FirstName = "Andrei";
            (int id, _, _) = p1;
            Console.WriteLine(id);
            var p2 = new Person(p1);
            Console.WriteLine(p2.FirstName);

        }

        private static void CreateFromInterface()
        {
            IPerson p = (IPerson)new PersonMock
            {
                MockFirstName = "Andrei",
                MockLastName ="Ignat",
                MockFullName = ()=>"test"
            };
            Console.WriteLine(p.LastName);
            Console.WriteLine(p.FullName());

        }

        static void ParseEnum()
        {
            Console.WriteLine("Please give the state");
            foreach (var item in enumState.KeyValues)
            {
                Console.Write($"{item.Value} => {item.Key} ");
            }
            Console.WriteLine();
            var s = Console.ReadLine();

            if(!Enum.TryParse<State>(s,true,out var res))
            {
                res = State.None;
            }
            
            var res1 = enumState.ParseExactState(s,  State.None);

            Console.WriteLine(res.ToString());
            Console.WriteLine(res1.ToString());
        }
    }
}
