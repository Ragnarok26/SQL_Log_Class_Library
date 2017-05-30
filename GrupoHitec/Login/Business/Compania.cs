using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoHitec.Login.Business
{
    /// <summary>
    /// Clase de lógica de negocio de compañía.
    /// </summary>
    public class Compania
    {
        /// <summary>
        /// Método estático que permite obtener el listado de compañías por identificador.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="CompanyId">Identificador numérico de la compañía que se desea obtener.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public static MSSQLServer.SqlCollectionResult GetCompaniaPorId(string ConnectionString, int CompanyId, string Schema = null)
        {
            DataAccess.Compania compania = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@CompanyId",
                    Type = "Int",
                    Value = CompanyId,
                }
            );
            try
            {
                compania = new DataAccess.Compania(ConnectionString, Schema);
                return compania.GetAllDataByParameters(parameters);
            }
            finally
            {
                compania = null;
                parameters = null;
            }
        }

        /// <summary>
        /// Método estático que permite obtener el listado de compañías por nombre.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="name">Nombre de la compañía que se desea obtener.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public static MSSQLServer.SqlCollectionResult GetCompaniasPorNombre(string ConnectionString, string name, string Schema = null)
        {
            DataAccess.Compania compania = null;
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
                compania = new DataAccess.Compania(ConnectionString, Schema);
                return compania.GetAllDataByName(parameters);
            }
            finally
            {
                compania = null;
            }
        }

        /// <summary>
        /// Método estático que permite obtener el listado de todas las compañías.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public static MSSQLServer.SqlCollectionResult GetCompanias(string ConnectionString, string Schema = null)
        {
            DataAccess.Compania compania = null;
            try
            {
                compania = new DataAccess.Compania(ConnectionString, Schema);
                return compania.GetAllData();
            }
            finally
            {
                compania = null;
            }
        }

        /// <summary>
        /// Método estático el cuál permite realizar la actualización en la base de datos de una compañía.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="Id">Identificador numérico de la compañía.</param>
        /// <param name="name">Nombre de la compañía.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public static MSSQLServer.SqlChangesResult UpdateCompania(string ConnectionString, int Id, string name, string Schema = null)
        {
            DataAccess.Compania compania = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@Id",
                    Type = "Int",
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
                compania = new DataAccess.Compania(ConnectionString, Schema);
                return compania.UpdateCompanySP(parameters);
            }
            finally
            {
                compania = null;
                parameters = null;
            }
        }

        /// <summary>
        /// Método estático el cuál permite realizar la eliminación en la base de datos de una compañía.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="Id">Identificador numérico de la compañía.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public static MSSQLServer.SqlChangesResult RemoveCompania(string ConnectionString, int Id, string Schema = null)
        {
            DataAccess.Compania compania = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@Id",
                    Type = "Int",
                    Value = Id,
                }
            );
            try
            {
                compania = new DataAccess.Compania(ConnectionString, Schema);
                return compania.DeleteCompanySP(parameters);
            }
            finally
            {
                compania = null;
                parameters = null;
            }
        }
    }
}
