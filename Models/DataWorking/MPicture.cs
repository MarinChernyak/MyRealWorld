using MRW_DAL.MyWEntities;

namespace MyRealWorld.Models.DataWorking
{

    public class MPicture :Picture
    {
        public int IdReference { get; set; }        
        public int Width { get; set; }
        public int Height { get; set; }
       
    }
}
