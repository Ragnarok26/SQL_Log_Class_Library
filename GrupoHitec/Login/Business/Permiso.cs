using System;
using System.Collections.Generic;
using System.Linq;

namespace GrupoHitec.Login.Business
{
    /// <summary>
    /// Clase de lógica de negocio de permiso.
    /// </summary>
    public class Permiso
    {
        /// <summary>
        /// Método estático que permite obtener el listado de permisos otorgados al usuario.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="app">Nombre de la aplicación en la cual se tienen asociados los permisos.</param>
        /// <param name="usuario">Instancia de la clase usuario que contiene los parámetros necesarios para obtener los permisos.</param>
        /// <returns>Retorna un listado de los permisos otorgados al usuario.</returns>
        public static List<Entities.Permiso> ObtenerPermisos(string ConnectionString, string app, Entities.Usuario usuario, string Schema = null)
        {
            DataAccess.Permiso permiso = null;
            MSSQLServer.SqlCollectionResult permisoResult = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@username",
                    Type = "NVarChar",
                    Value = usuario.UserName,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@password",
                    Type = "NVarChar",
                    Value = usuario.Password,
                }
            );
            try
            {
                permiso = new DataAccess.Permiso(ConnectionString, Schema);
                permisoResult = permiso.GetAllDataByParameters(parameters);
                if (!permisoResult.HasError)
                {
                    return (List<Entities.Permiso>)permisoResult.Collection;
                }
                else
                {
                    return new List<Entities.Permiso>();
                }
            }
            finally
            {
                permiso = null;
                usuario = null;
                parameters = null;
                permisoResult = null;
            }
        }
    }
}
