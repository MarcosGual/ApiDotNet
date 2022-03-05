using System.Runtime.CompilerServices;
using Comandos.Personas;
using Microsoft.AspNetCore.Mvc;
using Models;
using Resultados;

namespace apiUsuarios.Controllers;

[ApiController]
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
                FechaNacimiento = new DateTime(1987, 1, 5 + i),
                Soltero = true
            };
            ListaPersonas.Add(aux);
        }
    }

    [HttpGet]
    [Route("Persona/ObtenerPersonas")]
    public ActionResult<List<Persona>> Get()
    {
        return ListaPersonas;
    }

    [HttpGet]
    [Route("Persona")]
    public IEnumerable<Persona> GetPersonas()
    {
        return ListaPersonas;
    }

    [HttpGet]
    [Route("Persona/Obtener/{idUsuario}")]
    public ActionResult<ResultadoAPI> GetPersonasId(int idUsuario)
    {
        var resultado = new ResultadoAPI();
        try
        {
            var per = ListaPersonas.Where(c => c.Id == idUsuario).FirstOrDefault();
            resultado.Ok = true;
            resultado.Return = per;

            return resultado;
        }
        catch (Exception ex)
        {
            resultado.Ok = false;
            resultado.Error = "Id no encontrado";

            return resultado;
        }
    }

    [HttpPost]
    [Route("Persona/AltaPersona")]
    public ActionResult<ResultadoAPI> AltaPersona([FromBody] ComandoCrearPersona comando)
    {
        var resultado = new ResultadoAPI();

        if (comando.Nombre.Equals("") || comando.Apellido.Equals(""))
        {
            resultado.Ok = false;
            resultado.Error = "Ingrese nombre y apellido...";
            return resultado;
        }

        var per = new Persona();
        per.Nombre = comando.Nombre;
        per.Apellido = comando.Apellido;
        per.FechaNacimiento = comando.FechaNacimiento;
        per.Soltero = comando.Soltero;
        per.Id = ListaPersonas.Count;

        ListaPersonas.Add(per);
        resultado.Ok = true;
        resultado.Return = ListaPersonas;

        return resultado;
    }

    [HttpPut]
    [Route("Persona/UpdatePersona")]
    public ActionResult<ResultadoAPI> UpdatePersona([FromBody] ComandoUpdatePersona comando)
    {
        var resultado = new ResultadoAPI();

        if (comando.Nombre.Equals("") || comando.Apellido.Equals("") || comando.IdPersona.Equals(null))
        {
            resultado.Ok = false;
            resultado.Error = "Ingrese identificación de la persona, nombre y apellido...";
            return resultado;
        }

        ListaPersonas[comando.IdPersona].Nombre = comando.Nombre;
        ListaPersonas[comando.IdPersona].Apellido = comando.Apellido;

        resultado.Ok = true;
        resultado.Return = ListaPersonas;

        return resultado;
    }
}