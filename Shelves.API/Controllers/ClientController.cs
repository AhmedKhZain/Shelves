using Microsoft.AspNetCore.Mvc;
using Shelves.API.Requests;
using Shelves.Services.ClientuseCases;

namespace Shelves.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseApiController
    {
        private readonly IClientServce _clientServce;
        public ClientController(IClientServce clientServce) : base()
        {
            _clientServce = clientServce;
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(string name, string nationalID, string? email = null, string? phoneNumber = null)
        {
            var result = await _clientServce.AddClient(name, nationalID, email, phoneNumber);
            return MapResult(result);
        }
        [HttpGet("{nationalId}")]
        public async Task<IActionResult> GetById(
            [FromRoute] string nationalId
            )
        {
            var result = await _clientServce.GetClientById(nationalId);
            return MapResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] ClientGetAllRequest request
            )
        {
            var result = await _clientServce.GetAll(request.Name, request.Email, request.Phone, request.PageNumber, request.PageSize);
            return MapResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] string id)
        {
            var result = await _clientServce.DeleteClient(id);
            return MapResult(result);
        }
    }
}
