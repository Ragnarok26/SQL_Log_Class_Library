using System.Collections.Generic;

namespace GrupoHitec.Login.Entities
{
    /// <summary>
    /// Clase que contiene las propiedades del usuario.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Obtiene o establece el identificador numérico del usuario.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Obtiene o establece el identificador numérico del rol asignado al usuario.
        /// </summary>
        public long IdRol { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre de la aplicación.
        /// </summary>
        public string App { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre del usuario.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Obtiene o establece el identificador numérico de la compañía asignada al usuario.
        /// </summary>
        public int IdCompany { get; set; }
        /// <summary>
        /// Obtiene o establece el apellido paterno del usuario.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Obtiene o establece el apellido materno del usuario.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre con el cual el usuario podrá acceder al sistema.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Obtiene o establece si se tiene una sesión iniciada.
        /// </summary>
        public bool StartedSesion { get; set; }
        /// <summary>
        /// Obtiene o establece si se van a recordar los datos del usuario.
        /// </summary>
        public bool RememberMe { get; set; }
        /// <summary>
        /// Obtiene o establece el listado de roles otorgados al usuario.
        /// </summary>
        public List<Rol> Roles { get; set; }
        /// <summary>
        /// Obtiene o establece la compañía a la que se encuentra asociado el usuario
        /// </summary>
        public Compania Compania { get; set; }
        /// <summary>
        /// Obtiene o establece el listado de permisos otorgados al usuario.
        /// </summary>
        public List<Permiso> Permisos { get; set; }
        /// <summary>
        /// Obtiene o establece el listado de territorios otorgados al usuario.
        /// </summary>
        public List<Territorio> Territorios { get; set; }
    }
}
