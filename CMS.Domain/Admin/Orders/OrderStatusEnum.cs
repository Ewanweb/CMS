namespace CMS.Domain.Admin.Orders;

public enum OrderStatus 
{
    pending,
    processing,
    shipped,
    delivered,
    canceled
}