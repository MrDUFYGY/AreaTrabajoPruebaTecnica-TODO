using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http; // Importante: Cambiamos MVC por Web.Http
using BL;
using ML;

namespace SL_API_TODO.Controllers
{
    // Definimos un prefijo de ruta para que la URL sea limpia: api/todo
    [RoutePrefix("api/todo")]
    public class ToDoController : ApiController
    {
        // 1. OBTENER (GET): api/todo o api/todo?idEstatus=1
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll(int? idEstatus = null)
        {
            // Consumimos el método Get de la BL
            ML.Result result = BL.Tarea.Get(idEstatus);

            if (result.Correct)
            {
                return Ok(result); // Retorna JSON con status 200
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result); // Retorna 404 si falla
            }
        }


        // 2. CREAR (POST): api/todo
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] ML.Tarea tarea)
        {
            // Validación básica requerida en instrucciones (Título obligatorio)
            if (tarea == null || string.IsNullOrEmpty(tarea.c_Titulo))
            {
                return BadRequest("El título es obligatorio.");
            }

            ML.Result result = BL.Tarea.Add(tarea);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return InternalServerError(result.Ex); // Retorna 500 si hay error de servidor
            }
        }

        // 3. ACTUALIZAR ESTATUS (PUT): api/todo/{id}/estatus/{nuevoId}
        // Ejemplo: api/todo/5/estatus/2 (Para marcar la tarea 5 como completada)
        [HttpPut]
        [Route("{idTarea:int}/estatus/{nuevoIdEstatus:int}")]
        public IHttpActionResult UpdateEstatus(int idTarea, int nuevoIdEstatus)
        {
            ML.Result result = BL.Tarea.UpdateEstatus(idTarea, nuevoIdEstatus);

            if (result.Correct)
            {
                return Ok(new { Mensaje = "Estatus actualizado correctamente", Data = result });
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        // 4. ELIMINAR (DELETE): api/todo/{id}
        [HttpDelete]
        [Route("{idTarea:int}")]
        public IHttpActionResult Delete(int idTarea)
        {
            ML.Result result = BL.Tarea.Delete(idTarea);

            if (result.Correct)
            {
                return Ok(new { Mensaje = "Tarea eliminada correctamente" });
            }
            else
            {
                return BadRequest("No se pudo eliminar la tarea.");
            }
        }
    }
}