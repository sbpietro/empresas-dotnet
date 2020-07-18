using App_Empresas_Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Empresas_Repository_Spec
{
    public interface IEmpresaRepository
    {
        List<Empresa> List();

        Empresa Get(int id);

        List<Empresa> ListFiltered(string nome, int idTipoEmpresa);
    }
}
