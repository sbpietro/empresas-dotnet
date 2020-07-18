using System;
using System.Collections.Generic;
using System.Text;

namespace App_Empresas_Services_Spec.DTO
{
    public class EmpresaDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cnpj { get; set; }

        public int IdTipoEmpresa { get; set; }

        public TipoEmpresaDto TipoEmpresa { get; set; }
    }
}
