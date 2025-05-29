using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;

namespace MyRealWorld.DAL
{
    [PrimaryKey(nameof(Id))]
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ProjectVersion { get; set; }

        public ICollection<ProjectsKW> projkw { get; }
        public ICollection<Project_Pictures> projpics { get; }
    }
}
