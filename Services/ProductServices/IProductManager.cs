using Services.Dtos;
using System.Collections.Generic;
using System.ServiceModel;

namespace Services
{
    [ServiceContract]
    public interface IProductManager
    {
        [OperationContract]
        int AddProduct(ProductDTO productDTO);

        [OperationContract]
        List<ProductDTO> GetAllProducts(bool activeOnly = false);

        [OperationContract]
        bool UpdateProduct(ProductDTO productDTO);

        [OperationContract]
        bool DeleteProduct(int productId);

        [OperationContract]
        bool ReactivateProduct(int productId);

        [OperationContract]
        bool IsProductDeletable(int productId);
    }
}
