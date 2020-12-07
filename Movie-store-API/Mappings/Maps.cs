using AutoMapper;
using Movie_store_API.Data;
using Movie_store_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.Mappings
{
    public class Maps: Profile 
    {
        public Maps()
        {
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<Actor, CreateActorDTO>().ReverseMap();
            CreateMap<Actor, UpdateActorDto>().ReverseMap();
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<Movie, CreateMovieDTO>().ReverseMap();
            CreateMap<Movie, UpdateActorDto>().ReverseMap();

        }
    }
}
