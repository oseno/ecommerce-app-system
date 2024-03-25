using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace ShaneCyber.Models
{
    public class AppUser : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public string? PhoneNo { get; set; }

    }



}
