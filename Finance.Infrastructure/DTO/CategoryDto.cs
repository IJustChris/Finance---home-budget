namespace Finance.Infrastructure.DTO
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public int UserId { get; set; }
        public string IconName { get; set; }
        public string ColorHex { get; set; }
        public string Name { get; set; }
        public int? BudgetId { get; set; }

        public bool isBasic { get; set; }
        public bool IsSubcategory { get; set; }
    }
}
