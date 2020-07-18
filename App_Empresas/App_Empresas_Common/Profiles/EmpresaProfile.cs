using App_Empresas_Entities;
using App_Empresas_Services_Spec.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Empresas_Common.Profiles
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<Empresa, EmpresaDto>();
            CreateMap<TipoEmpresa, TipoEmpresaDto>();
            CreateMap<User, UserDto>();
            CreateMap<List<Empresa>, List<EmpresaDto>>();
            CreateMap<List<TipoEmpresa>, List<TipoEmpresaDto>>();
        }
    }
}
