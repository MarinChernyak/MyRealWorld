using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.Models.Geo
{
    public abstract class PlaceDataCommon
    {
        public enum PlaceDataType
        {
            City=1,
            Coordinates,
            Expansion
        }
        
        public string MainLabel { get; set; }
        public abstract void InitCombos();
        public abstract bool SaveData();
    }
}
