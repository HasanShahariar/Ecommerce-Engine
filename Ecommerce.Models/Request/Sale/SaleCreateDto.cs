
namespace Ecommerce.Models.Request.Sale
{
    public class SaleCreateDto
    {
        public int Id { get; set; }  
        public DateTime SaleDate { get; set; }  
        public string Code { get; set; }
        public int CustomerId { get; set; } 
        public decimal TotalAmount { get; set; }  
        public decimal Discount { get; set; }  
        public decimal? TaxAmount { get; set; }  
        public decimal NetAmount { get; set; }  
        public string? Remarks { get; set; }
        public ICollection<SaleDetailCreateDto> SaleDetails { get; set; }
    }
}
