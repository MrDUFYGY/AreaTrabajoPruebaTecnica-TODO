# ToDo API - Backend (.NET Framework 4.8)

API RESTful desarrollada en **ASP.NET Web API 2 (.NET Framework 4.8)** utilizando una arquitectura multicapas para la gestión de tareas.

## 📋 Características

* **Arquitectura N-Capas:**
    * **SL (Service Layer):** Controladores de API (`ApiController`).
    * **BL (Business Logic):** Lógica de negocio y orquestación.
    * **DL (Data Layer):** Acceso a datos con Entity Framework (Database First).
    * **ML (Model Layer):** Objetos de transferencia de datos (POCOs).
* **Persistencia:** SQL Server (LocalDB) mediante Stored Procedures.
* **Seguridad:** CORS habilitado para permitir peticiones desde clientes externos (Astro).
* **Formato de Respuesta:** JSON estandarizado (`Correct`, `ErrorMessage`, `Object`, `Objects`).

## 🛠 Requisitos Previos

* Visual Studio 2019 o 2022.
* .NET Framework 4.8 SDK.
* SQL Server Express o LocalDB.

## 🚀 Configuración de la Base de Datos

Antes de iniciar la API, ejecuta el siguiente script en SQL Server Management Studio (SSMS):

1.  Crear base de datos `ToDo`.
2.  Ejecutar el script de tablas y procedimientos almacenados (ubicado en `Docs/DatabaseScript.sql` o usa el siguiente resumen):

```sql
-- Tablas
CREATE TABLE Cat_Estatus (n_IdEstatus INT IDENTITY(1,1), c_Nombre NVARCHAR(50));
CREATE TABLE Op_Tareas (n_IdTarea INT IDENTITY(1,1), c_Titulo NVARCHAR(150), ...);

-- Stored Procedures Clave
EXEC RCSP_AddTarea ...
EXEC RCSP_GetTareaPendiComple ...
EXEC RCSP_UpdateEstatusTarea ...
EXEC RCSP_DeleteTareas ...
