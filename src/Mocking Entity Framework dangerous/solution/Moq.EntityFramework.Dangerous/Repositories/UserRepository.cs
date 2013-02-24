using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Moq.EntityFramework.Dangerous.Model;

namespace Moq.EntityFramework.Dangerous.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _context;

        public UserRepository(IUserContext context)
        {
            _context = context;
        }

        public IList<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public IList<User> GetWhereImageNotExists()
        {
            return _context.Users.Where(u => !File.Exists(u.PictureUrl)).ToList();
        }
    }

    public interface IUserRepository
    {
        IList<User> GetWhereImageNotExists();
    }
}