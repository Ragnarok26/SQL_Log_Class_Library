using System;
using System.Collections.Generic;

namespace GrupoHitec.Login.DataAccess
{
    /// <summary>
    /// Clase de acceso a datos de compañía (Extiende de la clase DAO).
    /// </summary>
    public class Compania : MSSQLServer.DAO<Entities.Compania>
    {
        /// <summary>
        /// Constructor de la clase compañía.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        public Compania(string ConnectionString, string Schema = null)
            : base(ConnectionString, Schema)
        {
        }

        /// <summary>
        /// Método heredado que permite obtener los datos de todas las compañías.
        /// </summary>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override MSSQLServer.SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetCompanies", null, typeof(Entities.Compania));
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
        /// Método heredado que permite obtener los datos de todas las compañías que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override MSSQLServer.SqlCollectionResult GetAllDataByParameters(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetCompanyById", parameters, typeof(Entities.Compania));
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
        /// Método que permite obtener los datos de todas las compañías que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public MSSQLServer.SqlCollectionResult GetAllDataByName(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetCompaniesByName", parameters, typeof(Entities.Compania));
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
        /// Método heredado el cuál permite realizar la inserción en la base de datos de acuerdo a las propiedades de la compañía.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren insertar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Insert(Entities.Compania dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la actualización en la base de datos de acuerdo a las propiedades de la compañía.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren actualizar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Update(Entities.Compania dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la eliminación en la base de datos de acuerdo a las propiedades de la compañía.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren para eliminar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Delete(Entities.Compania dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método el cuál permite realizar la actualización en la base de datos de acuerdo a los parametros especificados.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public MSSQLServer.SqlChangesResult UpdateCompanySP(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP(this.schema + ".UpdateCompany", parameters);
            }
            catch (Exception ex)
            {
                return new MSSQLServer.SqlChangesResult()
                {
                    RowsAffected = null,
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
        /// Método el cuál permite realizar la eliminación en la base de datos de acuerdo a los parametros especificados.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public MSSQLServer.SqlChangesResult DeleteCompanySP(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP(this.schema + ".DeleteCompany", parameters);
            }
            catch (Exception ex)
            {
                return new MSSQLServer.SqlChangesResult()
                {
                    RowsAffected = null,
                    HasError = true,
                    Message = "Error: No se estableció la conexión (" + ex.Message + ")."
                };
            }
            finally
            {
                connection = null;
            }
        }
    }
}
