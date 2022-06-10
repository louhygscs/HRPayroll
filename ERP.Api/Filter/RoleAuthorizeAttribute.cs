using ERP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace ERP.Api.Filter
{
    public class RoleAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            IEnumerable<string> HeaderValues;

            var UserToken = HttpContext.Current.Request.Headers["X-AUTH-TOKEN"];

            if (!actionContext.Request.Headers.TryGetValues("X-AUTH-TOKEN", out HeaderValues))
            {
                HandleUnauthorizedRequest(actionContext);
                return;
            }
            else
            {
                //if (HeaderValues.First() != SessionHelper.PatientUser.UserToken)
                if (UserToken != null)
                {
                    if (HeaderValues.First() != GlobalHelper.ApiHeaderToken)
                    {
                        HandleUnauthorizedRequest(actionContext);
                        return;
                    }
                }
            }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}