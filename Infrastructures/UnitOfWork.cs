using Application;
using Application.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        public UnitOfWork(AppDbContext dbContext,IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public Task<int> SaveChangeAsync()
        {
           return _dbContext.SaveChangesAsync();
        }
    }
}
