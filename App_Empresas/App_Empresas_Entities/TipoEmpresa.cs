using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App_Empresas_Entities
{
    public class TipoEmpresa
    {       
        public int Id { get; set; }
        
        public string Nome { get; set; }

        public string Sigla { get; set; }

        public IEnumerable<Empresa> Empresas { get; set; }
    }
}
