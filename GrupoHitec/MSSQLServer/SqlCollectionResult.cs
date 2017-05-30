using System.Collections;

namespace GrupoHitec.MSSQLServer
{
    /// <summary>
    /// Extiende de la clase SqlResult; contiene el resultado de una operación SQL que retorna una colección de datos.
    /// </summary>
    public class SqlCollectionResult : SqlResult
    {
        /// <summary>
        /// Contiene el resultado de la operación SQL.
        /// </summary>
        public IEnumerable Collection { get; set; }

        /// <summary>
        /// Constructor de la clase en la cual se inicializan sus propiedades.
        /// </summary>
        public SqlCollectionResult()
            : base()
        {
            Collection = null;
        }
    }
}
