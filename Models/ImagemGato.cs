using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace DesafioApi.Models
{
    public class ImagemGato
    {
        public string id {get;set;}
        public string url {get;set;}
        public int width {get;set;}
        public int height {get;set;}
    }
}