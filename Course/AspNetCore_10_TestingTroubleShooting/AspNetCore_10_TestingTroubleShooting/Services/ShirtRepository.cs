using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore_10_TestingTroubleShooting.Models;
using AspNetCore_10_TestingTroubleShooting.Data;

namespace AspNetCore_10_TestingTroubleShooting.Services
{
    public class ShirtRepository : IShirtRepository
    {
        #region Fields
        private readonly ShirtContext _context;
        #endregion

        #region Constructor
        public ShirtRepository(ShirtContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IEnumerable<Shirt> GetShirts()
        {
            return _context.Shirts.ToList();
        }

        public bool AddShirt(Shirt shirt)
        {
            _context.Add(shirt);

            var entries = _context.SaveChanges();

            if (entries > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveShirt(int id)
        {
            var shirt = _context.Shirts.SingleOrDefault(m => m.Id == id);
            _context.Shirts.Remove(shirt);

            var entries = _context.SaveChanges();

            if (entries > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
