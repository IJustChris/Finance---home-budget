using System.Collections.Generic;

namespace Finance.Infrastructure.DTO
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ColorHex { get; set; }
        public string IconName { get; set; }
        public decimal BudgetLimit { get; set; }
        public IEnumerable<int> CategoriesId { get; set; }
        public decimal ActualExpenditure { get; set; }
    }
}
