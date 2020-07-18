using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Empresas_Services_Spec;
using App_Empresas_Services_Spec.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App_Empresas.Controllers
{
    //[Authorize]
    [Route("api/v1/enterprises")]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public IActionResult GetEmpresa(int id)
        {
            var empresa = _empresaService.Obter(id);
            if (empresa == null)
                return NotFound();

            return Json(empresa);
        }

        [Authorize("Bearer")]
        [HttpGet]
        public IActionResult ListEmpresa(int? enterprise_types, string name)
        {
            var lista = new List<EmpresaDto>();

            if (!enterprise_types.HasValue && string.IsNullOrEmpty(name))
                lista = _empresaService.Listar();
            else
                lista = _empresaService.ListarFiltrado(name, enterprise_types.Value);

            if (lista == null)
                return NotFound();

            if (lista.Count == 0)
                return NoContent();

            return Json(lista);
        }

    }
}