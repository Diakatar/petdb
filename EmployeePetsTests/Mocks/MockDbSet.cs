using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using EmployeePets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;

namespace EmployeePetsTests.Mocks
{
    public sealed class MockDbSet<TEntity> : Mock<DbSet<TEntity>> where TEntity : class, IEntityModel
    {
        public MockDbSet(List<TEntity> dataSource = null) {
            var data = (dataSource ?? new List<TEntity>());
            var queryable = data.AsQueryable();

            As<IQueryable<TEntity>>().Setup(e => e.Provider).Returns(queryable.Provider);
            As<IQueryable<TEntity>>().Setup(e => e.Expression).Returns(queryable.Expression);
            As<IQueryable<TEntity>>().Setup(e => e.ElementType).Returns(queryable.ElementType);
            As<IQueryable<TEntity>>().Setup(e => e.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            
            //Mocking the insertion of entities
            Setup(_ => _.AddAsync(It.IsAny<TEntity>(), It.IsAny<CancellationToken>()))
                .Returns<TEntity, CancellationToken>((arg, _) =>
                {
                     data.Add(arg);
                     return new ValueTask<EntityEntry<TEntity>>(new EntityEntry<TEntity>(new InternalClrEntityEntry()));
                });
            //Mocking the deletions of entities
            Setup(_ => _.Remove(It.IsAny<TEntity>())).Callback<TEntity>(arg=>data.Remove(arg));
            
            //Mocking the update of entities
            Setup(_ => _.Update(It.IsAny<TEntity>())).Verifiable();
            
            //Mocking the retrieve of entities
            Setup(_ => _.Find(It.IsAny<long>())).Returns((EntityEntry<TEntity> arg) => { data.FirstOrDefault(i=>i.)})

            //...the same can be done for other members like Remove
        }
    }
}