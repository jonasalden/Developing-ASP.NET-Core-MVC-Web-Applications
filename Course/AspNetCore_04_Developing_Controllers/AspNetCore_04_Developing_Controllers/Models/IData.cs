using System.Collections.Generic;

namespace AspNetCore_04_Developing_Controllers.Models
{
    public interface IData
    {
        List<City> CityList { get; set; }
        List<City> CityInitializeData();
        City GetCityById(int? id);
    }
}
