using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moq.EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            var entities = new ShopEntities();

            var users = entities.Users.ToList();
        }
    }
}
