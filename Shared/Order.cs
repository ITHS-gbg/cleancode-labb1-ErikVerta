namespace Shared;

public class Order : EntityBase
{
    public List<OrderItem> OrderItems{ get; set; }
    public Customer Customer { get; set; }
    public DateTime ShippingDate { get; set; }
}