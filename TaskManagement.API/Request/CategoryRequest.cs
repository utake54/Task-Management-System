namespace TaskManagement.API.Request
{
    public class AddCategoryRequest
    {
        public string Category { get; set; }
    }

    public class UpdateCategoryRequest : GetByIdCategoryRequest
    {
        public string Category { get; set; }
    }
    public class GetByIdCategoryRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryRequest : GetByIdCategoryRequest
    {

    }
    public class GetCategoryRequest
    {

    }
}
