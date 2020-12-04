using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }

        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }

        public string Bio { get; set; }

        public virtual IList<MovieDTO> Movies { get; set; }
    }

    public class CreateActorDTO
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Bio { get; set; }
    }

    public class UpdateActorDto
    {
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }
        
        [Required]

        public string Lastname { get; set; }

        public string Bio { get; set; }

    }
}
