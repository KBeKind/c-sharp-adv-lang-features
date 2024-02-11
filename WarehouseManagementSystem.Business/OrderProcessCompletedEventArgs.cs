using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
	public class OrderProcessCompletedEventArgs : EventArgs
	{
		public Order? Order { get; set; }
	}
}
