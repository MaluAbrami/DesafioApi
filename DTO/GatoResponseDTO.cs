using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.Models;

namespace DesafioApi.DTO
{
    public class GatoResponseDTO
    {
        public int Id {get;set;}
        public string Nome {get;set;}
        public string Descricao {get;set;}
        public string UrlImagem {get;set;}
        public RacaResponseDTO Raca {get;set;}
    }
}