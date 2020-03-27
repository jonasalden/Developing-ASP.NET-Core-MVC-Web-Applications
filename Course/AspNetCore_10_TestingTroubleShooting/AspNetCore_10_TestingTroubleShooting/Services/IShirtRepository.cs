using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore_10_TestingTroubleShooting.Models;

namespace AspNetCore_10_TestingTroubleShooting.Services
{
    public interface IShirtRepository
    {
        IEnumerable<Shirt> GetShirts();
        bool RemoveShirt(int id);
        bool AddShirt(Shirt shirt);
    }
}
