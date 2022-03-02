using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SehirRehberiAPI.Data;
using SehirRehberiAPI.Dtos;
using SehirRehberiAPI.Model;
using System.Collections.Generic;

namespace SehirRehberiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IMapper _mapper;

        public CitiesController(IEntityRepository entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities=_entityRepository.GetCities();
            var citiesReturn=_mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesReturn);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Insert([FromBody] City city)
        {
            _entityRepository.Add(city);
            _entityRepository.SaveAll();
            return Ok(city);
        }

        [HttpGet]
        [Route("detail")]
        public IActionResult GetCityById(int id)
        {
          var city= _entityRepository.GetCityById(id);
            var cityToReturn= _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }

        [HttpGet]
        [Route("photos")]

        public IActionResult GetPhotoByCity(int cityId)
        {
            var list=_entityRepository.GetPhotosByCity(cityId);
            return Ok(list);
        }
    }
}
