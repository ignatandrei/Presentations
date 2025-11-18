using Microsoft.AspNetCore.Identity;

namespace whatsNewNet10.Web;

internal class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}