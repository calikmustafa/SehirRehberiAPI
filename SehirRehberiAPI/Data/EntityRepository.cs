using Microsoft.EntityFrameworkCore;
using SehirRehberiAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberiAPI.Data
{
    public class EntityRepository : IEntityRepository
    {
        private DataContext _context;
        public EntityRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class, new()
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class, new()
        {
            _context.Remove(entity);
        }

        public List<City> GetCities()
        {
            var list = _context.Cities.Include(c=>c.Photos).ToList();
            return list;
        }

        public City GetCityById(int cityId)
        {
            var city=_context.Cities.Include(c=>c.Photos).FirstOrDefault(c=>c.Id==cityId);
            return city;
        }

        public Photo GetPhoto(int id)
        {
            var photo=_context.Photos.ToList().FirstOrDefault(c=>c.Id==id);
            return photo;
        }

        public List<Photo> GetPhotosByCity(int cityId)
        {
            var list=_context.Photos.Where(c=>c.CityId==cityId).ToList();
            return list;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
