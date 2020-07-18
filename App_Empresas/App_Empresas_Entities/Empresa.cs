using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Empresas_Entities
{
    public class Empresa
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cnpj { get; set; }

        public int IdTipoEmpresa { get; set; }

        public TipoEmpresa TipoEmpresa { get; set; }
    }
}
