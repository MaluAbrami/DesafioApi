using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.Data;
using DesafioApi.Interfaces;
using DesafioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Repository
{
    public class GatoRepository : IGatoRepository
    {
        private readonly ApplicationDbContext database;

        public GatoRepository(ApplicationDbContext database)
        {
            this.database = database;
        }

        public void AdicionarGato(Gato gato)
        {
            database.Gatos.Add(gato);
            database.SaveChanges();
        }

        public List<Gato> ListarGatos()
        {
            return database.Gatos.ToList();
        }

        public Gato BuscarGatoPorId(int id)
        {
            return database.Gatos.FirstOrDefault(g => g.Id == id);
        }

        public Gato AtualizarGato(Gato gato)
        {
            database.Gatos.Update(gato);
            database.SaveChanges();
            return gato;
        }

        public void DeletarGato(Gato gato)
        {
            database.Gatos.Remove(gato);
            database.SaveChanges();
        }
    }
}