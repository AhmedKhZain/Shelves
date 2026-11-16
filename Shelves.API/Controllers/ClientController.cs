using Microsoft.AspNetCore.Mvc;
using Shelves.Domain.Shared;
using Shelves.Services.ClientuseCases;

namespace Shelves.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServce _clientServce;
        public ClientController(IClientServce clientServce)
        {
            _clientServce = clientServce;
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(string name, string nationalID, string? email = null, string? phoneNumber = null)
        {
            var result = await _clientServce.AddClient(name, nationalID, email, phoneNumber);
            return Map(result);
        }


        public IActionResult Map<T>(ResultOf<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return StatusCode(500, result.Errors);
            }
        }

    }
}
