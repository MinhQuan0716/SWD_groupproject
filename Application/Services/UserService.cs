using Application.Interface;
using Application.InterfaceRepository;
using Application.Util;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> RegisterAsync(string username, string password)
        {
            bool user = await _userRepository.CheckEmail(username);
            if (user)
            {
                throw new Exception("Account already existed");
            }
            User newUser = new User
            {
                Email = username,
                Password = password.Hash()
            };
            await _userRepository.AddAsync(newUser);
            return await _unitOfWork.SaveChangeAsync() > 0;

        }
    }
}

