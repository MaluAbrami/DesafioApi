using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.DTO;
using DesafioApi.Interfaces;
using DesafioApi.Models;
using DesafioApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RacaController : Controller
    {
        private readonly ITheApiCatService theApiCatService;
        private readonly IRacaService racaService;

        public RacaController(ITheApiCatService theApiCatService, IRacaService racaService)
        {
            this.theApiCatService = theApiCatService;
            this.racaService = racaService;
        }

        [HttpPost("atualizarRacasNoBanco")]
        public IActionResult AtualizarRacasNoBanco()
        {
            try
            {
                theApiCatService.ListarEArmazenarRacasNoBanco();
                return Ok("As raças no banco de dados foram atualizadas!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar raças no banco: {ex.Message}");
            }
        }

        [HttpGet("listarRacasDisponiveis")]
        public IActionResult ListarRacas()
        {
            try
            {
                List<RacaResponseDTO> racas = racaService.ListarRacasDisponiveis();
                return Ok(racas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar listar raças disponiveis no banco de dados {ex.Message}");
            }
        }

        [HttpGet("buscarImagem/{nomeRaca}")]
        public IActionResult BuscarImagem(string nomeRaca)
        {
            try
            {
                string racaId = racaService.BuscarIdDaRaca(nomeRaca);
                ImagemGato img = theApiCatService.BuscarImagemPorRaca(racaId);
                return Ok(img);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar imagem: {ex.Message}");
            }
        }

        [HttpGet("adquirirInformacoesDaRaca/{nomeRaca}")]
        public IActionResult BuscarRaca(string nomeRaca)
        {
            try
            {
                string racaId = racaService.BuscarIdDaRaca(nomeRaca);
                Raca raca = racaService.BuscarRacaPorId(racaId);
                return Ok(raca);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao tentar listar raças disponiveis no banco de dados {ex.Message}");
            }
        }
    }
}