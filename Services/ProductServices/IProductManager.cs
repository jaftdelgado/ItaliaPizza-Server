using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract]
    public interface IProductManager
    {
        [OperationContract]
        int AddProduct(ProductDTO productDTO);

    }
}
