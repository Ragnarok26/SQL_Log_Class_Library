namespace GrupoHitec.MSSQLServer
{
    /// <summary>
    /// Permite configurar un parámetro SQL.
    /// </summary>
    public class SqlParam
    {
        /// <summary>
        /// Nombre del parámetro de la consulta SQL.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Tipo de dato SQL del parámetro.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// En caso de que sea un parámetro personalizado en SQL, se debe de especificar el nombre con el que se creó en el manejador.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// Indica si el parámetro SQL es de entrada, salida o ambas.
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// Valor que será asignado al parámetro SQL.
        /// </summary>
        public object Value { get; set; }
    }
}
