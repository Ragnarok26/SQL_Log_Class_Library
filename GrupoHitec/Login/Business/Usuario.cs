using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace GrupoHitec.Login.Business
{
    /// <summary>
    /// Clase de lógica de negocio de usuario.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Método estático que permite obtener la contraseña del usuario asociada a los permisos.
        /// </summary>
        /// <returns>Retorna la cadena de la contraseña.</returns>
        public static string ObtenerPassword(Entities.Usuario usuario)
        {
            try
            {
                if (usuario.Permisos != null)
                {
                    return usuario.Permisos.FirstOrDefault(v => !string.IsNullOrEmpty(v.Password)).Password;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Método estático el cual permite obtener el usuario que ha iniciado sesión en el sistema.
        /// </summary>
        /// <param name="SessionName">Nombre que se le asignó a la sesión del usuario.</param>
        /// <returns>Retorna una instancia de la clase usuario.</returns>
        public static Entities.Usuario Obtener(string SessionName)
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[SessionName];
            JavaScriptSerializer js = null;
            Entities.Usuario usuario = null;
            if (myCookie != null)
            {
                if (!string.IsNullOrEmpty(myCookie["UserData"]))
                {
                    try
                    {
                        js = new JavaScriptSerializer();
                        usuario = (Entities.Usuario)js.Deserialize(myCookie["UserData"], typeof(Entities.Usuario));
                    }
                    catch
                    {
                        usuario = null;
                    }
                }
            }
            return usuario;
        }

        /// <summary>
        /// Método estático que permite registrar la sesión del usuario.
        /// </summary>
        /// <param name="usuario">Objeto con los datos del usuario.</param>
        /// <param name="SessionName">Nombre que se le asignará a la sesión del usuario.</param>
        /// <returns>Retorna verdadero cuando se logró registrar la sesión, en caso contrario retorna falso.</returns>
        public static bool RegistrarSesion(Entities.Usuario usuario, string SessionName)
        {
            HttpCookie myCookie = null;
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                myCookie = HttpContext.Current.Request.Cookies[SessionName];
            }
            catch
            {
                myCookie = null;
            }
            finally
            {
                if (myCookie != null)
                {
                    HttpContext.Current.Response.Cookies.Remove(SessionName);
                }
                myCookie = new HttpCookie(SessionName);
            }
            try
            {
                myCookie["UserData"] = js.Serialize(usuario);
                myCookie.Expires = DateTime.Now.AddYears(1000);
                HttpContext.Current.Response.Cookies.Add(myCookie);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                js = null;
                myCookie = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static MSSQLServer.SqlCollectionResult GetUsuarioPorId(string ConnectionString, Entities.Usuario user, string Schema = null)
        {
            DataAccess.Usuario usuario = null;
            MSSQLServer.SqlCollectionResult usuarioResult = new MSSQLServer.SqlCollectionResult();
            MSSQLServer.SqlCollectionResult companiaResult = new MSSQLServer.SqlCollectionResult();
            MSSQLServer.SqlCollectionResult rolResult = new MSSQLServer.SqlCollectionResult();
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = user.Id,
                }
            );
            try
            {
                usuario = new DataAccess.Usuario(ConnectionString, Schema);
                usuarioResult = usuario.GetAllDataById(parameters);
                if (!usuarioResult.HasError)
                {
                    if (((List<Entities.Usuario>)usuarioResult.Collection).Count == 1)
                    {
                        rolResult = Business.Rol.GetRolPorUsuario(ConnectionString,
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Id, 
                            Schema
                        );
                        if (!rolResult.HasError)
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Roles = ((List<Entities.Rol>)rolResult.Collection);
                        }
                        else
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Roles = new List<Entities.Rol>();
                        }
                        companiaResult = Business.Compania.GetCompaniaPorId(ConnectionString,
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).IdCompany, 
                            Schema
                        );
                        if (!companiaResult.HasError)
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Compania = ((List<Entities.Compania>)companiaResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Compania = new Entities.Compania() { Id = 0, Name = string.Empty };
                        }
                    }
                    else
                    {
                        usuarioResult.Collection = null;
                    }
                }
                return usuarioResult;
            }
            finally
            {
                usuario = null;
                rolResult = null;
                parameters = null;
                usuarioResult = null;
                companiaResult = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static MSSQLServer.SqlCollectionResult GetUsuariosPorNombre(string ConnectionString, Entities.Usuario user, string Schema = null)
        {
            DataAccess.Usuario usuario = null;
            MSSQLServer.SqlCollectionResult usuarioResult = new MSSQLServer.SqlCollectionResult();
            MSSQLServer.SqlCollectionResult companiaResult = new MSSQLServer.SqlCollectionResult();
            MSSQLServer.SqlCollectionResult rolResult = new MSSQLServer.SqlCollectionResult();
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@name",
                    Type = "NVarChar",
                    Value = user.Name,
                }
            );
            try
            {
                usuario = new DataAccess.Usuario(ConnectionString, Schema);
                usuarioResult = usuario.GetAllDataByName(parameters);
                if (!usuarioResult.HasError)
                {
                    for (int x = 0; x < ((List<Entities.Usuario>)usuarioResult.Collection).Count; x++)
                    {
                        rolResult = Business.Rol.GetRolPorUsuario(ConnectionString,
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(x).Id,
                            Schema
                        );
                        if (!rolResult.HasError)
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(x).Roles = ((List<Entities.Rol>)rolResult.Collection);
                        }
                        else
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(x).Roles = new List<Entities.Rol>();
                        }
                        companiaResult = Business.Compania.GetCompaniaPorId(ConnectionString,
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(x).IdCompany,
                            Schema
                        );
                        if (!companiaResult.HasError)
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(x).Compania = ((List<Entities.Compania>)companiaResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(x).Compania = new Entities.Compania() { Id = 0, Name = string.Empty };
                        }
                    }
                }
                return usuarioResult;
            }
            finally
            {
                usuario = null;
                rolResult = null;
                parameters = null;
                usuarioResult = null;
                companiaResult = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static MSSQLServer.SqlCollectionResult GetUsuarioPorCredencial(string ConnectionString, Entities.Usuario user, string Schema = null)
        {
            DataAccess.Usuario usuario = null;
            MSSQLServer.SqlCollectionResult usuarioResult = new MSSQLServer.SqlCollectionResult();
            MSSQLServer.SqlCollectionResult companiaResult = new MSSQLServer.SqlCollectionResult();
            MSSQLServer.SqlCollectionResult rolResult = new MSSQLServer.SqlCollectionResult();
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@username",
                    Type = "NVarChar",
                    Value = user.UserName,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@password",
                    Type = "NVarChar",
                    Value = user.Password,
                }
            );
            try
            {
                usuario = new DataAccess.Usuario(ConnectionString);
                usuarioResult = usuario.GetAllDataByParameters(parameters);
                if (!usuarioResult.HasError)
                {
                    if (((List<Entities.Usuario>)usuarioResult.Collection).Count == 1)
                    {
                        rolResult = Business.Rol.GetRolPorUsuario(ConnectionString,
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Id,
                            Schema
                        );
                        if (!rolResult.HasError)
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Roles = ((List<Entities.Rol>)rolResult.Collection);
                        }
                        else
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Roles = new List<Entities.Rol>();
                        }
                        companiaResult = Business.Compania.GetCompaniaPorId(ConnectionString,
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).IdCompany,
                            Schema
                        );
                        if (!companiaResult.HasError)
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Compania = ((List<Entities.Compania>)companiaResult.Collection).FirstOrDefault();
                        }
                        else
                        {
                            ((List<Entities.Usuario>)usuarioResult.Collection).ElementAt(0).Compania = new Entities.Compania() { Id = 0, Name = string.Empty };
                        }
                    }
                    else
                    {
                        usuarioResult.Collection = null;
                    }
                }
                return usuarioResult;
            }
            finally
            {
                usuario = null;
                rolResult = null;
                parameters = null;
                usuarioResult = null;
                companiaResult = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static MSSQLServer.SqlChangesResult UpdateUsuario(string ConnectionString, Entities.Usuario user, string Schema = null)
        {
            DataAccess.Usuario usuario = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = user.Id,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@name",
                    Type = "NVarChar",
                    Value = user.Name,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@firstName",
                    Type = "NVarChar",
                    Value = user.FirstName,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@lastName",
                    Type = "NVarChar",
                    Value = user.LastName,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@userName",
                    Type = "NVarChar",
                    Value = user.UserName,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@password",
                    Type = "NVarChar",
                    Value = user.Password,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@idCompany",
                    Type = "Int",
                    Value = user.IdCompany,
                }
            );
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@idRol",
                    Type = "BigInt",
                    Value = user.IdRol,
                }
            );
            try
            {
                usuario = new DataAccess.Usuario(ConnectionString, Schema);
                return usuario.UpdateUserSP(parameters);
            }
            finally
            {
                usuario = null;
                parameters = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static MSSQLServer.SqlChangesResult RemoveUsuario(string ConnectionString, Entities.Usuario user, string Schema = null)
        {
            DataAccess.Usuario usuario = null;
            List<MSSQLServer.SqlParam> parameters = new List<MSSQLServer.SqlParam>();
            parameters.Add(
                new MSSQLServer.SqlParam()
                {
                    Name = "@Id",
                    Type = "BigInt",
                    Value = user.Id,
                }
            );
            try
            {
                usuario = new DataAccess.Usuario(ConnectionString, Schema);
                return usuario.DeleteUserSP(parameters);
            }
            finally
            {
                usuario = null;
                parameters = null;
            }
        }
    }
}
