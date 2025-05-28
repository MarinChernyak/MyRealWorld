using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRW_DAL
{

    [PrimaryKey(nameof(Id))]
    internal   class KeyWords
    {
         int Id { get; set; }
        [Required]
        public string KWS { get; set; }
        [Required]
        public string Default { get; set; }

    }
}
