using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRealWorld.DAL
{
    [PrimaryKey(nameof(ProjectId), nameof(KWId))]
    public class ProjectsKW
    {

        public int ProjectId { get; set; }

        public int KWId { get; set; }



    }
}
