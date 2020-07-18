using App_Empresas_Services_Spec.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App_Empresas_Services_Spec
{
    public interface IUserService
    {
        Task<UserDto> GetUser(string id);
    }
}
