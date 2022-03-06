using System.Runtime.CompilerServices;
using Comandos.Personas;
using Comandos.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Models;
using Resultados;

namespace apiUsuarios.Controllers;

[ApiController]
public class UsuarioController : ControllerBase
{
    public List<Usuario> ListaUsuarios { get; set; }

    public UsuarioController()
    {
        ListaUsuarios = new List<Usuario>();
        Usuario usu1 = new Usuario(1, "marcos.1891@outlook.com", "marcos123", true, new DateTime(2020, 5, 5));
        ListaUsuarios.Add(usu1);

        Usuario usu2 = new Usuario(2, "antoabba@gmail.com", "vanina13", false, DateTime.Today);
        ListaUsuarios.Add(usu2);
    }

    [HttpGet]
    [Route("Persona/Usuarios")]
    public ActionResult<ResultadoAPI> Get()
    {
        var resultado = new ResultadoAPI();
        try
        {
            resultado.Ok = true;
            resultado.Return = ListaUsuarios;

            return resultado;
        }
        catch (Exception ex)
        {
            resultado.Ok = false;
            resultado.CodigoError = 1;
            resultado.Error = "Error al obtener usuarios - " + ex.Message;

            return resultado;
        }
    }

    [HttpPost]
    [Route("Usuario/Login")]
    public ActionResult<ResultadoAPI> Login([FromBody] ComandoLoginUsuario comando)
    {
        var resultado = new ResultadoAPI();
        var email = comando.Email.Trim();
        var Password = comando.Password;

        try
        {
            var usu = ListaUsuarios.FirstOrDefault(o => o.Email.Equals(email) && o.Password.Equals(Password));

            if (usu != null)
            {
                if (usu.Activo == false)
                {
                    resultado.Ok = false;
                    resultado.Error = "Usuario bloqueado";

                    return resultado;
                }
                resultado.Ok = true;
                resultado.Return = usu;
            }
            else
            {
                resultado.Ok = false;
                resultado.Error = "Usuario o contraseña incorrectos...";
            }
            return resultado;
        }
        catch (Exception ex)
        {

            return resultado;
        }
    }

    // [HttpGet]
    // [Route("Persona/Obtener/{idUsuario}")]
    // public ActionResult<ResultadoAPI> GetUsuario(int idUsuario)
    // {
    //     var resultado = new ResultadoAPI();
    //     try
    //     {
    //         var per = ListaPersonas.Where(c => c.Id == idUsuario).FirstOrDefault();
    //         resultado.Ok = true;
    //         resultado.Return = per;

    //         return resultado;
    //     }
    //     catch (Exception ex)
    //     {
    //         resultado.Ok = false;
    //         resultado.Error = "Id no encontrado";

    //         return resultado;
    //     }
    // }

    // [HttpPost]
    // [Route("Persona/AltaPersona")]
    // public ActionResult<ResultadoAPI> AltaPersona([FromBody] ComandoCrearPersona comando)
    // {
    //     var resultado = new ResultadoAPI();

    //     if (comando.Nombre.Equals("") || comando.Apellido.Equals(""))
    //     {
    //         resultado.Ok = false;
    //         resultado.Error = "Ingrese nombre y apellido...";
    //         return resultado;
    //     }

    //     var per = new Persona();
    //     per.Nombre = comando.Nombre;
    //     per.Apellido = comando.Apellido;
    //     per.FechaNacimiento = comando.FechaNacimiento;
    //     per.Soltero = comando.Soltero;
    //     per.Id = ListaPersonas.Count;

    //     ListaPersonas.Add(per);
    //     resultado.Ok = true;
    //     resultado.Return = ListaPersonas;

    //     return resultado;
    // }

    // [HttpPut]
    // [Route("Persona/UpdatePersona")]
    // public ActionResult<ResultadoAPI> UpdatePersona([FromBody] ComandoUpdatePersona comando)
    // {
    //     var resultado = new ResultadoAPI();

    //     if (comando.Nombre.Equals("") || comando.Apellido.Equals("") || comando.IdPersona.Equals(null))
    //     {
    //         resultado.Ok = false;
    //         resultado.Error = "Ingrese identificación de la persona, nombre y apellido...";
    //         return resultado;
    //     }

    //     var existe = false;

    //     var per = ListaPersonas.Where(o => o.Id == comando.IdPersona).FirstOrDefault();
    //     if (per != null)
    //     {
    //         per.Nombre = comando.Nombre;
    //         per.Apellido = comando.Apellido;
    //         existe = true;
    //     }
    //     else
    //         existe = false;

    //     resultado.InfoAdicional = existe ? "La persona se ha podido encontrar exitosamente" : "Id no encontrado";
    //     resultado.Ok = true;
    //     resultado.Return = ListaPersonas;

    //     return resultado;
    // }

    // [HttpDelete]
    // [Route("[controller]/#")]
    // public ActionResult<ResultadoAPI> BorrarPersona(int IdPersona)
    // {
    //     var resultado = new ResultadoAPI();

    //     var per = ListaPersonas.Where(o => o.Id == IdPersona).FirstOrDefault();
    //     if (per != null)
    //     {
    //         ListaPersonas.RemoveAt(per.Id);
    //     }
    //     else
    //         resultado.InfoAdicional = "Id no encontrado";
    //     resultado.Ok = true;
    //     resultado.Return = ListaPersonas;

    //     return resultado;
    // }
}