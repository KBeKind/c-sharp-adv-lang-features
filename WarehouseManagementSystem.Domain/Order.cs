namespace WarehouseManagementSystem.Domain
{
    public class Order
    {
        public Guid OrderNumber { get; init; }
        public ShippingProvider ShippingProvider { get; init; }
        public bool IsReadyForShipment { get; set; } = true;
        public IEnumerable<Item> LineItems { get; set; }

		public decimal Total => LineItems?.Sum(i => i.Price) ?? 0;

		public Order()
        {
            OrderNumber = Guid.NewGuid();
			LineItems = new List<Item>(); // Initialize LineItems to avoid null reference
		}

		public void Deconstruct(out decimal total, out bool ready)
		{
			total = Total;
			ready = IsReadyForShipment;
		}

        public void Deconstruct(out decimal total, out bool ready, out IEnumerable<Item> items)
        {
			total = Total;
			ready = IsReadyForShipment;
            items = LineItems;
		}


	}

    public class ProcessedOrder : Order { }

    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }


  

}