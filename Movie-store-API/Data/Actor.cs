using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.Data
{
    public class Actor
    {
        [Key]
        public int Id { get; set; } 

        
        public string Firstname { get; set; }

        
        public string Lastname { get; set; }

        public string Bio { get; set; }
         
        public virtual IList<Movie> Movies { get; set; }


    }
}
