using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fitz_Roy.Models;
using LoginRequest = Fitz_Roy.Models.LoginRequest;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Aquí puedes validar las credenciales. Para simplificar, 
        // vamos a asumir que cualquier combinación de username y password es válida.
        // En un caso real, deberías validar contra una base de datos.

        if (IsValidUser(request))
        {
            var token = GenerateJwtToken(request.Username);
            return Ok(new { token });
        }

        return Unauthorized("Invalid credentials");
    }

    private bool IsValidUser(LoginRequest request)
    {
        // Valida las credenciales del usuario aquí
        // En este caso, cualquier usuario es válido, pero se puede reemplazar por una validación real
        return !string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password);
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}