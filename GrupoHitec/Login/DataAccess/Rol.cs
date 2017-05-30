using System;
using System.Collections.Generic;

namespace GrupoHitec.Login.DataAccess
{
    /// <summary>
    /// Clase de acceso a datos de rol (Extiende de la clase DAO).
    /// </summary>
    public class Rol : MSSQLServer.DAO<Entities.Rol>
    {
        /// <summary>
        /// Constructor de la clase rol.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        public Rol(string ConnectionString, string Schema = null)
            : base(ConnectionString, Schema)
        {
        }

        /// <summary>
        /// Método heredado que permite obtener los datos de todos los roles.
        /// </summary>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override MSSQLServer.SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetRoles", null, typeof(Entities.Rol));
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
        /// Método heredado que permite obtener los datos de todos los roles que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override MSSQLServer.SqlCollectionResult GetAllDataByParameters(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetRoleById", parameters, typeof(Entities.Rol));
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
        /// Método que permite obtener los datos de todos los roles que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public MSSQLServer.SqlCollectionResult GetAllDataByUserParameters(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetRoleByUser", parameters, typeof(Entities.Rol));
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
        /// Método que permite obtener los datos de todos los roles que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public MSSQLServer.SqlCollectionResult GetAllDataByName(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetRolesByName", parameters, typeof(Entities.Rol));
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
        /// Método heredado el cuál permite realizar la inserción en la base de datos de acuerdo a las propiedades del rol.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren insertar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Insert(Entities.Rol dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la actualización en la base de datos de acuerdo a las propiedades del rol.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren actualizar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Update(Entities.Rol dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la eliminación en la base de datos de acuerdo a las propiedades del rol.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren para eliminar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Delete(Entities.Rol dataobject)
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
        public MSSQLServer.SqlChangesResult UpdateRolSP(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP(this.schema + ".UpdateRol", parameters);
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
        public MSSQLServer.SqlChangesResult DeleteRolSP(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP(this.schema + ".DeleteRol", parameters);
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
