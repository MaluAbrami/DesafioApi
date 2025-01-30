using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.Models;

namespace DesafioApi.Interfaces
{
    public interface ITheApiCatService
    {
        public List<Raca> ListarEArmazenarRacasNoBanco();
        public ImagemGato BuscarImagemPorRaca(string racaId);
    }
}