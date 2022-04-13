using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TarefasBackEnd.Models;
using TarefasBackEnd.Models.ViewModels;
using TarefasBackEnd.Repositories;
using TarefasBackEnd.ViewModels;

namespace TarefasBackEnd.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("cadastrar")]
        //[ProducesResponseType(typeof(IEnumerable<TokenResp>), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] UsuarioCadastroViewModel model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid) return BadRequest();

            repository.Create(model);
            return Ok();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UsuarioLoginViewModel model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid) return BadRequest();

            Usuario usuario = repository.Read(model.Email, model.Senha);
            if (usuario is null)
                return Unauthorized();
            return Ok(new
            {
                token = GenerateToken(usuario)
            });

        }
        private string GenerateToken(Usuario usuario)
        {
            //O Handler executa um comando de uma determinada ação
            var tokenHandler = new JwtSecurityTokenHandler();

            //chave que usa para fazer a criptografia das informações

            var key = Encoding.ASCII.GetBytes(SecretCompartilhada.Secret);
            var descriptor = new SecurityTokenDescriptor
            {
                //informações do usuário
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //informações que quero armazenar para o usuário
                    new Claim(ClaimTypes.Name,usuario.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier,usuario.Nome.ToString())
                }),
                //Tempo que o Token vai ser valido, e após esse tempo tem que ser gerado um novo
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(
                    //usando algoritimo para fazer a criptografia do meu token usando a chave key
                    new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature
                    )
            };
            //Criando o token com base no descriptor
            var token = tokenHandler.CreateToken(descriptor);
            return "Bearer "+tokenHandler.WriteToken(token); //convert o meu Token para uma string
        }
    }
}

