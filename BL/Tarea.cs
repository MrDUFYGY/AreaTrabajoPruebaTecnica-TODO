using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;
using DL_EF; 

namespace BL
{
    public class Tarea
    {
        // 1. ADD: Crear nueva tarea
        public static ML.Result Add(ML.Tarea tarea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ToDoEntities context = new DL_EF.ToDoEntities())
                {
                    // Ejecutamos el SP: RCSP_AddTarea
                    // El SP devuelve el ID decimal, usamos FirstOrDefault para obtenerlo
                    var query = context.RCSP_AddTarea(tarea.c_Titulo, tarea.c_Descripcion).FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = query; // Aquí viaja el ID de la nueva tarea
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al insertar la tarea.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        // 2. GET: Listar tareas (Todas o por Filtro)
        // Recibe int? para filtrar por ID de estatus (1=Pendiente, 2=Completada, null=Todas)
        public static ML.Result Get(int? idEstatus)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ToDoEntities context = new DL_EF.ToDoEntities())
                {
                    // Ejecutamos el SP: RCSP_GetTareaPendiComple
                    var query = context.RCSP_GetTareaPendiComple(idEstatus).ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var objEF in query)
                        {
                            ML.Tarea tarea = new ML.Tarea();

                            // Mapeo de propiedades simples
                            tarea.n_IdTarea = objEF.n_IdTarea;
                            tarea.c_Titulo = objEF.c_Titulo;
                            tarea.c_Descripcion = objEF.c_Descripcion;

                            // Mapeo de la propiedad compuesta (Objeto Estatus)
                            tarea.Estatus = new ML.Estatus();
                            tarea.Estatus.n_IdEstatus = objEF.n_IdEstatus;

                            // Nota: Asegúrate de que tu modelo complejo en EF tenga la propiedad 'c_NombreEstatus'
                            // tal como la definimos en el SELECT del SP (E.c_Nombre as c_NombreEstatus)
                            tarea.Estatus.c_Nombre = objEF.c_NombreEstatus;

                            // En el SP filtramos b_Habilitado = 1, así que por defecto es true
                            tarea.b_Habilitado = true;

                            result.Objects.Add(tarea);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        // 3. UPDATE STATUS: Actualizar solo el estatus
        public static ML.Result UpdateEstatus(int idTarea, int nuevoIdEstatus)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ToDoEntities context = new DL_EF.ToDoEntities())
                {
                    // Ejecutamos el SP: RCSP_UpdateEstatusTarea
                    // Al ser un UPDATE sin retorno de datos, devuelve el número de filas afectadas
                    int rowsAffected = context.RCSP_UpdateEstatusTarea(idTarea, nuevoIdEstatus);

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el estatus. Verifique que la tarea exista.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        // 4. DELETE: Eliminar tarea (Logico)
        public static ML.Result Delete(int idTarea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.ToDoEntities context = new DL_EF.ToDoEntities())
                {
                    // Ejecutamos el SP: RCSP_DeleteTareas
                    int rowsAffected = context.RCSP_DeleteTareas(idTarea);

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar la tarea.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}