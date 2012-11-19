using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moq.EntityFramework.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly IObjectContext _context;

        public ShopRepository()
        {
            _context = new ShopEntities();
        }

        public ShopRepository(IObjectContext context)
        {
            _context = context;
        }

        public IList<User> GetUsers()
        {
            return _context.Users.Include("Orders").ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Include("Orders").SingleOrDefault(u => u.id == id);
        }
    }
}
