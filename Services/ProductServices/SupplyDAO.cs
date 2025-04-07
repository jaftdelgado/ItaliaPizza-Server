using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class SupplyDAO
    {
        public List<Supply> GetBySupplier(int supplierId)
        {
            using (var context = new italiapizzaEntities())
            {
                return context.Supplies.Where(s => s.SupplierID == supplierId).ToList();
            }
        }
    }
}
