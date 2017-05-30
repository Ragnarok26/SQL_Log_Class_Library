using System;
using System.Collections.Generic;

namespace GrupoHitec.Login.DataAccess
{
    /// <summary>
    /// Clase de acceso a datos de usuario (Extiende de la clase DAO).
    /// </summary>
    public class Usuario : MSSQLServer.DAO<Entities.Usuario>
    {
        /// <summary>
        /// Constructor de la clase usuario.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        public Usuario(string ConnectionString, string Schema = null)
            : base(ConnectionString, Schema)
        {
        }

        /// <summary>
        /// Método heredado que permite obtener los datos de todos los usuarios.
        /// </summary>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override MSSQLServer.SqlCollectionResult GetAllData()
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetUsers", null, typeof(Entities.Usuario));
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
        /// Método heredado que permite obtener los datos de todos los usuarios que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public override MSSQLServer.SqlCollectionResult GetAllDataByParameters(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetUserByCredential", parameters, typeof(Entities.Usuario));
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
        /// Método que permite obtener los datos de todos los usuarios que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public MSSQLServer.SqlCollectionResult GetAllDataByName(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetUsersByName", parameters, typeof(Entities.Usuario));
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
        /// Método que permite obtener los datos de todos los usuarios que cumplan con los parámetros establecidos.
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Devuelve una instancia de la clase SqlCollectionResult.</returns>
        public MSSQLServer.SqlCollectionResult GetAllDataById(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.GetDataFromSP(this.schema + ".GetUserById", parameters, typeof(Entities.Usuario));
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
        /// Método heredado el cuál permite realizar la inserción en la base de datos de acuerdo a las propiedades del usuario.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren insertar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Insert(Entities.Usuario dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la actualización en la base de datos de acuerdo a las propiedades del usuario.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren actualizar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Update(Entities.Usuario dataObject)
        {
            return new MSSQLServer.SqlChangesResult()
            {
                RowsAffected = null,
                HasError = true,
                Message = "No está permitida la operación."
            };
        }

        /// <summary>
        /// Método heredado el cuál permite realizar la eliminación en la base de datos de acuerdo a las propiedades del usuario.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren para eliminar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public override MSSQLServer.SqlChangesResult Delete(Entities.Usuario dataObject)
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
        public MSSQLServer.SqlChangesResult UpdateUserSP(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                //return connection.ApplyChangesFromSP("UpdateUser", parameters);
                return connection.ApplyChangesWithValueFromSP(this.schema + ".UpdateUser", parameters);
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
        public MSSQLServer.SqlChangesResult DeleteUserSP(List<MSSQLServer.SqlParam> parameters = null)
        {
            try
            {
                return connection.ApplyChangesFromSP(this.schema + ".DeleteUser", parameters);
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
