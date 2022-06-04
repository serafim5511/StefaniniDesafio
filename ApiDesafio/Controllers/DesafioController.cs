using ApiDesafio.Models;
using Domain.Interfaces;
using EntitiesDesafio;
using Microsoft.AspNetCore.Mvc;

namespace ApiDesafio.Controllers
{
    public class DesafioController : Controller
    {
        private readonly IPessoa _pessoa;

        public DesafioController(IPessoa Pessoa)
        {
            _pessoa = Pessoa;
        }

        [HttpPost("/api/Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] Pessoa pessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                await _pessoa.Add(pessoa);

                return Ok(new ApiResposta<DateTime>(DateTime.Now, true, 200, "Ok"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResposta<DateTime>(DateTime.Now, false, 400, "Ocorreu um erro"));
            }
        }
        [HttpGet("/api/BuscarLista")]
        public async Task<IActionResult> BuscarLista()
        {     
            try
            {
                return Ok(await _pessoa.List());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResposta<DateTime>(DateTime.Now, false, 400, "Ocorreu um erro"));
            }
        }
        [HttpGet("/api/BuscarMotorista")]
        public async Task<IActionResult> BuscarMotorista(int id)
        {
            try
            {
                return Ok(await _pessoa.GetEntityById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResposta<DateTime>(DateTime.Now, false, 400, "Ocorreu um erro"));
            }
        }
        [HttpPost("/api/Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] Pessoa motorista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _pessoa.Update(motorista);

                return Ok(new ApiResposta<DateTime>(DateTime.Now, true, 200, "Ok"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResposta<DateTime>(DateTime.Now, false, 400, "Ocorreu um erro"));
            }
        }
        [HttpDelete("/api/Delete")]
        public async Task<IActionResult> Delete([FromBody] Pessoa motorista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _pessoa.Delete(motorista);

                return Ok(new ApiResposta<DateTime>(DateTime.Now, true, 200, "Ok"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResposta<DateTime>(DateTime.Now, false, 400, "Ocorreu um erro"));
            }
        }
    }
}
