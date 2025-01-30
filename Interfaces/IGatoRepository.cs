using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.Models;

namespace DesafioApi.Interfaces
{
    public interface IGatoRepository
    {
        public void AdicionarGato(Gato gato);
        public List<Gato> ListarGatos();
        public Gato BuscarGatoPorId(int id);
        public Gato AtualizarGato(Gato gato);
        public void DeletarGato(Gato gato);
    }
}