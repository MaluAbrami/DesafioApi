using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.DTO;
using DesafioApi.Interfaces;
using DesafioApi.Models;

namespace DesafioApi.Services
{
    public class RacaService : IRacaService
    {
        private readonly IRacaRepository repository;

        public RacaService(IRacaRepository repository)
        {
            this.repository = repository;
        }

        public List<RacaResponseDTO> ListarRacasDisponiveis()
        {
            List<Raca> racas = repository.ListarRacas();
            
            List<RacaResponseDTO> racasResponse = new List<RacaResponseDTO>();
            foreach(Raca r in racas)
            {
                RacaResponseDTO racaResponseDTO = new RacaResponseDTO();
                racaResponseDTO.Id = r.id;
                racaResponseDTO.Nome = r.name;
                racaResponseDTO.Descricao = r.description;
                racaResponseDTO.Origem = r.origin;
                racasResponse.Add(racaResponseDTO);
            }

            if(racas.Count == 0)
                throw new Exception("Ainda não há raças cadastradas no banco de dados. Atualize suas raças e tente novamente.");

            return racasResponse;
        }

        public string BuscarIdDaRaca(string nomeRaca)
        {
            Raca raca = repository.VerificarSeRacaExistePorNome(nomeRaca);
            if(raca == null)
                throw new Exception("Raça não existe");

            return raca.id;
        }

        public Raca BuscarRacaPorId(string racaId)
        {
            return repository.VerificarSeRacaExistePorId(racaId);
        }
    }
}