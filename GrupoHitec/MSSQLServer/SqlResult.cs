namespace GrupoHitec.MSSQLServer
{
    /// <summary>
    /// Clase abstracta la cual es la generalización de un resultado en SQL.
    /// </summary>
    public abstract class SqlResult
    {
        /// <summary>
        /// Indica si el resultado SQL presentó errores.
        /// </summary>
        public bool HasError { get; set; }
        /// <summary>
        /// Contiene el mensaje que se haya presentado del resultado SQL.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Constructor de la clase el cual inicializa las propiedades de la misma.
        /// </summary>
        public SqlResult()
        {
            HasError = false;
            Message = string.Empty;
        }
    }
}
