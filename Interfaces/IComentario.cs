using Projeto.Checkpoint.Models;
using System.Collections.Generic;

namespace Projeto.Checkpoint.Interfaces
{
    public interface IComentario 
    {
        ComentarioModel Comentar(ComentarioModel comentario);
    }
}