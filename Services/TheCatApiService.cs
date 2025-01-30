using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DesafioApi.Interfaces;
using DesafioApi.Models;
using DesafioApi.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

namespace DesafioApi.Services
{
    public class TheCatApiService : ITheApiCatService
    {
        private readonly HttpClient httpClient;
        private const string BaseUrl = "https://api.thecatapi.com/v1/";
        private const string ApiKey = "live_KGHTD3yP7JrU4qTKq8yspNUEMD2QA5TcEgoH6iltOfVfufh1pR9KySOC2xqz4NYG";
        private readonly IRacaRepository repository;

        public TheCatApiService(HttpClient httpClient, IRacaRepository repository)
        {
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            this.repository = repository;
        }

        public List<Raca> ListarEArmazenarRacasNoBanco()
        {
            HttpResponseMessage response = httpClient.GetAsync($"{BaseUrl}breeds").Result;

            if(response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                List<Raca> racas = JsonConvert.DeserializeObject<List<Raca>>(jsonString);

                List<Raca> listaRetorno = new List<Raca>();
                foreach(Raca r in racas)
                {
                    Raca racaExiste = repository.VerificarSeRacaExistePorId(r.id);
                    if(racaExiste == null)
                    {
                        repository.AdicionarRaca(r);
                        listaRetorno.Add(r);
                    }
                }

                if(listaRetorno.Count == 0)
                    throw new Exception("As raças já estão atualizadas no banco de dados");
                
                return listaRetorno;
            }
            else
            {
                throw new Exception("Não foi possível acessar a API Externa");
            }
        }

        public ImagemGato BuscarImagemPorRaca(string racaId)
        {
            HttpResponseMessage response = httpClient.GetAsync($"{BaseUrl}images/search?breed_ids={racaId}").Result;

            if(response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                List<ImagemGato> img = JsonConvert.DeserializeObject<List<ImagemGato>>(jsonString);

                foreach (var imagem in img)
                {
                    Console.WriteLine($"ID: {imagem.id}, URL: {imagem.url}, Width: {imagem.width}, Height: {imagem.height}");
                }
                
                return img[0];
            }
            else
            {
                throw new Exception("Não foi possível acessar a API Externa");
            }
        }
    }
}