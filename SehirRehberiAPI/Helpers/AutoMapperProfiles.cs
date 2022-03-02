using AutoMapper;
using SehirRehberiAPI.Dtos;
using SehirRehberiAPI.Model;
using System.Linq;

namespace SehirRehberiAPI.Helpers
{
    public class AutoMapperProfiles:Profile

        //İKİ TABLOYU EŞLEŞTİRİCEZ
    {
        public AutoMapperProfiles()
        {
            //CİTYFORLİSTDTO' DA photourl var fakat city'inin için de photonun içinde photourl var buna ulaşmak gerek.
            CreateMap<City, CityForListDto>()//BÖYLE YAPARSA SADECE İSİMLERİ EŞLEŞENLERİ MAP YAPICAK BİZ EŞLEŞMEYENLERİ(PHOTOURL) DE AYRI GÖSTERİCEZ.
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
                });

            //SRC => CİTY
            //DEST=>CİTYFORLİSTDTO

            //cityforlistdto'deki photourli map et src(city) deki photoların ismain olanların urlsini al demek


            CreateMap<City, CityForDetailDto>();
        }

    }
}
