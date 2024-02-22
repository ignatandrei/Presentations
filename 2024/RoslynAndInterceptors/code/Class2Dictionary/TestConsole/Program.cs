using TestConsole;

var p = new Person();
p.FirstName = "Andrei";
//set last name via prop
p.SetPropValue(ePerson_Properties.LastName, "Ignat");
Console.WriteLine("called directly last name : " + p.LastName);
//get last name via enum
Console.WriteLine("called via enum of prop first name : " + p.GetPropValue(ePerson_Properties.FirstName));

