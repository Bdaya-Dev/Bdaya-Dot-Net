namespace Bdaya.Responses;

public static class Responses
{
    public static class Delete
    {
        public static readonly ApiResult Success = ApiResult.Success("Deleted Successfuly");
        public static readonly ApiResult Fail = ApiResult.Fail("Failed to delete");
    }

    public static class Create
    {
        public static readonly ApiResult Success = ApiResult.Success("Created Successfuly");
        public static readonly ApiResult Fail = ApiResult.Fail("Failed to Create");
    }
    public static class Update
    {
        public static readonly ApiResult Success = ApiResult.Success("Updated Successfuly");
        public static readonly ApiResult Fail = ApiResult.Fail("Failed to update");
    }

    public static class Transaction
    {
        public static readonly ApiResult Success = ApiResult.Success("Success Process");
        public static readonly ApiResult Fail = ApiResult.Fail("Fail of Process");
    }

}
