using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheToolkit.Tests
{
    public class MemoryCacheTests 
    {
        private ICacheService _cache;

        public MemoryCacheTests()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();

            services.AddTransient<ICacheService, MemoryCacheLibrary>();

            var serviceProvider = services.BuildServiceProvider();
            _cache = serviceProvider.GetService<ICacheService>();

        }
        [Fact]
        public void TestWrite()
        {
            var obj1 = new DummyObject() { Id = Guid.NewGuid(), Name = "TESTE", Value = 1234 };

            _cache.SetObject(obj1.Id.ToString(), obj1, 60);

            var resp = _cache.GetObject<DummyObject>(obj1.Id.ToString());

            Assert.True(resp != null);
        }

        [Fact]
        public void TestWriteList()
        {
            var list = new List<DummyObject>();

            var obj1 = new DummyObject() { Id = Guid.NewGuid(), Name = "TESTE", Value = 1234 };
            var obj2 = new DummyObject() { Id = Guid.NewGuid(), Name = "MARCIO", Value = 7888 };

            list.Add(obj1);
            list.Add(obj2);

            _cache.SetObject("LISTTEST", list, 60);

            var resp = _cache.GetObject<List<DummyObject>>("LISTTEST");

            Assert.True(resp.Count == 2);
        }
    }
}
