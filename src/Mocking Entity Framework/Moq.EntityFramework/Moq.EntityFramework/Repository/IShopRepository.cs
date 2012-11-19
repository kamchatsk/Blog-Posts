using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moq.EntityFramework.Repository
{
    public interface IShopRepository
    {
        IList<User> GetUsers();

        User GetUserById(int id);
    }
}
