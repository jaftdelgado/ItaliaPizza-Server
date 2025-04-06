using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class SupplierDAO
    {
        public List<Supplier> GetByCategoryName(string categoryName)
        {
            using (var context = new italiapizzaEntities())
            {
                return (from s in context.Suppliers
                        join c in context.SupplyCategories on s.CategorySupply equals c.SupplyCategoryID
                        where c.CategoryName == categoryName
                        select s).ToList();
            }
        }

    }
}
