using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace DataLayer.Dtos.Image
{
    public class ImageDto
{
   public IFormFile file {get; set;}
   public string UserName { get; set; }
   public string comment {get; set;}

   public int ? Id {get; set;}

   public string ? ImageContentBase64 { get; set; }
     
  
    }

}