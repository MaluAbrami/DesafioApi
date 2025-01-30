using System;
using DesafioApi.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DesafioApi.Tests
{
    public class UnitTest1
    {

        private readonly DbContextOptions<ApplicationDbContext> options;

        public UnitTest1()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;      
        }

        [Fact]
        public void Test1()
        {
            
        }
    }
}
