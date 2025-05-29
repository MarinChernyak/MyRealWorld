using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Mono.TextTemplating;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRealWorld.DAL
{

    [PrimaryKey(nameof(Id))]
    public   class KeyWords
    {
         int Id { get; set; }
        [Required]
        public string KWS { get; set; }
        [Required]
        public string DefaultEn { get; set; }

        public ICollection<ProjectsKW> projkw { get; }
    }
}
