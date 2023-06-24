
Console.WriteLine("Hello, World!");
PersonDal p1 = new PersonDal();
var p2 = new PersonDal();
PersonDal p3 = new();

var pers = p3.FromId(2);
p3.FindRelatives(pers);
ArgumentNullException.ThrowIfNull(pers);//change the name of the pers

WriteLine(pers.Name);