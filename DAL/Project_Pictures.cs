using Microsoft.EntityFrameworkCore;


namespace MyRealWorld.DAL
{
    [PrimaryKey(nameof(ProjectId), nameof(PictureID))]
    public class Project_Pictures
    {
        public int ProjectId { get; set; }
        public int PictureID { get; set; }

        public Picture pictures { get; set; }
    }
}
