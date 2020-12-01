using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.Data
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        
        public string Title { get; set; }

        

        public int? Year { get; set; }

        

        public string Summary { get; set; }

        public string Image { get; set; }

        
        
        public double? Price { get; set; }

        public int? ActorId { get; set; }

        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }


    }
}
