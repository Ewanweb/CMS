﻿namespace CMS.Domain.Admin.Orders;

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductImage { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public virtual Order Order { get; set; }
}