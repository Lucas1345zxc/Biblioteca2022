using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Biblioteca.Controllers
{

    public class UsuarioController : Controller
    {


        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            List<Usuario> listagem = new UsuarioService().Listar();
            return View(listagem);
        }

        public IActionResult editarUsuario(int id)
        {
            Usuario user = new UsuarioService().Listar(id);
            return View(user);
        }

        [HttpPost]

        public IActionResult editarUsuario(Usuario userEditado)
    {

        UsuarioService us = new UsuarioService();
        us.editarUsuario(userEditado);
        
        return RedirectToAction ("ListaDeUsuarios");

    }

     public IActionResult RegistrarUsuarios()
     {

      return View();
 
     }
     

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {

            //novoUser.Senha = criptografar...

            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);

            return RedirectToAction("ListaDeUsuarios");

        }

        public IActionResult ExcluirUsuario(int id){

            UsuarioService us = new UsuarioService();
            us.excluirUsuario(id);
            return RedirectToAction("ListaDeUsuarios");

        }

        public IActionResult Sair(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NeedAdmin(){
            
            return View();
            
        }

    }

}