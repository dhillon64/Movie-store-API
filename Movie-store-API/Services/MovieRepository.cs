using Microsoft.EntityFrameworkCore;
using Movie_store_API.Contracts;
using Movie_store_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Movie entity)
        {
            await _db.Movies.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Movie entity)
        {
            _db.Movies.Remove(entity);
            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Movies.AnyAsync(q => q.Id == id);
        }

        public async Task<IList<Movie>> FindAll()
        {
            return await _db.Movies.ToListAsync();
        }

        public async Task<Movie> FindById(int id)
        {
            return await _db.Movies.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> Update(Movie entity)
        {
            _db.Movies.Update(entity);
            return await Save();
        }

        public async Task<ICollection<Movie>> FindMoviesByActor(int id)
        {
            var Movies = await FindAll();
            return Movies.Where(q => q.Id == id).ToList();
        }

       
    }
}
