using System.Collections.Generic;
using System.Linq;

namespace Model.DAO
{
    public class SupplyCategoryDAO
    {
        public List<SupplyCategory> GetAll()
        {
            using (var context = new italiapizzaEntities())
            {
                return context.SupplyCategories.ToList();
            }
        }
    }
}
