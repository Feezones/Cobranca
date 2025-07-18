using FitBack.Models;
using FitBack.Repositories;
using FitBack.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace FitBack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UsuarioRepository _userRepo;
    private readonly JwtService _jwtService;

    public AuthController(UsuarioRepository userRepo, JwtService jwtService)
    {
        _userRepo = userRepo;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] Usuario user)
    {
        if (_userRepo.GetByEmail(user.Email) != null)
            return BadRequest("Email já cadastrado.");

        user.SenhaHash = HashPassword(user.SenhaHash);
        _userRepo.Add(user);
        return Ok("Usuário registrado com sucesso.");
    }

    [HttpPost("login")]
    public IActionResult Login(Usuario login)
    {
        var user = _userRepo.GetByEmail(login.Email);
        if (user == null || !VerifyPassword(login.SenhaHash, user.SenhaHash))
            return Unauthorized("Usuário ou senha inválidos.");

        var token = _jwtService.GenerateToken(user.Id, user.Nome);
        return Ok(new { Token = token });
    }

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    private static bool VerifyPassword(string input, string hash)
    {
        return HashPassword(input) == hash;
    }
}
