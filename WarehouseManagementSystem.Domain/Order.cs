namespace WarehouseManagementSystem.Domain
{
	public record Order(ShippingProvider ShippingProvider, IEnumerable<Item> LineItems, bool IsReadyForShipment = true)
	{
		public Guid OrderNumber { get; init; } = Guid.NewGuid();
		public decimal Total => LineItems?.Sum(i => i.Price) ?? 0;
		//public Order()
		//      {
		//          OrderNumber = Guid.NewGuid();
		//	LineItems = new List<Item>(); // Initialize LineItems to avoid null reference
		//}

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


	public record PriorityOrder(ShippingProvider ShippingProvider, IEnumerable<Item> LineItems, bool IsReadyForShipment = true) : Order(ShippingProvider, LineItems, IsReadyForShipment);


	public record ShippedOrder(
		ShippingProvider ShippingProvider,
		IEnumerable<Item> LineItems) : Order(ShippingProvider, LineItems, false)
	{
		public DateTime ShippedDate { get; set; }
	}

	public record CancelledOrder(
		ShippingProvider ShippingProvider,
		IEnumerable<Item> LineItems) : Order(ShippingProvider, LineItems, false)
	{
		public DateTime CancelledDate { get; set; }
	}



	public record ProcessedOrder(
		ShippingProvider ShippingProvider,
		IEnumerable<Item> LineItems,
		bool IsReadyForShipment = true) : Order(ShippingProvider, LineItems, IsReadyForShipment);



	public class Item
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public bool InStock { get; set; }
	}




}