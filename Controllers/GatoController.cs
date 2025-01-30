using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.DTO;
using DesafioApi.Interfaces;
using DesafioApi.Models;
using DesafioApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DesafioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatoController : Controller
    {
        private readonly IGatoService service;

        public GatoController(IGatoService service)
        {
            this.service = service;
        }

        [HttpPost("adicionarGato")]
        public IActionResult Adicionar(GatoDTO gatoDto)
        {
            try
            {
                Gato gato = service.AdicionarGato(gatoDto);
                
                return Created("Gato adicionado com sucesso", gato);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar gato: {ex.Message}");
            }
        }

        [HttpGet("listarGatos")]
        public IActionResult Listar()
        {
            try
            {
                List<GatoResponseDTO> gatos = service.ListarGatos();
                if(gatos == null)
                    return Ok("Nenhum gato cadastrado");

                return Ok(gatos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar gatos cadastrados: {ex.Message}");
            }
        }

        [HttpGet("listarGatosPorRaca/{nomeRaca}")]
        public IActionResult ListarPorRaca(string nomeRaca)
        {
            try
            {
                List<GatoResponseDTO> gatos = service.ListarGatosPorRaca(nomeRaca);
                if(gatos == null)
                    return Ok("Não há gatos cadastrados dessa raça");
                
                return Ok(new {mensagem = $"Gatos cadastrados da raça {nomeRaca}", gatos});
            }   
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar gatos cadastrados por raça: {ex.Message}");
            }
        }

        [HttpGet("buscarGato/{id}")]
        public IActionResult Buscar(int id)
        {
            try
            {
                GatoResponseDTO gato = service.BuscarGatoPorId(id);
                if(gato == null)
                    return NotFound("Gato não encontrado");

                return Ok(new {mensagem = "Gato encontrado com sucesso", gato});
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar buscar gato: {ex.Message}");
            }
        }

        [HttpPut("atualizarGato/{id}")]
        public IActionResult Atualizar([FromBody]GatoDTO gatoDTO, int id)
        {
            try
            {
                Gato gato = service.AtualizarGato(gatoDTO, id);
                if(gato == null)
                    return NotFound("Gato não encontrado");

                return Ok(new {mensagem = "Gato encontrado com sucesso", gato});
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar o gato: {ex.Message}");
            }
        }

        [HttpDelete("deletarGato/{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                GatoResponseDTO gato = service.DeletarGato(id);
                if(gato == null)
                    return NotFound("Gato não encontrado");

                return Ok(new {mensagem = "Gato deletado com sucesso", gato});
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar gato: {ex.Message}");
            }
        }

        [HttpDelete("deletarTodosOsGatos")]
        public IActionResult Deletar()
        {
            try
            {
                List<Gato> gatos = service.DeletarTodosOsGatos();
                return Ok(new {mensagem = "Gatos deletados com sucesso", gatos});
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar todos os gatos: {ex.Message}");
            }
        }
    }
}