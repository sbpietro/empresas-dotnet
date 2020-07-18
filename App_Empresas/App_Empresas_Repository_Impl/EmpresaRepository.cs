using App_Empresas_Entities;
using App_Empresas_Repository_Impl.Context;
using App_Empresas_Repository_Spec;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App_Empresas_Repository_Impl
{
    public class EmpresaRepository : AppEmpresaContext, IEmpresaRepository
    {
        public EmpresaRepository(DbContextOptions<AppEmpresaContext> contextOptions) : base(contextOptions)
        {
        }

        public List<Empresa> List()
        {
            var lista = Empresas.ToList();
            return lista;
        }

        public Empresa Get(int id)
        {
            return base.Empresas.Single(x => x.Id == id);
        }

        public List<Empresa> ListFiltered(string nome, int idTipoEmpresa)
        {
            var lista = Empresas.ToList();
            if (!string.IsNullOrEmpty(nome))
                lista = lista.Where(x => x.Nome.Equals(nome)).ToList();

            if (idTipoEmpresa > 0)
                lista = lista.Where(x => x.IdTipoEmpresa == idTipoEmpresa).ToList();

            return lista;
        } 
    }
}
