using Microsoft.AspNetCore.Mvc;
using Models;

namespace apiUsuarios.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonaController : ControllerBase
{
    public List<Persona> ListaPersonas { get; set; }
    private readonly ILogger<WeatherForecastController> _logger;

    public PersonaController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        ListaPersonas = new List<Persona>();

        for (int i = 0; i < 10; i++)
        {
            Persona aux = new Persona
            {
                Id = i,
                Nombre = "Marcos" + i,
                Apellido = "Gual" + i,
                FechaNacimiento = new DateTime(1987, 1, 5 + i)
            };
            ListaPersonas.Add(aux);
        }
    }

    [HttpGet]
    public List<Persona> Get()
    {
        return ListaPersonas;
    }
}