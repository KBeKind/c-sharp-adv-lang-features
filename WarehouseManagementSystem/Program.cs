
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;
using static WarehouseManagementSystem.Business.OrderProcessor;

var order = new Order
{
	LineItems = new[]
	{
		new Item { Name = "PS1", Price = 50 },
		new Item { Name = "PS2", Price = 60 },
		new Item { Name = "PS4", Price = 70 },
		new Item { Name = "PS5", Price = 80 }
	}
};

var isReadyForShipment = (Order order) =>
{
	return order.IsReadyForShipment;
};


bool SendMessageToWarehouse(Order order)
{
	Console.WriteLine($"Please pack the order {order.OrderNumber}");
	return true;
}

var processor = new OrderProcessor
{
	OnOrderInitialized = SendMessageToWarehouse,
	OnOrderTest = OrderTestMethod
};

//var onCompleted = (Order order) =>
//{
//	Console.WriteLine($"Processed {order.OrderNumber}");
//};

OrderInitialized OrderTestMethod()
{
	Console.WriteLine("Order Test Method");
	return SendMessageToWarehouse;
}

//processor.OnOrderTest()?.Invoke(order);
//processor.OnOrderTest()(order);



void SendConfirmationEmail(Order order)
{
	Console.WriteLine($"Order Confirmation Email for order {order.OrderNumber}");
}

void LogOrderProcessCompleted(Order order)
{
	Console.WriteLine($"Order Process Completed for order {order.OrderNumber}");
}

void UpdateStock(Order order)
{
	Console.WriteLine($"Stock updated after order {order.OrderNumber}");
}


OrderProcessor.ProcessCompleted chain = SendConfirmationEmail;
chain += LogOrderProcessCompleted;
chain += UpdateStock;
processor.Process(order, chain);

Console.WriteLine("******************");

chain -= LogOrderProcessCompleted;
processor.Process(order, chain);


//OrderProcessor.ProcessCompleted chain2 = One;
//chain2 += Two;
//chain2 += Three;

//void One(Order order) => Console.WriteLine("One");
//void Two(Order order) => Console.WriteLine("Two");
//void Three(Order order) => Console.WriteLine("Three");
