using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.DTO;
using DesafioApi.Interfaces;
using DesafioApi.Models;
using DesafioApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DesafioApi.Services
{
    public class GatoService : IGatoService
    {
        private readonly IGatoRepository gatoRepository;
        private readonly IRacaRepository racaRepository;
        private readonly ITheApiCatService theCatApiService;

        public GatoService(IGatoRepository gatoRepository, IRacaRepository racaRepository, ITheApiCatService theCatApiService)
        {
            this.gatoRepository = gatoRepository;
            this.racaRepository = racaRepository;
            this.theCatApiService = theCatApiService;
        }

        public Gato AdicionarGato(GatoDTO gatoDto)
        {
            if(gatoDto.Nome.Length < 3)
                throw new Exception("O nome do gato deve conter pelo menos 3 caracteres");
            if(gatoDto.Raca.Length == 0)
                throw new Exception("A raça não pode ser vazia");
            if(gatoDto.Descricao.Length == 0)
                throw new Exception("A descrição não pode ser vazia");

            Raca racaExiste = racaRepository.VerificarSeRacaExistePorNome(gatoDto.Raca);
            if(racaExiste != null)
            {
                Gato gato = new Gato();
                gato.Nome = gatoDto.Nome;
                gato.NomeRaca = gatoDto.Raca;
                gato.Descricao = gatoDto.Descricao;
                gato.raca = racaExiste;

                try
                {
                    ImagemGato imagem = theCatApiService.BuscarImagemPorRaca(racaExiste.id);
                    gato.UrlImagem = imagem.url ?? throw new Exception("URL da imagem não encontrada");
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar a imagem do gato", ex);
                }

                gatoRepository.AdicionarGato(gato);
                return gato;
            }
            else 
                throw new Exception("A raça informada não existe");
        }

        public List<GatoResponseDTO> ListarGatos()
        {
            List<Gato> gatos = gatoRepository.ListarGatos();
            
            if(gatos.Count == 0)
                return null;

            List<GatoResponseDTO> gatoResponseDTOs = new List<GatoResponseDTO>();
            foreach(Gato g in gatos)
            {
                GatoResponseDTO gatoResponseDTO = new GatoResponseDTO();
                gatoResponseDTO.Id = g.Id;
                gatoResponseDTO.Nome = g.Nome;
                gatoResponseDTO.Descricao = g.Descricao;
                gatoResponseDTO.UrlImagem = g.UrlImagem;

                Raca raca = racaRepository.VerificarSeRacaExistePorNome(g.NomeRaca);

                gatoResponseDTO.Raca = new RacaResponseDTO
                {
                    Id = raca.id,
                    Nome = raca.name,
                    Descricao = raca.description,
                    Origem = raca.origin
                };

                gatoResponseDTOs.Add(gatoResponseDTO);
            }
            
            return gatoResponseDTOs;
        }

        public List<GatoResponseDTO> ListarGatosPorRaca(string nomeRaca)
        {
            Raca raca = racaRepository.VerificarSeRacaExistePorNome(nomeRaca);
            if(raca == null)
                throw new Exception("Raça informada não existe");

            List<Gato> gatosExistentes = gatoRepository.ListarGatos();

            if(gatosExistentes.Count == 0)
                throw new Exception("Não há gatos cadastrados");

            List<GatoResponseDTO> gatosDaRaca = new List<GatoResponseDTO>();

            foreach(Gato g in gatosExistentes)
            {
                if(g.NomeRaca == nomeRaca)
                {
                    GatoResponseDTO gatoResponseDTO = new GatoResponseDTO();
                    gatoResponseDTO.Id = g.Id;
                    gatoResponseDTO.Nome = g.Nome;
                    gatoResponseDTO.Descricao = g.Descricao;
                    gatoResponseDTO.UrlImagem = g.UrlImagem;

                    gatoResponseDTO.Raca = new RacaResponseDTO
                    {
                        Id = raca.id,
                        Nome = raca.name,
                        Descricao = raca.description,
                        Origem = raca.origin
                    };

                    gatosDaRaca.Add(gatoResponseDTO);
                }
            }

            if(gatosDaRaca.Count == 0)
                return null;
            
            return gatosDaRaca;
        }

        public GatoResponseDTO BuscarGatoPorId(int id)
        {
            Gato gato = gatoRepository.BuscarGatoPorId(id);

            if(gato == null)
                return null;

            GatoResponseDTO gatoResponseDTO = new GatoResponseDTO();
            gatoResponseDTO.Id = gato.Id;
            gatoResponseDTO.Nome = gato.Nome;
            gatoResponseDTO.Descricao = gato.Descricao;
            gatoResponseDTO.UrlImagem = gato.UrlImagem;

            Raca raca = racaRepository.VerificarSeRacaExistePorNome(gato.NomeRaca);

            gatoResponseDTO.Raca = new RacaResponseDTO
            {
                Id = raca.id,
                Nome = raca.name,
                Descricao = raca.description,
                Origem = raca.origin
            };

            return gatoResponseDTO;
        }

        public Gato AtualizarGato(GatoDTO gatoDto, int id)
        {
            Gato gato = gatoRepository.BuscarGatoPorId(id);
            if(gato == null)
                return null;
            
            if(gatoDto.Nome.Length < 3)
                throw new Exception("O nome do gato deve conter pelo menos 3 caracteres");
            if(gatoDto.Raca.Length == 0)
                throw new Exception("A raça não pode ser vazia");
            if(gatoDto.Descricao.Length == 0)
                throw new Exception("A descrição não pode ser vazia");

            Raca racaExiste = racaRepository.VerificarSeRacaExistePorNome(gatoDto.Raca);
            if(racaExiste != null)
            {
                gato.Nome = gatoDto.Nome;
                gato.NomeRaca = gatoDto.Raca;
                gato.Descricao = gatoDto.Descricao;
                gato.raca = racaExiste;

                try
                {
                    ImagemGato imagem = theCatApiService.BuscarImagemPorRaca(racaExiste.id);
                    //Esse operador verifica se a expressão à esquerda é nula. Se for, ele retorna o valor à direita. Caso contrário, ele retorna o valor à esquerda
                    gato.UrlImagem = imagem.url ?? throw new Exception("URL da imagem não encontrada");
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar a imagem do gato", ex);
                }

                gatoRepository.AtualizarGato(gato);
                return gato;
            }
            else
                throw new Exception("A raça informada não existe");
        }

        public GatoResponseDTO DeletarGato(int id)
        {
            Gato gato = gatoRepository.BuscarGatoPorId(id);
            if(gato == null)
                return null;

            GatoResponseDTO gatoResponseDTO = new GatoResponseDTO();
            gatoResponseDTO.Id = gato.Id;
            gatoResponseDTO.Nome = gato.Nome;
            gatoResponseDTO.Descricao = gato.Descricao;
            gatoResponseDTO.UrlImagem = gato.UrlImagem;

            Raca raca = racaRepository.VerificarSeRacaExistePorNome(gato.NomeRaca);

            gatoResponseDTO.Raca = new RacaResponseDTO
            {
                Id = raca.id,
                Nome = raca.name,
                Descricao = raca.description,
                Origem = raca.origin
            };

            gatoRepository.DeletarGato(gato);
            return gatoResponseDTO;
        }

        public List<Gato> DeletarTodosOsGatos()
        {
            List<Gato> gatos = gatoRepository.ListarGatos();

            if(gatos.Count == 0)
                throw new Exception("Não há nenhum gato cadastrado para deletar");

            foreach(Gato g in gatos)
            {
                GatoResponseDTO gatoResponseDTO = new GatoResponseDTO();
                gatoResponseDTO.Id = g.Id;
                gatoResponseDTO.Nome = g.Nome;
                gatoResponseDTO.Descricao = g.Descricao;
                gatoResponseDTO.UrlImagem = g.UrlImagem;

                Raca raca = racaRepository.VerificarSeRacaExistePorNome(g.NomeRaca);

                gatoResponseDTO.Raca = new RacaResponseDTO
                {
                    Id = raca.id,
                    Nome = raca.name,
                    Descricao = raca.description,
                    Origem = raca.origin
                };

                gatoRepository.DeletarGato(g);
            }

            return gatos;
        }
    }
}