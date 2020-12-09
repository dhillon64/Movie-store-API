using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]

        public string Email { get; set; }
    }

    public class CustomerViewDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }


    }
}
