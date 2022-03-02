using SehirRehberiAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberiAPI.Data
{
    public interface IEntityRepository 
    {
        void Add<T>(T entity) where T : class, new();
        void Delete<T>(T entity) where T : class, new();
        bool SaveAll();

        List<City> GetCities();
        List<Photo> GetPhotosByCity(int cityId);
        City GetCityById(int cityId);
        Photo GetPhoto(int id);




    }
}
