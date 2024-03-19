using Portafolio.Api.Services;
using Portafolio.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Portafolio.Api.Controllers;

[ApiController]
[Route("Api/[Controller]")]

public class ProyectosController : ControllerBase
{
    private readonly ILogger<ProyectosController> _logger;
    private readonly ProyectosServices _proyectoServices; // Accede a la clase dentro del espacio de nombres

    public ProyectosController(ILogger<ProyectosController> logger,
                             ProyectosServices proyectosServices) // Especifica la ruta completa
    {
        _logger = logger;
        _proyectoServices = proyectosServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetProyectos()
    {
        var proyectos = await _proyectoServices.GetAsync();
        return Ok(proyectos);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetProyectosById(string Id)
    {
        return Ok(await _proyectoServices.GetProyectoById(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProyecto([FromBody] Mproyectos mproyectos)
    {
        if (mproyectos == null)
            return BadRequest();
        if (mproyectos.NombreP == string.Empty)
            ModelState.AddModelError("Nombre", "El nombre no debe estar vacio"); //BELLAKITA

        await _proyectoServices.InsertProyecto(mproyectos);

        return Created("Created", true);
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateProyecto([FromBody] Mproyectos mproyectos, string Id)
    {
        if (mproyectos == null)
            return BadRequest();
        if (mproyectos.NombreP == string.Empty)
            ModelState.AddModelError("Nombre", "El nombre no debe estar vacio");
        mproyectos.Id = Id;

        await _proyectoServices.UpdateProyecto(mproyectos);
        return Created("Created", true);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProyecto(string Id)
    {
        await _proyectoServices.DeleteProyecto(Id);
        return NoContent();
    }
}
