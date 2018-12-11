using System;
using System.Collections.Generic;
using System.IO;
using Projeto.Checkpoint.Interfaces;
using Projeto.Checkpoint.Models;

namespace Projeto.Checkpoint.Repositorios
{
    public class ComentarioRepositorio : IComentario
    {
        public ComentarioModel Comentar(ComentarioModel comentario)
        {
            if (File.Exists ("comentarios.csv")) {
                    //Se arquivo existe Pega a quantidade de linhas e incrementa 1
                    comentario.Id = File.ReadAllLines ("comentarios.csv").Length + 1;
                } else {
                    //caso não exista seta como 1
                    comentario.Id = 1;
                }

            using(StreamWriter sw = new StreamWriter("comentarios.csv", true)){
                sw.WriteLine($"{comentario.Usuario.Id};{comentario.Usuario.Nome};{comentario.Id};{comentario.Texto};{comentario.DataCriacao.ToShortDateString()};{comentario.Status}");
            }

            return comentario;
        }

        public List<ComentarioModel> Listar(){

            List<ComentarioModel> lsUsuarios = new List<ComentarioModel> ();

            if(!File.Exists("comentarios.csv")){
                return lsUsuarios;
            }

            string[] linhas = System.IO.File.ReadAllLines ("comentarios.csv");

            ComentarioModel comentario ;

             foreach (var item in linhas) {

                //Verifica se a linha é vazia
                if (string.IsNullOrEmpty (item)) {
                    //Retorna para o foreach
                    continue;
                }

                string[] linha = item.Split (';');

                comentario = new ComentarioModel(
                    idUsuario: int.Parse(linha[0]),
                    nome: linha[1],
                    idComentario: int.Parse(linha[2]),
                    texto: linha[3],
                    datacriacao: DateTime.Parse(linha[4]),
                    status: linha[5]
                );

               lsUsuarios.Add (comentario);
            }

            return lsUsuarios;

        }

        public ComentarioModel Aprovar(ComentarioModel comentario)
        {
            string[] linhas = System.IO.File.ReadAllLines("comentarios.csv");

            for (int i = 0; i < linhas.Length; i++)
            {
                if(string.IsNullOrEmpty(linhas[i])){
                    continue;
                }

                string[] dados = linhas[i].Split(";");

                if(dados[2] == comentario.Id.ToString()){
                    linhas[i] = $"{comentario.Usuario.Id};{comentario.Usuario.Nome};{comentario.Id};{comentario.Texto};{comentario.DataCriacao};{comentario.Status}";
                    break;
                }
            }

            System.IO.File.WriteAllLines("comentarios.csv", linhas);

            return comentario;
        }

        public ComentarioModel BuscarporId (int id) {

                string[] linhas = System.IO.File.ReadAllLines ("comentarios.csv");

                foreach (var item in linhas) {
                    string[] dados = item.Split (';');

                     //Verifica se a linha é vazia
                    if (string.IsNullOrEmpty (item)) {
                        //Retorna para o foreach
                        continue;
                    }

                    if (id.ToString () == dados[2]) {
                        ComentarioModel comentario = new ComentarioModel (
                            idUsuario: int.Parse (dados[0]),
                            nome: dados[1],
                            idComentario: int.Parse (dados[2]),
                            texto: dados[3],
                            datacriacao: DateTime.Parse(dados[4]),
                            status: dados[5]
                        );

                        return comentario;
                    }
                }
                return null;
            }
    }
}