using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyRealWorld.Models.Utilities;
using System.Reflection;

namespace MyRealWorld.Models.Factories
{
    public class GeoFactory :BaseFactory
    {
        #region GET
        public GeoFactory()
        {
        }
        //    public List<SelectListItem> GetCountriesListCollection()
        //    {
        //        List<SelectListItem> lst = new List<SelectListItem>();
        //        using (NostraGeoContext _context = new NostraGeoContext())
        //        {
        //            List<Country> lstCountry = _context.Countries.OrderBy(x => x.CountryName).ToList();
        //            lst = FromLstObjectsToDropDownFeed(lstCountry, "CountryName", "Id");
        //        }               
        //        return lst;
        //    }
        //    public List<SelectListItem> GetStatesListCollection(int IdCountry)
        //    {
        //        List<SelectListItem> lst = new List<SelectListItem>();
        //        using (NostraGeoContext _context = new NostraGeoContext())
        //        {
        //            List<StateRegion> lstData = _context.StateRegions.Where(x => x.CountryRef == IdCountry).ToList();

        //            if (lstData != null)
        //            {

        //                lstData = lstData.OrderBy(x => x.StateRegion1).ToList();
        //            }
        //            lst = FromLstObjectsToDropDownFeed(lstData, "StateRegion1", "Id");
        //        }
        //        InsertSelectItem(lst);


        //        return lst;
        //    }
        //    public List<SelectListItem> GetCitiesListCollection(int IdCountry, int IdState=0)
        //    {
        //        List<SelectListItem> lst = new List<SelectListItem>();
        //        using (NostraGeoContext _context = new NostraGeoContext())
        //        {
        //            if (IdCountry > 0 || IdState > 0)
        //            {
        //                IQueryable<City> lstData = null;
        //                if (IdState > 0)
        //                {
        //                    lstData = _context.Cities.Where(x => x.RegionState == IdState);
        //                }
        //                else if (IdCountry > 0)
        //                {
        //                    lstData = _context.Cities.Where(x => x.Country == IdCountry);
        //                }
        //                if (lstData != null)
        //                {

        //                    lstData = lstData.OrderBy(x => x.CityName);
        //                }
        //                lst = FromLstObjectsToDropDownFeed(lstData.ToList(), "CityName", "Id");
        //            }
        //        }
        //        InsertSelectItem(lst);

        //        return lst;
        //    }
        //    public List<List<SelectListItem>> GetStatesCitiesByCountry(short? id)
        //    {
        //        short Id = id ?? 0;
        //        List<List<SelectListItem>> lstout = new List<List<SelectListItem>>();

        //        if (id > 0)
        //        {
        //            lstout.Add(GetStatesListCollection(Id));
        //            lstout.Add(GetCitiesListCollection(Id, 0));
        //        }
        //        return lstout;
        //    }
        //    public List<SelectListItem> GetTimeZones()
        //    {
        //        List<SelectListItem> lst = new List<SelectListItem>();
        //        using (NostraGeoContext _context = new NostraGeoContext())
        //        {
        //            List<TimeZoneList> lstTZ = _context.TimeZoneLists.OrderBy(x => x.TimeOffset).ToList();
        //            foreach(TimeZoneList tz in lstTZ)
        //            {
        //                lst.Add(new SelectListItem()
        //                {
        //                    Value=tz.Idtzone.ToString(),
        //                    Text = $"{tz.TzoneName} - ({tz.TimeOffset})"
        //                });
        //            }
        //        }
        //        return lst;
        //    }


        //    public void GetCountryStateByCity(int idCity, out int IdCountry,out int IdState)
        //    {
        //        IdCountry = IdState = 0;
        //        if(idCity>0)
        //        {
        //            using (NostraGeoContext _context = new NostraGeoContext())
        //            {
        //                City ct = _context.Cities.FirstOrDefault(x => x.Id == idCity);
        //                if(ct !=null)
        //                {
        //                    IdCountry = ct.Country;
        //                    IdState = ct.RegionState;
        //                }
        //            }
        //        }
        //    }
        //    public string GetDisplayPlaceEvent(int id)
        //    {
        //        string place = string.Empty;
        //        if (id > 0)
        //        {
        //            using (NostraGeoContext _context = new NostraGeoContext())
        //            {
        //                City ct = _context.Cities.FirstOrDefault(x => x.Id == id);
        //                if (ct != null)
        //                {
        //                    place = ct.CityName;
        //                    int IdCountry = ct.Country;
        //                    if(IdCountry>0)
        //                    {
        //                        var country = _context.Countries.FirstOrDefault(x => x.Id == IdCountry);
        //                        place = $"{place} ({country.Acronym})";
        //                    }
        //                }
        //            }
        //        }
        //        return place;
        //    }

        #endregion
    }
}
