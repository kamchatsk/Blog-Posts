using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace Moq.EntityFramework
{
    public interface IObjectContext
    {
        IObjectSet<Order> Orders { get; }

        IObjectSet<Product> Products { get; }

        IObjectSet<User> Users { get; }
    }
}
