using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Checkpoint.Models;
using Projeto.Checkpoint.Repositorios;

namespace Projeto.Checkpoint.Controllers 
{
    public class UsuarioController : Controller 
    {
        [HttpGet]
        public ActionResult Index(){
            
            ComentarioRepositorio comentarioRepositorio = new ComentarioRepositorio();
            ViewData["Comentarios"] = comentarioRepositorio.Listar();
            return View ();
        }

        [HttpGet]
        public ActionResult Sobre(){
            return View();
        }

        [HttpGet]
        public ActionResult Contato(){
            return View();
        }

        [HttpGet]
        public ActionResult Duvidas(){
            return View();
        }

        [HttpGet]
        public ActionResult Cadastrar () {
            return View ();
        }

        [HttpPost]
        public ActionResult Cadastrar (IFormCollection form) {
            UsuarioModel usuario = new UsuarioModel(
                nome: form["nome"], 
                email: form["email"],
                senha: form["senha"]
                );

            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            usuarioRepositorio.Cadastrar(usuario);

            ViewBag.Mensagem = "Usuário Cadastrado";

            return RedirectToAction("Login", "Usuario");
        }

        [HttpGet]
        public IActionResult Login () {
            return View ();
        }

        [HttpPost]
        public IActionResult Login (IFormCollection form) {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            UsuarioModel usuario = usuarioRepositorio.Login(form["email"], form["senha"]);
            
          

            if(usuario != null){

                HttpContext.Session.SetString("idUsuario", usuario.Id.ToString());
                HttpContext.Session.SetString("nomeUsuario", usuario.Nome);
                
                if(usuario.Email == "admin@carfel.com" && usuario.Senha == "admin" ){
                    return RedirectToAction("Administrador", "Comentario");
                }

                return RedirectToAction ("Comentar", "Comentario");
            }


            ViewBag.Mensagem = "Usuário inválido";
            return View ();
        }

        [HttpPost]
        public IActionResult Logout(){
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("senha");
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Usuario");
        }


    }
}