using Model;
using System.Collections.Generic;
using System.Linq;

namespace Services.SupplyServices
{
    public class SupplyDAO
    {
        public List<SupplyCategory> GetCategories()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.SupplyCategories.ToList();
            }
        }

        public List<Supplier> GetSuppliersByCategory(int categoryId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Suppliers.Where(s => s.CategorySupply == categoryId).ToList();

            }
        }

        public List<Supply> GetSuppliesBySupplier(int supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Supplies.Where(s => s.SupplierID == supplierId).ToList();

            }
        }
    }
}
