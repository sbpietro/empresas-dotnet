using App_Empresas_Entities;
using App_Empresas_Repository_Spec;
using App_Empresas_Services_Spec;
using App_Empresas_Services_Spec.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Empresas_Services_Impl.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IMapper _mapper;

        public EmpresaService(IEmpresaRepository empresaRepository, IMapper mapper)
        {
            _empresaRepository = empresaRepository;
            _mapper = mapper;
        }

        public List<EmpresaDto> Listar()
        {
            var lista = _empresaRepository.List();
            return TransformList(lista);
        }

        public List<EmpresaDto> ListarFiltrado(string nome, int idTipoEmpresa)
        {
            var lista = _empresaRepository.ListFiltered(nome, idTipoEmpresa);
            return TransformList(lista);
        }

        public EmpresaDto Obter(int id)
        {
            var entidade = _empresaRepository.Get(id);

            return _mapper.Map<Empresa, EmpresaDto>(entidade);
        }

        private List<EmpresaDto> TransformList(List<Empresa> lista)
        {
            var resultado = new List<EmpresaDto>();

            foreach (var item in lista)
            {
                var dto = _mapper.Map<Empresa, EmpresaDto>(item);
                resultado.Add(dto);
            }

            return resultado;
        }
    }
}
