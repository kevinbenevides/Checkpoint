using System;
namespace Projeto.Checkpoint.Models {
    public class ComentarioModel {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
        public UsuarioModel Usuario { get; set; }

        public ComentarioModel (int idUsuario, string nome, int idComentario, string texto,  DateTime datacriacao, string status) {
            this.Usuario = new UsuarioModel();
            this.Usuario.Id = idUsuario;
            this.Usuario.Nome = nome;
            this.Id = idComentario;
            this.Texto = texto;
            this.DataCriacao = datacriacao;
            this.Status = status;
        }

        public ComentarioModel()
        {
        }

        // public ComentarioModel(int idUsuario, string nome, int idComentario, string texto, DateTime datacriacao, string status){
        //     this.Usuario = new UsuarioModel();
        //     this.Usuario.Id = idUsuario;
        //     this.Usuario.Nome = nome;
        //     this.Id = idComentario;
        //     this.Texto = texto;
        //     this.DataCriacao = datacriacao;
        //     this.Status = status;
        // }
    }
}