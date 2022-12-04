using System.Net.Http.Headers;

namespace Shared
{
    public class Customer : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
        public Customer(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}