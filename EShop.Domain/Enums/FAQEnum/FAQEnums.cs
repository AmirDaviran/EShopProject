
namespace EShop.Domain.Enums.FAQEnum
{
    public enum OperationResult
    {
        Success,
        NotFound,
        Failure
    }
    public enum CreateFAQCategoryResult
    {
        Success,
        Failure
    }

    public enum UpdateFAQCategoryResult
    {
        Success,
        Failure,
        NotFound
    }

    public enum DeleteFAQCategoryResult
    {
        Success,
        Failure,
        NotFound
    }
}
