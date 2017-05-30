using GrupoHitec.MSSQLServer;
using System;
using System.Collections.Generic;

namespace GrupoHitec.Login.DataAccess
{
    /// <summary>
    /// Clase de acceso a datos de territorio (Extiende de la clase DAO).
    /// </summary>
    public class Territorio : DAO<Entities.Territorio>
    {
        /// <summary>
        /// Constructor de la clase territorio.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        public Territorio(string ConnectionString, string Schema = null)
            : base(ConnectionString, Schema)
        {
        }

        /// <summary>
        /// Método heredado que permite obtener los datos de todos los territorios.
        /// </summary>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetTerritories", null, typeof(Entities.Territorio));
            }
            catch (Exception ex)
            {
                return new SqlCollectionResult()
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
        /// Método heredado que permite obtener los datos de todos los territorios que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override SqlCollectionResult GetAllDataByParameters(List<SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetTerritoriesByUserId", parameters, typeof(Entities.Territorio));
            }
            catch (Exception ex)
            {
                return new SqlCollectionResult()
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
        /// Método heredado el cuál permite realizar la inserción en la base de datos de acuerdo a las propiedades del territorio.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren insertar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override SqlChangesResult Insert(Entities.Territorio dataObject)
        {
            return new SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la actualización en la base de datos de acuerdo a las propiedades del territorio.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren actualizar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override SqlChangesResult Update(Entities.Territorio dataObject)
        {
            return new SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la eliminación en la base de datos de acuerdo a las propiedades del territorio.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren para eliminar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override SqlChangesResult Delete(Entities.Territorio dataObject)
        {
            return new SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }
    }
}
