namespace EShop.Domain.Enums.SpecificationEnum;


    #region Product Enums
    public enum CreateSpecificationResult
    {
        Success,
        InvalidInput,
        NotFound,
    }

    public enum UpdateSpecificationResult
    {
        Success,
        NotFound,
        InvalidInput
    }

    public enum DeleteSpecificationResult
    {
        Success,
        NotFound
    }

    #endregion
