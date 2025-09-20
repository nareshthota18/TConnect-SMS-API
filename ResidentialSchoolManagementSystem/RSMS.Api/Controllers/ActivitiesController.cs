using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSMS.Services.Implementations;
using RSMS.Services.Interfaces;

namespace RSMS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesService _service;

        public ActivitiesController(IActivitiesService service)
        {
            _service = service;
        }
    }
}
