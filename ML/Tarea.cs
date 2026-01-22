using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Tarea
    {
        public int n_IdTarea { get; set; }
        public string c_Titulo { get; set; }
        public string c_Descripcion { get; set; }

        // Aquí hacemos la conexión con el otro modelo
        // Esto te permitirá acceder a Tarea.Estatus.n_IdEstatus o Tarea.Estatus.c_Nombre
        public ML.Estatus Estatus { get; set; }

        public bool b_Habilitado { get; set; }

        // Constructor opcional para inicializar Estatus y evitar NullReferenceException
        public Tarea()
        {
            Estatus = new ML.Estatus();
        }
    }
}
