using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyRealWorld.DAL
{
    [PrimaryKey(nameof(Id))]
    public class Picture
    {
        public int Id { get; set; }
        [Required]
        public string UrlImg { get; set; }
        public string DescriptionImg {  get; set; }

        public ICollection<Project_Pictures> projpics { get; }
    }
}
