using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.DTO;
using DesafioApi.Models;

namespace DesafioApi.Interfaces
{
    public interface IRacaService
    {
        public List<RacaResponseDTO> ListarRacasDisponiveis();
        public string BuscarIdDaRaca(string nomeRaca);
        public Raca BuscarRacaPorId(string racaId);
    }
}