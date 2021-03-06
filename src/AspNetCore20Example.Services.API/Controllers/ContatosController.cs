﻿using AspNetCore20Example.Application.Interfaces;
using AspNetCore20Example.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AspNetCore20Example.Services.Api.Controllers
{
    public class ContatosController : BaseController
    {
        private readonly IContatoAppService _contatoAppService;

        public ContatosController(IContatoAppService contatoAppService)
        {
            _contatoAppService = contatoAppService;
        }

        [HttpGet]
        [Route("contatos")]
        [AllowAnonymous]
        public IEnumerable<ContatoViewModel> Get()
        {
            return _contatoAppService.ObterAtivos();
        }

        [HttpGet]
        [Authorize(Policy = "PermiteVerContatos")]
        [Route("contatos/{id:guid}")]
        public ContatoViewModel Get(Guid id)
        {
            return _contatoAppService.FindById(id);
        }

        [HttpPost]
        [Route("contatos")]
        [Authorize(Policy = "PermiteGerenciarContatos")]
        public IActionResult Post([FromBody]ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid)
                return Response();

            var retorno = _contatoAppService.Add(contatoViewModel);
            return Response(viewModel: retorno);
        }

        [HttpPut]
        [Route("contatos")]
        [Authorize(Policy = "PermiteGerenciarContatos")]
        public IActionResult Put([FromBody]ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid)
                return Response();

            var retorno = _contatoAppService.Update(contatoViewModel);
            return Response(viewModel: retorno);
        }

        [HttpDelete]
        [Route("contatos/{id:guid}")]
        [Authorize(Policy = "PermiteGerenciarContatos")]
        public IActionResult Delete(Guid id)
        {
            _contatoAppService.Delete(id);
            return Response();
        }

        [HttpPost]
        [Route("telefone")]
        [Authorize(Policy = "PermiteGerenciarTelefones")]
        public IActionResult Post([FromBody]TelefoneViewModel telefoneViewModel)
        {
            if (!ModelState.IsValid)
                return Response();

            var retorno = _contatoAppService.AdicionarTelefone(telefoneViewModel);
            return Response(viewModel: retorno);
        }

        [HttpPut]
        [Route("telefone")]
        [Authorize(Policy = "PermiteGerenciarTelefones")]
        public IActionResult Put([FromBody]TelefoneViewModel telefoneViewModel)
        {
            if (!ModelState.IsValid)
                return Response();

            var retorno = _contatoAppService.AtualizarTelefone(telefoneViewModel);
            return Response(viewModel: retorno);
        }
    }
}
