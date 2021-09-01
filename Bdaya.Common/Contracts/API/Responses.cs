namespace Bdaya.Responses;

public static class Responses
{
    public static class Delete
    {
        public static readonly IApiResult Success = IApiResult.Success("Deleted Successfuly");
        public static readonly IApiResult Fail = IApiResult.Fail("Failed to delete");
    }

    public static class Create
    {
        public static readonly IApiResult Success = IApiResult.Success("Created Successfuly");
        public static readonly IApiResult Fail = IApiResult.Fail("Failed to Create");
    }
    public static class Update
    {
        public static readonly IApiResult Success = IApiResult.Success("Updated Successfuly");
        public static readonly IApiResult Fail = IApiResult.Fail("Failed to update");
    }

    public static class Transaction
    {
        public static readonly IApiResult Success = IApiResult.Success("Success Process");
        public static readonly IApiResult Fail = IApiResult.Fail("Fail of Process");
    }

}
