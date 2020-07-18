using App_Empresas_Services_Spec.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Empresas_Services_Spec
{
    public interface IEmpresaService
    {
        List<EmpresaDto> Listar();

        EmpresaDto Obter(int id);

        List<EmpresaDto> ListarFiltrado(string nome, int idTipoEmpresa);
    }
}
