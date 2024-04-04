using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; } // Corrected property definition

        public string comment { get; set; } // Corrected property definition
    }
}
