namespace EShop.Domain.Enums.ProductSpecificationMapping;

public class ProductSpecificationMappingEnums
{

    public enum CreateProductSpecificationResult
    { Success, InvalidInput, NotFound }
    public enum UpdateProductSpecificationResult 
    { Success, InvalidInput, NotFound }
    public enum DeleteProductSpecificationResult 
    { Success, NotFound }
}