using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? Year { get; set; }

        public string Summary { get; set; }

        public string Image { get; set; }

        public double? Price { get; set; }

        public int? ActorId { get; set; }
 
        public ActorDTO Actor { get; set; }
    }

    public class CreateMovieDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]

        public int? Year { get; set; }

        [Required]
        [StringLength(500)]

        public string Summary { get; set; }

        public string Image { get; set; }

        public double? Price { get; set; }
        [Required]
        public int ActorId { get; set; }

    }

    public class UpdateMovieDTO
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]

        public int? Year { get; set; }

        [Required]
        [StringLength(500)]

        public string Summary { get; set; }

        public string Image { get; set; }

        public double? Price { get; set; }
       
    }


}
