using App_Empresas_Entities;
using App_Empresas_Repository_Spec;
using App_Empresas_Services_Spec;
using App_Empresas_Services_Spec.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App_Empresas_Services_Impl.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUser(string id)
        {
            var result = await _userRepository.Get(id);

            var mapeado = _mapper.Map<User, UserDto>(result);

            return mapeado;
        }
    }
}
