using Projeto.Checkpoint.Models;
using System.Collections.Generic;

namespace Projeto.Checkpoint.Interfaces
{
    public interface IUsuario
    {
        /// <summary>
        /// Cadastra os usuários
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
         UsuarioModel Cadastrar(UsuarioModel usuario);

        /// <summary>
        /// Faz o login dos usuários
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
         UsuarioModel Login(string email, string senha);
    }
}