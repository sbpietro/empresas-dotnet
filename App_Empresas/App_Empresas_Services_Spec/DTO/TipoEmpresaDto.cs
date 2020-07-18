using System;
using System.Collections.Generic;
using System.Text;

namespace App_Empresas_Services_Spec.DTO
{
    public class TipoEmpresaDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public IEnumerable<EmpresaDto> Empresas { get; set; }
    }
}
