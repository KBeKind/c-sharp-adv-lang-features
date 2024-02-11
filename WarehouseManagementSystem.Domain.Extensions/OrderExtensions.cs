using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace WarehouseManagementSystem.Domain.Extensions
{
	public static class OrderExtensions
	{
		public static string GenerateReport(this Order order)
		{

			var status = order switch
			{
				//(>100, true) => "High Priority Order",
				//(_, true) => "Order is ready",
				//(_, false) => "Order is not ready",
				//_ => "Order is null!"

				//( > 100, true) match => "High Priority Order",
				//{ Total: > 50 and <= 100 } => "Priority Order",
				//{ Total: > 50 and <= 100 and not 75 } => "Priority Order",
				//(>50 and <= 100, true) => "Priority Order",
				//(var total, true) => $"Order is ready {total}",
				//(_, false) => "Order is not ready",
				//_ => "Order is null!"

				ShippedOrder => "Shipped Order",
				CancelledOrder => "Cancelled Order",
				//PriorityOrder or ((total: > 100, true) and not (ShippedOrder or CancelledOrder)) => "High Priority Order",
				PriorityOrder or (total: > 100, true) => "High Priority Order",
				{ Total: > 50 and <= 100 } => "Priority Order",
				(var total, true) => $"Order is ready {total}",
				(_, false) => "Order is not ready",
				_ => "Order is null!"



			};

			var shippingProviderStatus = (order.ShippingProvider, order.LineItems.Count(), order.IsReadyForShipment)
				switch
				{
					(_, > 10, true) => "Multiple Shipments",
					(SwedishPostalServiceShippingProvider, 1, _) => "Manual pickup required",
					(_, _, true) => "Ready for shipment",
					_ => "Not ready for shipment",
				};

				return $"ORDER REPORT ({order.OrderNumber}){Environment.NewLine}" +
					$"Items: {order.LineItems.Count()}{Environment.NewLine}" +
					$"Total: {order.Total}{Environment.NewLine}" +
					$"{status} { shippingProviderStatus}";
				}

		public static string GenerateReport(this Order order, string recipient)
		{
			return $"ORDER REPORT ({order.OrderNumber}){Environment.NewLine}" +
				$"Items: {order.LineItems.Count()}{Environment.NewLine}" +
				$"Total: {order.Total}{Environment.NewLine}" +
				$"Recipient: {recipient}{Environment.NewLine}";
		}


		public static string GenerateReport(this (Guid, int, decimal, IEnumerable<Item>) order)
		{
			return $"ORDER REPORT ({order.Item1}){Environment.NewLine}" +
				$"Item Count: {order.Item2}{Environment.NewLine}" +
				$"Total: {order.Item3}{Environment.NewLine}";
		}


		public static void Deconstruct(this Order order, out Guid orderNumber, out decimal total, out IEnumerable<Item> lineItems, out decimal averagePrice)
		{
			orderNumber = order.OrderNumber;
			total = order.Total;
			lineItems = order.LineItems;
			averagePrice = order.LineItems.Average(i => i.Price);
		}
	
	}
}
