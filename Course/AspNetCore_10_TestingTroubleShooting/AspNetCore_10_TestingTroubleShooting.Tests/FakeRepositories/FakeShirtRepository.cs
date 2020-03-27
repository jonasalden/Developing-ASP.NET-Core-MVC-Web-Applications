using AspNetCore_10_TestingTroubleShooting.Services;
using AspNetCore_10_TestingTroubleShooting.Models;
using System.Collections.Generic;

namespace AspNetCore_10_TestingTroubleShooting.Tests.FakeRepositories
{
    internal class FakeShirtRepository : IShirtRepository
    {
        public IEnumerable<Shirt> GetShirts()
        {
            return new List<Shirt>()
            {
                new Shirt { Color = ShirtColor.Black, Size = ShirtSize.S, Price = 11F },
                new Shirt { Color = ShirtColor.Gray, Size = ShirtSize.M, Price = 12F },
                new Shirt { Color = ShirtColor.White, Size = ShirtSize.L, Price = 13F }
            };
        }

        public bool RemoveShirt(int id)
        {
            return true;
        }

        public bool AddShirt(Shirt shirt)
        {
            return true;
        }
    }
}
