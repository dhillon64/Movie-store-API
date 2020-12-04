
using Microsoft.EntityFrameworkCore;
using Movie_store_API.Contracts;
using Movie_store_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store_API.Services
{
    public class ActorRepository : IActorRepository
    {
        private readonly ApplicationDbContext _db;

        public ActorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Actor entity)
        {
            await _db.Actors.AddAsync(entity);
            return await Save();

            
        }

        public async Task<bool> Delete(Actor entity)
        {
            _db.Remove(entity);
             return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Actors.AnyAsync(q => q.Id == id);
        }

        public async Task<IList<Actor>> FindAll()
        {
            return await _db.Actors.ToListAsync();
        }

        public async Task<Actor> FindById(int id)
        {
            return await _db.Actors.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
            
        }

        public async Task<bool> Update(Actor entity)
        {
             _db.Update(entity);
            return await Save();
        }
    }
}
