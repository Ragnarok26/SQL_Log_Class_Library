namespace GrupoHitec.Login.Entities
{
    /// <summary>
    /// Clase que contiene las propiedades del permiso.
    /// </summary>
    public class Permiso
    {
        /// <summary>
        /// Obtiene o establece el identificador numérico del permiso.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre descriptivo del permiso.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Obtiene o establece la contraseña del usuario asociada al permiso.
        /// </summary>
        public string Password { get; set; }
    }
}
