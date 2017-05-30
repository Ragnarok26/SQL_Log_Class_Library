using System.Collections.Generic;
using System.Linq;

namespace GrupoHitec.Login.Business
{
    /// <summary>
    /// Clase de lógica de negocio de rol.
    /// </summary>
    public class Rol
    {
        /// <summary>
        /// Método estático que permite obtener el listado de todos los roles.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public static MSSQLServer.SqlCollectionResult GetRoles(string ConnectionString, string Schema = null)
        {
            DataAccess.Rol rol = null;
            try
            {
                rol = new DataAccess.Rol(ConnectionString, Schema);
                return rol.GetAllData();
            }
            finally
            {
                rol = null;
            }
        }

        /// <summary>
        /// Método estático que permite obtener el listado de roles por identificador.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="RolId">Identificador numérico del rol que se desea obtener.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public static MSSQLServer.SqlCollectionResult GetRolPorId(string ConnectionString, long RolId, string Schema = null)
        {
            DataAccess.Rol rol = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@RoleId",
                    Type = "BigInt",
                    Value = RolId,
                }
            );
            try
            {
                rol = new DataAccess.Rol(ConnectionString, Schema);
                return rol.GetAllDataByParameters(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }

        /// <summary>
        /// Método estático que permite obtener el listado de roles por identificador de usuario.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="UserId">Identificador numérico del usuario del cual se desea obtener el rol.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public static MSSQLServer.SqlCollectionResult GetRolPorUsuario(string ConnectionString, long UserId, string Schema = null)
        {
            DataAccess.Rol rol = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@UserId",
                    Type = "BigInt",
                    Value = UserId,
                }
            );
            try
            {
                rol = new DataAccess.Rol(ConnectionString, Schema);
                return rol.GetAllDataByUserParameters(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }

        /// <summary>
        /// Método estático que permite obtener el listado de roles por nombre.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="name">Nombre del rol que se desea obtener.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public static MSSQLServer.SqlCollectionResult GetRolesPorNombre(string ConnectionString, string name, string Schema = null)
        {
            DataAccess.Rol rol = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@name",
                    Type = "NVarChar",
                    Value = name,
                }
            );
            try
            {
                rol = new DataAccess.Rol(ConnectionString, Schema);
                return rol.GetAllDataByName(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }

        /// <summary>
        /// Método estático el cuál permite realizar la actualización en la base de datos de un rol.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="Id">Identificador numérico del rol.</param>
        /// <param name="name">Nombre del rol.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public static MSSQLServer.SqlChangesResult UpdateRol(string ConnectionString, long Id, string name, string Schema = null)
        {
            DataAccess.Rol rol = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = Id,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@name",
                    Type = "NVarChar",
                    Value = name,
                }
            );
            try
            {
                rol = new DataAccess.Rol(ConnectionString, Schema);
                return rol.UpdateRolSP(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }

        /// <summary>
        /// Método estático el cuál permite realizar la eliminación en la base de datos de un rol.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="Id">Identificador numérico del rol.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public static MSSQLServer.SqlChangesResult RemoveRol(string ConnectionString, long Id, string Schema = null)
        {
            DataAccess.Rol rol = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = Id,
                }
            );
            try
            {
                rol = new DataAccess.Rol(ConnectionString, Schema);
                return rol.DeleteRolSP(parameters);
            }
            finally
            {
                rol = null;
                parameters = null;
            }
        }
    }
}
