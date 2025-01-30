using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.Models;

namespace DesafioApi.Interfaces
{
    public interface IRacaRepository
    {
        public Raca VerificarSeRacaExistePorId(string id);
        public Raca VerificarSeRacaExistePorNome(string raca);
        public void AdicionarRaca(Raca raca);
        public List<Raca> ListarRacas();
    }
}