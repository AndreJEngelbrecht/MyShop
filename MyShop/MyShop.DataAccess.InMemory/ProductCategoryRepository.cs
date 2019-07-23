using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        public void Insert(ProductCategory pC)
        {
            productCategories.Add(pC);
        }
        public void Update(ProductCategory productCategory)
        {
            ProductCategory ProductCategoryToUpdate = productCategories.Find(pC => pC.Id == productCategory.Id);
            if (ProductCategoryToUpdate != null)
            {
                ProductCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Category not found");
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(pC => pC.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Category not found");
            }
        }
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory ProductCategoryToDelete = productCategories.Find(pC => pC.Id == Id);
            if (ProductCategoryToDelete != null)
            {
                productCategories.Remove(ProductCategoryToDelete);
            }
            else
            {
                throw new Exception("Category not found");
            }
        }
    }
}
