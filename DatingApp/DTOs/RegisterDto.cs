using System;

using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string knownAs { get; set; }
        [Required]
        public DateTime dob { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string country { get; set; }

        [Required, MaxLength(20), MinLength(6)]
        public string Password { get; set; }


    }
}
