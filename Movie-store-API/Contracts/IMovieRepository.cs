using Movie_store_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.Contracts
{
    public interface IMovieRepository: IRepositoryBase<Movie>
    {
        Task<ICollection<Movie>> FindMoviesByActor(int id);

    }
}
