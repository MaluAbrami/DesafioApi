using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.Data;
using DesafioApi.Interfaces;
using DesafioApi.Models;

namespace DesafioApi.Repository
{
    public class RacaRepository : IRacaRepository
    {
        private readonly ApplicationDbContext database;

        public RacaRepository(ApplicationDbContext database)
        {
            this.database = database;
        }

        public Raca VerificarSeRacaExistePorId(string id)
        {
            Raca raca = database.Racas.FirstOrDefault(r => r.id == id);
            return raca;
        }

        public Raca VerificarSeRacaExistePorNome(string raca)
        {
            return database.Racas.FirstOrDefault(r => r.name == raca);
        }

        public void AdicionarRaca(Raca raca)
        {
            database.Racas.Add(raca);
            database.SaveChanges();
        }

        public List<Raca> ListarRacas()
        {
            return database.Racas.ToList();
        }
    }
}