using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain
{
	public record Customer(string Firstname, string Lastname)
	{
		public string FullName => $"{Firstname} {Lastname}";
		public Address Address { get; init; }

	}

	public record Address(string Street, string City, string State, string ZipCode)
	{
		public string FullAddress => $"{Street}, {City}, {State}, {ZipCode}";
	}


	public record PriorityCustomer(string Firstname, string Lastname) : Customer(Firstname, Lastname);

}
