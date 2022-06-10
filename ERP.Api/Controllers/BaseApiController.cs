using ERP.Api.Filter;
using System.Web.Http;

namespace ERP.Api.Controllers
{
    [RoleAuthorize]
    public class BaseApiController : ApiController
    {
        // GET: BaseApi
        public BaseApiController()
        {
        }
    }
}