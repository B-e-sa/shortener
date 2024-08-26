using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Shortener.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private ISender _sender;

        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    }
}