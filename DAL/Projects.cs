using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;

namespace MRW_DAL
{
    [PrimaryKey(nameof(Id))]
    internal class Projects
    {
        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ProjectVersion { get; set; }
        [Required]
        public string Picture { get; set; }
    }
}
