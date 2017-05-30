using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace GrupoHitec.Login
{
    /// <summary>
    /// Clase que contiene las propiedades del login.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Método estático que permite comprobar los datos del usuario para el inicio de sesión.
        /// </summary>
        /// <param name="app">Nombre de la aplicación en la que se está intentando loguear el usuario.</param>
        /// <param name="connectionString">Cadena de conexión de donde se obtendrán los datos.</param>
        /// <param name="sessionName">Nombre que se le asignó a la sesión del usuario.</param>
        /// <param name="user">Instancia de la clase usuario.</param>
        /// <param name="windowsUser">(Opcional) Indica si el usuario es de Windows.</param>
        /// <param name="empresa">(Opcional) Nombre de la empresa a la cuál se encuentra asociado el usuario.</param>
        /// <returns>Retorna una instancia de la clase Usuario.</returns>
        public static Entities.Usuario IniciarSesion(string app, string connectionString, string sessionName, string schema = null, Entities.Usuario usuario = null, bool necesitaPermisos = false, bool windowsUser = false, string empresa = null)
        {
            HttpCookie myCookie = null;
            JavaScriptSerializer js = null;
            bool flagPermisos = false;
            try
            {
                myCookie = HttpContext.Current.Request.Cookies[sessionName];
                js = new JavaScriptSerializer();
                if (myCookie != null)
                {
                    if (!string.IsNullOrEmpty(myCookie["UserData"]))
                    {
                        try
                        {
                            usuario = (Entities.Usuario)js.Deserialize(myCookie["UserData"], typeof(Entities.Usuario));
                            usuario.Territorios = Business.Territorio.ObtenerTerritorios(empresa);
                            if (necesitaPermisos)
                            {
                                flagPermisos = usuario.Permisos != null ? (usuario.Permisos.Count > 0) : false;
                                if (!flagPermisos)
                                {
                                    usuario = null;
                                }
                            }
                        }
                        catch
                        {
                            usuario = null;
                        }
                    }
                    else
                    {
                        usuario = null;
                    }
                }
                else
                {
                    try
                    {
                        myCookie = new HttpCookie(sessionName);
                        usuario.UserName = windowsUser ? HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.LastIndexOf(@"\") + 1) : usuario.UserName;
                        usuario.Permisos = Business.Permiso.ObtenerPermisos(connectionString, app, usuario, schema);
                        if (windowsUser)
                        {
                            usuario.Password = Business.Usuario.ObtenerPassword(usuario);
                        }
                        usuario.Territorios = Business.Territorio.ObtenerTerritorios(empresa);
                        if (necesitaPermisos)
                        {
                            flagPermisos = usuario.Permisos != null ? (usuario.Permisos.Count > 0) : false;
                            if (flagPermisos)
                            {
                                myCookie["UserData"] = js.Serialize(usuario);
                                myCookie.Expires = DateTime.Now.AddYears(1000);
                            }
                            else
                            {
                                usuario = null;
                                myCookie = null;
                            }
                        }
                        else
                        {
                            myCookie["UserData"] = js.Serialize(usuario);
                            myCookie.Expires = DateTime.Now.AddYears(1000);
                        }
                    }
                    catch
                    {
                        myCookie = null;
                        usuario = null;
                    }
                    if (myCookie != null)
                    {
                        HttpContext.Current.Response.Cookies.Add(myCookie);
                    }
                }
                return usuario;
            }
            finally
            {
                js = null;
                usuario = null;
                myCookie = null;
            }
        }

        /// <summary>
        /// Método estático que permite cerrar la sesión del usuario.
        /// </summary>
        /// <param name="sessionName">Nombre que se le asignó a la sesión del usuario.</param>
        public static void CerrarSesion(string sessionName)
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[sessionName];
            //JavaScriptSerializer js = null;
            //Usuario usuario = null;
            if (myCookie != null)
            {
                HttpContext.Current.Request.Cookies.Remove(sessionName);
                /*if (!string.IsNullOrEmpty(myCookie["UserData"]))
                {
                    try
                    {
                        js = new JavaScriptSerializer();
                        usuario = (Usuario)js.Deserialize(myCookie["UserData"], typeof(Usuario));
                    }
                    catch
                    {
                        usuario = null;
                    }
                }*/
                myCookie = null;
            }
            /*if (usuario != null)
            {
                usuario.StartedSesion = false;
                js = new JavaScriptSerializer();
                myCookie["UserData"] = js.Serialize(usuario);
                myCookie.Expires = DateTime.Now.AddYears(1000);
                HttpContext.Current.Response.Cookies.Add(myCookie);
            }*/
        }
    }
}
