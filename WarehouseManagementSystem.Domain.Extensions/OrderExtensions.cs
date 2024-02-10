using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace WarehouseManagementSystem.Domain.Extensions
{
	public static class OrderExtensions
	{
		public static string GenerateReport(this Order order)
		{
			return $"ORDER REPORT ({order.OrderNumber}){Environment.NewLine}" +
				$"Items: {order.LineItems.Count()}{Environment.NewLine}" +
				$"Total: {order.Total}{Environment.NewLine}";
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
