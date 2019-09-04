namespace GuitarFilter.Migrations
{
    using GuitarFilter.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuitarFilter.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuitarFilter.Models.ApplicationDbContext context)
        {
            #region tblFilterNames - Назви фільтрів
            context.FilterNames.AddOrUpdate(f => f.Id,
                new Entities.FilterName
                {
                    Id = 1,
                    Name = "Тип"
                });
            context.FilterNames.AddOrUpdate(f => f.Id,
                new Entities.FilterName
                {
                    Id = 2,
                    Name = "Гриф"
                });
            context.FilterNames.AddOrUpdate(f => f.Id,
                new Entities.FilterName
                {
                    Id = 3,
                    Name = "Виробник"
                });
            #endregion

            #region tblFilterValues - Значення фільтра 
            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 1,
                    Name = "Електрогітара"
                });
            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 2,
                    Name = "Басгітара"
                });
            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 3,
                    Name = "Акустична гітара"
                });

            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 4,
                    Name = "Класична гітара"
                });
            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 5,
                    Name = "Клен"
                });
            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 6,
                    Name = "Червоне дерево"
                });
            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 7,
                    Name = "Cort"
                });
            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 8,
                    Name = "Yamaha"
                });
            context.FilterValues.AddOrUpdate(f => f.Id,
                new Entities.FilterValue
                {
                    Id = 9,
                    Name = "Fender"
                });
            #endregion

            #region FilterNameGroup - Групування фільрів по значеннях
            FilterNameGroup[] filterNameGroups =
            {
                new FilterNameGroup { FilterNameId = 1, FilterValueId=1 },
                new FilterNameGroup { FilterNameId = 1, FilterValueId=2 },
                new FilterNameGroup { FilterNameId = 1, FilterValueId=3 },
                new FilterNameGroup { FilterNameId = 1, FilterValueId=4 },

                new FilterNameGroup { FilterNameId = 2, FilterValueId=5 },
                new FilterNameGroup { FilterNameId = 2, FilterValueId=6 },

                new FilterNameGroup { FilterNameId = 3, FilterValueId=7 },
                new FilterNameGroup { FilterNameId = 3, FilterValueId=8 },
                new FilterNameGroup { FilterNameId = 3, FilterValueId=9 }
            };
            context.FilterNameGroups.AddOrUpdate(g =>
                    new { g.FilterNameId, g.FilterValueId }, filterNameGroups);
            #endregion

            #region tblProducts - Товари
            context.Products.AddOrUpdate(f => f.Id,
                new Product
                {
                    Id = 1,
                    Name = "Електрогітара CORT CR300 (Aged Vintage Burst)",
                    Price = 12376,
                    Quantity = 3,
                    DateCreate = DateTime.Now
                });
            context.Products.AddOrUpdate(f => f.Id,
                new Product
                {
                    Id = 2,
                    Name = "Акустична гітара YAMAHA FG820 (BL)",
                    Price = 9921,
                    Quantity = 2,
                    DateCreate = DateTime.Now
                });
            #endregion

            #region tblFilters - Фільтри по товарах
            Filter[] filters =
            {
                new Filter { FilterNameId = 1, FilterValueId=1, ProductId=1 },
                new Filter { FilterNameId = 2, FilterValueId=6, ProductId=1 },
                new Filter { FilterNameId = 3, FilterValueId=7, ProductId=1 },
                new Filter { FilterNameId = 1, FilterValueId=3, ProductId=2 },
                new Filter { FilterNameId = 2, FilterValueId=5, ProductId=2 },
                new Filter { FilterNameId = 3, FilterValueId=8, ProductId=2 },
            };
            context.Filters.AddOrUpdate(g => new { g.FilterNameId, g.FilterValueId, g.ProductId }, filters);
            #endregion
        }
    }
}
