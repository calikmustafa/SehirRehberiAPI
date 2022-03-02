namespace SehirRehberiAPI.Dtos
{
    public class CityForListDto
        //ŞEHRİN TÜM ALANLARI DEĞİLDE SADECE BU ALANLAR GELSİN DEDİĞİMİZDE AUTOMAPPER VE DTO KULLANMAK ZORUNDAYIZ
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }

        
    }
}
