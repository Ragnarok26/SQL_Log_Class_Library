namespace GrupoHitec.MSSQLServer
{
    /// <summary>
    /// Extiende de la Clase SqlResult; contiene el resultado de una operación SQL que retorna la cantidad de filas afectadas.
    /// </summary>
    public class SqlChangesResult : SqlResult
    {
        /// <summary>
        /// Contiene el resultado de la operación SQL.
        /// </summary>
        public int? RowsAffected { get; set; }

        /// <summary>
        /// Constructor de la clase en la cual se inicializan sus propiedades.
        /// </summary>
        public SqlChangesResult()
            : base()
        {
            RowsAffected = null;
        }
    }
}
