
namespace EShop.Domain.Enums.ProductEnums
{

    #region Product Enums
    public enum CreateProductResult
    {
        Success,
        InvalidInput,
        ImageUploadFailed,
        
    }

    public enum UpdateProductResult
    {
        Success,
        NotFound,
        InvalidInput,
        ImageUploadFailed,
        }
    public enum DeleteProductResult
    {
        Success,
        NotFound,
        
    }

    #endregion

}
