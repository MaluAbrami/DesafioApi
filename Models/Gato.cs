using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DesafioApi.Models
{
    public class Gato
    {
        public int Id {get;set;}
        public string Nome {get;set;}
        public string NomeRaca {get;set;}
        public string Descricao {get;set;}
        public string UrlImagem {get;set;}
        public Raca raca {get;set;}
    }
}