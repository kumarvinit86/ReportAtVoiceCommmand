namespace Reports.Persistence.Entities;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string ?Discription { get; set; } = string.Empty;
    public decimal? TotalAmount { get; set; }
    public DateTime ?OrderDate { get; set; }
}
