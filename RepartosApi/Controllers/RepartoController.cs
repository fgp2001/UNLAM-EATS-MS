using Microsoft.AspNetCore.Mvc;
using RepartosApi.Data.Entidades;
using RepartosApi.Services;

namespace RepartosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepartoController : Controller
    {
        private readonly IRepartoService _repartoService;

        public RepartoController(IRepartoService repartoService)
        {
            _repartoService = repartoService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var repartos = _repartoService.ObtenerRepartos();
            return Ok(repartos);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var reparto =_repartoService.ObtenerRepartoPorId(id);
            if (reparto == null)
            {
                return NotFound();
            }
            return Ok(reparto);

        }


        [HttpPost]
        public IActionResult Create(Reparto reparto)
        {
            _repartoService.AgregarReparto(reparto);
            return CreatedAtAction(nameof(GetById), new { id = reparto.Id }, reparto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Reparto reparto)
        {
            if (id != reparto.Id)
            {
                return BadRequest();
            }
            _repartoService.ActualizarReparto(reparto); 
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repartoService.EliminarReparto(id);
            return NoContent();

        }


        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarRepartidor([FromBody] AsignarRepartidorRequest request)
        {
            var reparto = await (_repartoService as RepartoService)!.AsignarRepartidorAsync(request.IdReparto, request.IdRepartidor);
            if (reparto == null)
            {
                return BadRequest("Reparto o repartidor inválido");
            }
            return Ok(reparto);
        }
    }
}