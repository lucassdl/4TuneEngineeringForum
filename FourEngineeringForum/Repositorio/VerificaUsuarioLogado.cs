using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace FourEngineeringForum.Repositorio
{
    /// <summary>
    /// Classe responsável por fazer o controle de acesso às views para publicação de posts e cadastro de tópicos, definindo que o acesso seja permitido apenas por usuários autenticados no sistema
    /// </summary>
    public class VerificaUsuarioLogado : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Boolean usuarioLogado = AutenticarUsuario.VerificarSessaoUsuario();

            if (!usuarioLogado)
            {
                filterContext.Result = new RedirectToRouteResult(
                 new RouteValueDictionary(new { action = "Index", Controller = "Login", area = "" }));
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
