using App_Empresas_Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App_Empresas_Repository_Spec
{
    public interface IUserRepository
    {
        Task<User> Get(string id);
    }
}
