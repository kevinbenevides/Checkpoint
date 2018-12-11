using System;
using System.Collections.Generic;
using System.IO;
using Projeto.Checkpoint.Interfaces;
using Projeto.Checkpoint.Models;

namespace Projeto.Checkpoint.Repositorios {
        public class UsuarioRepositorio : IUsuario {
            public UsuarioModel Cadastrar (UsuarioModel usuario) {
                //Verifica se o arquivo existe
                if (File.Exists ("usuarios.csv")) {
                    //Se arquivo existe Pega a quantidade de linhas e incrementa 1
                    usuario.Id = File.ReadAllLines ("usuarios.csv").Length + 1;
                } else {
                    //caso não exista seta como 1
                    usuario.Id = 1;
                }

                //Grava as informações no arquivo usuarios.csv
                using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)) {
                    sw.WriteLine ($"{usuario.Id};{usuario.Nome};{usuario.Email};{usuario.Senha}");
                }

                return usuario;
            }

            public UsuarioModel Login (string email, string senha) {
                using (StreamReader sr = new StreamReader ("usuarios.csv")) {
                    while (!sr.EndOfStream) {
                        var linha = sr.ReadLine ();

                        if (string.IsNullOrEmpty (linha)) {
                            continue;
                        }

                        string[] dados = linha.Split (";");

                        if (dados[2] == email && dados[3] == senha) {
                            UsuarioModel usuario = new UsuarioModel (
                                id: int.Parse (dados[0]),
                                nome: dados[1],
                                email: dados[2],
                                senha: dados[3]
                            );

                            return usuario;
                        }
                    }
                }

                return null;
            }

            public UsuarioModel BuscarporId (int id) {

                string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

                foreach (var item in linhas) {
                    string[] dados = item.Split (';');

                    if (id.ToString () == dados[0]) {
                        UsuarioModel usuario = new UsuarioModel (
                            id: int.Parse (dados[0]),
                            nome: dados[1],
                            email: dados[2],
                            senha: dados[3]
                        );

                        return usuario;
                    }
                }
                return null;
            }
        }
}