namespace GrupoHitec.Login.Entities
{
    /// <summary>
    /// Clase que contiene las propiedades del territorio.
    /// </summary>
    public class Territorio
    {
        /// <summary>
        /// Obtiene o establece el identificador numérico del territorio.
        /// </summary>
        public int TerritoryId { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre descriptivo del territorio.
        /// </summary>
        public string Territory { get; set; }
        /// <summary>
        /// Obtiene o establece la contraseña del usuario asociada al territorio.
        /// </summary>
        public string Empresa { get; set; }
    }
}
