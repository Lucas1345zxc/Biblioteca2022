using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult ListaDeUsuarios(){
            
            Autenticacao.CheckLogin(this);
            Autenticacao.verficaSeUsuarioAdminExiste(this);
            
            List<Usuario> Listagem = new UsuarioService().Listar();

            return View(Listagem);

        }

        public IActionResult EditarUsuario(int id)
        {
            
            Autenticacao.CheckLogin(this);
            Autenticacao.verficaSeUsuarioAdminExiste(this);

            Usuario user = new UsuarioService().Listar(id);
            return View(user);
                        
        }

        [HttpPost]
         public IActionResult EditarUsuario(Usuario userEditado)
        {
            
            userEditado.Senha = Criptografo.TextoCriptografado(userEditado.Senha);
            UsuarioService us = new UsuarioService();
            us.EditarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
                        
        }

        public IActionResult RegistrarUsuarios()
        {
    
            Autenticacao.CheckLogin(this);
            Autenticacao.verficaSeUsuarioAdminExiste(this);
            return View();
                        
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
           
            Autenticacao.CheckLogin(this);
            Autenticacao.verficaSeUsuarioAdminExiste(this);

            novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);

            UsuarioService us = new UsuarioService();
            us.IncluirUsuario(novoUser);

            return RedirectToAction("ListaDeUsuarios");
                        
        }

        public IActionResult ExcluirUsuario(int id)
        {
           
            Autenticacao.CheckLogin(this);
           
            UsuarioService us = new UsuarioService();
            us.ExcluirUsuario(id);

            return RedirectToAction("ListaDeUsuarios");
                        
        }

          public IActionResult Sair()
        {
           
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
                        
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }
    }

}