using DoctorTime.API.DTO.LoginDTO;
using DoctorTime.API.Security.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorTime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<ActionResult<AuthenticationResponseDTO>> Login([FromBody] AuthenticationRequestDTO requestDTO )
        {
            try
            {
                AuthenticationResponseDTO responseDTO = await _loginService.LoginAsync(requestDTO.Email, requestDTO.Password);
                return Ok(responseDTO);
            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }
    }
}
