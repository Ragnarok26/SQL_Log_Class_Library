using System;
using System.Collections.Generic;

namespace GrupoHitec.Login.DataAccess
{
    /// <summary>
    /// Clase de acceso a datos de permiso (Extiende de la clase DAO).
    /// </summary>
    public class Permiso : MSSQLServer.DAO<Entities.Permiso>
    {
        /// <summary>
        /// Constructor de la clase permiso.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        public Permiso(string ConnectionString, string Schema = null)
            : base(ConnectionString, Schema)
        {
        }

        /// <summary>
        /// Método heredado que permite obtener los datos de todos los permisos.
        /// </summary>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override MSSQLServer.SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetPermissions", null, typeof(Entities.Permiso));
            }
            catch (Exception ex)
            {
                return new MSSQLServer.SqlCollectionResult()
                {
                    Collection = null,
                    HasError = true,
                    Message = "Error: No se estableció la conexión (" + ex.Message + ")."
                };
            }
            finally
            {
                connection = null;
            }
        }

        /// <summary>
        /// Método heredado que permite obtener los datos de todos los permisos que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override MSSQLServer.SqlCollectionResult GetAllDataByParameters(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetPermissionsByUserId", parameters, typeof(Entities.Permiso));
            }
            catch (Exception ex)
            {
                return new MSSQLServer.SqlCollectionResult()
                {
                    Collection = null,
                    HasError = true,
                    Message = "Error: No se estableció la conexión (" + ex.Message + ")."
                };
            }
            finally
            {
                connection = null;
            }
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la inserción en la base de datos de acuerdo a las propiedades del permiso.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren insertar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Insert(Entities.Permiso dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la actualización en la base de datos de acuerdo a las propiedades del permiso.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren actualizar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Update(Entities.Permiso dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la eliminación en la base de datos de acuerdo a las propiedades del permiso.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren para eliminar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Delete(Entities.Permiso dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }
    }
}
