using Microsoft.AspNetCore.Identity;

namespace MovieApp.WEBUI.Identity
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
