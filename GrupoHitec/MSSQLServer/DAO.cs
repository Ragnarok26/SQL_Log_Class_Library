using System.Collections.Generic;

namespace GrupoHitec.MSSQLServer
{
    /// <summary>
    /// Clase abstracta la cual es la generalización de las operaciones báscias de SQL.
    /// </summary>
    public abstract class DAO<T>
    {
        /// <summary>
        /// Propiedad de que permite la conexion a SQL.
        /// </summary>
        protected Connection connection;

        /// <summary>
        /// Propiedad que denota el esquema en el que se va a trabajar.
        /// </summary>
        protected string schema;

        /// <summary>
        /// Constructor de la clase el cual inicializa las propiedades de la misma.
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexión con las configuraciones necesarias para conectarse a SQL.</param>
        public DAO(string ConnectionString, string Schema)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                connection = new Connection(ConnectionString);
            }
            else
            {
                connection = null;
            }
            if (!string.IsNullOrEmpty(Schema))
            {
                schema = Schema;
            }
            else
            {
                schema = "dbo";
            }
        }

        /// <summary>
        /// Método abstracto el cuál permite obtener todos los datos deseados.
        /// </summary>
        /// <returns>Retorna una instancia de la clase SqlCollectionResult.</returns>
        public abstract SqlCollectionResult GetAllData();

        /// <summary>
        /// Método abstracto el cuál permite obtener todos los datos deseados en base a los parámetros especificados (si no se especifican los parámetros, el valor por defecto será null).
        /// </summary>
        /// <param name="parameters">Lista de objetos SqlParam el cual contendrá los parámetros indicados en la consulta SQL.</param>
        /// <returns>Retorna una instancia de la clase SqlCollectionResult.</returns>
        public abstract SqlCollectionResult GetAllDataByParameters(List<SqlParam> parameters = null);

        /// <summary>
        /// Método abstracto el cuál permite realizar la inserción en la base de datos de acuerdo a las propiedades de un objeto.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren insertar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public abstract SqlChangesResult Insert(T dataObject);

        /// <summary>
        /// Método abstracto el cuál permite realizar la actualización en la base de datos de acuerdo a las propiedades de un objeto.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren actualizar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public abstract SqlChangesResult Update(T dataObject);

        /// <summary>
        /// Método abstracto el cuál permite realizar la eliminación en la base de datos de acuerdo a las propiedades de un objeto.
        /// </summary>
        /// <param name="dataObject">Objeto que contiene las propiedades que se requieren para eliminar en la base de datos.</param>
        /// <returns>Retorna una instancia de la clase SqlChangesResult.</returns>
        public abstract SqlChangesResult Delete(T dataObject);
    }
}
