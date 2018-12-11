using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Checkpoint.Models;
using Projeto.Checkpoint.Repositorios;

namespace Projeto.Checkpoint_Copia.Controllers
{
    public class ComentarioController : Controller
    {
        [HttpGet]
        public ActionResult Comentar(){

            if(string.IsNullOrEmpty(HttpContext.Session.GetString("idUsuario"))){
                return RedirectToAction("Login", "Usuario");
            }

            ComentarioRepositorio comentarioRepositorio = new ComentarioRepositorio();

            ViewData["Comentarios"] = comentarioRepositorio.Listar();

            return View();
        }

        [HttpGet]
        public ActionResult Administrador(){
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("idUsuario"))){
                return RedirectToAction("Login", "Usuario");
            }

             ComentarioRepositorio comentarioRepositorio = new ComentarioRepositorio();
             ViewData["Comentarios"] = comentarioRepositorio.Listar();
    
            return View();
        }


        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form){
            UsuarioRepositorio usuarioRep = new UsuarioRepositorio();
            UsuarioModel usuario = new UsuarioModel(
                 nome: HttpContext.Session.GetString("nomeUsuario")
            );
            ComentarioModel comentario = new ComentarioModel();
            comentario.Id = 1;
            comentario.Texto = form["texto"];
            comentario.DataCriacao = DateTime.Now;
            comentario.Usuario = usuarioRep.BuscarporId(int.Parse(HttpContext.Session.GetString("idUsuario")));  
            comentario.Status = "Espera";         

            ComentarioRepositorio comentarioRepositorio = new ComentarioRepositorio();
            comentarioRepositorio.Comentar(comentario);

            ViewData["Comentarios"] = comentarioRepositorio.Listar();

            return RedirectToAction("Comentar", "Comentario");
        }


        [HttpGet]
        public IActionResult Excluir (int id) {
            //Pega os dados do arquivo usuario.csv
            string[] linhas = System.IO.File.ReadAllLines("comentarios.csv");
            
            //Percorre as linhas do arquivo
            for(int i = 0; i < linhas.Length; i++)
            {

                //Verifica se a linha é vazia
                if (string.IsNullOrEmpty (linhas[i])) {
                    //Retorna para o foreach
                    continue;
                }

                //Separa as colunas de linha
                string[] linha = linhas[i].Split(';');

                //Verifica se o id da linha é o id passado
                if(id.ToString() == linha[2]){
                    //Defino a linha como vazia
                    linhas[i] = "";
                    break;
                } 
            }

            //Armazeno no arquivo csv todas as linhas
            System.IO.File.WriteAllLines("comentarios.csv", linhas);

            return RedirectToAction("Administrador", "Comentario");
        }

        [HttpGet]
        public IActionResult Aprovar(int id){
            
            ComentarioRepositorio comentarioRepositorio = new ComentarioRepositorio();

            ComentarioModel comentario = comentarioRepositorio.BuscarporId(id);

            if(comentario.Status == "Espera")
            {                 
                comentario.Status = "Aprovado";
                comentarioRepositorio.Aprovar(comentario);
            }

            return RedirectToAction("Administrador", "Comentario");
        }
    }
}