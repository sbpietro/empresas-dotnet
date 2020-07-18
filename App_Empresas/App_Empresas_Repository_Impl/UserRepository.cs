using App_Empresas_Entities;
using App_Empresas_Repository_Impl.Context;
using App_Empresas_Repository_Spec;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App_Empresas_Repository_Impl
{
    public class UserRepository : AppEmpresaContext, IUserRepository
    {
        public UserRepository(DbContextOptions<AppEmpresaContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public async Task<User> Get(string id)
        {
            return await Users.FirstAsync(x => x.UserID == id);
        }
    }
}
