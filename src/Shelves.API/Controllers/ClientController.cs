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
        public async Task<IActionResult> AddClient([FromBody] ClientAddRequest request)
        {
            var result = await _clientServce.AddClient(request.Name, request.NationalID, request.Email, request.PhoneNumber);
            return MapResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id
            )
        {
            var result = await _clientServce.GetClientById(id);
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
        public async Task<IActionResult> DeleteClient([FromRoute] Guid id)
        {
            var result = await _clientServce.DeleteClient(id);
            return MapResult(result);
        }
    }
}
