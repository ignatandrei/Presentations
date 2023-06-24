using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Part3Console;

class PersonDal
{
    public Person? FromId(int id)
    {
        if (id % 2 == 0) return null;
        return new Person(id, $"Andrei {id}");
    }
    

    public Person[]? FindRelatives(Person? p, [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0,
         [CallerArgumentExpression(nameof(p))] string? message = null
        )
    {
        if(p==null)
        {
            Console.WriteLine(message);//why is pers?
            ArgumentNullException.ThrowIfNull(p);
        }

        Person[]? result = null;
        return result;
    }
}