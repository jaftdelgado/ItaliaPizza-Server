using Model;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Services.WasteServices
{
    public interface IWasteDAO
    {
        bool RegisterSupplyLoss(Supply supply);
    }
    public class WasteDAO : IWasteDAO
    {
        public bool RegisterSupplyLoss(Supply supply)
        {
            try
            {
                using (var context = new italiapizzaEntities())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var supplyLoss = context.Supplies.FirstOrDefault(s => s.SupplyID == supply.SupplyID);

                            supplyLoss.Stock = supply.Stock;

                            context.SaveChanges();
                            transaction.Commit();
                            return true;
                        } catch (SqlException e)
                        {
                            transaction.Rollback();
                            Console.WriteLine(e.Message);
                            return false;
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
