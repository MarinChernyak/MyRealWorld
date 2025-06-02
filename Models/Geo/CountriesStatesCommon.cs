using Microsoft.AspNetCore.Mvc.Rendering;
using MyRealWorld.Common;
using MyRealWorld.Models.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.Models.Geo
{
    public class CountriesStatesCommon : PlaceDataCommon
    {
        [DisplayName("Country")]
        public short? CountryId { get; set; }
        [DisplayName("State/Province/Land")]
        public int? StateId { get; set; }

        public List<SelectListItem> Countries { get; protected set; }
        public List<SelectListItem> States { get; protected set; }
        public override void InitCombos()
        {
            //FillUpCountries();
            //States = new List<SelectListItem>();
        }

        public override bool SaveData()
        {
            return false;
        }
        //protected void FillUpCountries()
        //{
        //    GeoFactory fact = new GeoFactory();
        //    Countries = fact.GetCountriesListCollection();
        //    Countries.Insert(0, new SelectListItem()
        //    {
        //        Text = Constants.Values.ZeroStringComboText,
        //        Value = Constants.Values.ZeroStringComboValue
        //    });
        //    SelectListItem item = Countries.FirstOrDefault(x => x.Value == CountryId.ToString());
        //    if (item != null)
        //    {
        //        item.Selected = true;
        //    }
        //}

    }
}
