using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.DTO;
using DesafioApi.Models;

namespace DesafioApi.Interfaces
{
    public interface IGatoService
    {
        public Gato AdicionarGato(GatoDTO gatoDto);
        public List<GatoResponseDTO> ListarGatos();
        public List<GatoResponseDTO> ListarGatosPorRaca(string nomeRaca);
        public GatoResponseDTO BuscarGatoPorId(int id);
        public Gato AtualizarGato(GatoDTO gatoDto, int id);
        public GatoResponseDTO DeletarGato(int id);
        public List<Gato> DeletarTodosOsGatos();
    }
}