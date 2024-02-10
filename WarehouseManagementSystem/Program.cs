
using System.Text.Json;
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;
using WarehouseManagementSystem.Domain.Extensions;
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

var order2 = new Order
{
	LineItems = new[]
	{
		new Item { Name = "P1", Price = 90 },
		new Item { Name = "P2", Price = 100 },
		new Item { Name = "P4", Price = 110 },
		new Item { Name = "P5", Price = 120 }
	}
};

var order3 = new Order
{
	LineItems = new[]
	{
		new Item { Name = "S1", Price = 20 },
		new Item { Name = "S2", Price = 30 },
		new Item { Name = "S4", Price = 40 },
		new Item { Name = "S5", Price = 50 }
	}
};



var isReadyForShipment = (Order order) =>
{
	return order.IsReadyForShipment;
};


//List<Order> orders = new List<Order>
//{
//	order,
//	order2,
//	order3,

//};

IEnumerable<Order> orders = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));


bool SendMessageToWarehouse(Order order)
{
	Console.WriteLine($"Please pack the order {order.OrderNumber}");
	return true;
}

var processor = new OrderProcessor
{
	//OnOrderInitialized = SendMessageToWarehouse,
	OnOrderInitialized = (order) =>  order.IsReadyForShipment,
	//OnOrderTest = OrderTestMethod
};

//var onCompleted = (Order order) =>
//{
//	Console.WriteLine($"Processed {order.OrderNumber}");
//};

//OrderInitialized OrderTestMethod()
//{
//	Console.WriteLine("Order Test Method");
//	return SendMessageToWarehouse;
//}

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


//OrderProcessor.ProcessCompleted chain = SendConfirmationEmail;
Action<Order> chain = SendConfirmationEmail;

chain += LogOrderProcessCompleted;
chain += UpdateStock;
//processor.Process(order, chain);

//Console.WriteLine("******************");

chain -= LogOrderProcessCompleted;
//processor.Process(order, chain);

//Console.WriteLine("******************");

//OrderProcessor.ProcessCompleted onComplete = (order) => { Console.WriteLine("ORDER COMPLETE");  };
Action<Order> onComplete = (order) => { Console.WriteLine("ORDER COMPLETE"); };



Func<Order, bool> onComplete2 = (order) => { return order.IsReadyForShipment; };

var processor2 = new OrderProcessor
{
	OnOrderInitialized = onComplete2,
};




//processor.Process(order, onComplete);
//Console.WriteLine("******************");

//OrderProcessor.ProcessCompleted chain2 = (order) => { Console.WriteLine("One"); };
Action<Order> chain2 = (order) => { Console.WriteLine("One"); };

chain2 += (order) => { Console.WriteLine("Two"); };
chain2 += (order) => { Console.WriteLine("Three"); };

//chain2(order);
//Console.WriteLine("******************");


//chain2 += Two;
//chain2 += Three;

//void One(Order order) => Console.WriteLine("One");
//void Two(Order order) => Console.WriteLine("Two");
//void Three(Order order) => Console.WriteLine("Three");


Func<Order, bool> aFunc = (Order order) => true;
Func<Order, bool> aFunc2 = SendMessageToWarehouse;
Func<Order, bool> aFunc3 = (Order order) => order.IsReadyForShipment;

// Console.WriteLine("******************");


var processor3 = new OrderProcessor { };

var onCompleted = (Order order) =>
{
	Console.WriteLine($"Processed {order.OrderNumber}");
};


processor3.OrderCreated += (sender, args) =>
{
	Thread.Sleep(1000);
	Console.WriteLine("1");
};
processor3.OrderCreated += Log;

void Log(object sender, EventArgs args)
{
    Console.WriteLine("Log method call");
    Console.WriteLine(args.GetType().Name);
}



//processor3.Process(order);


//processor3.Process(order, 30);


//Console.WriteLine("******************");

//foreach (var item in order.LineItems.Find(item => item.Price > 60))
//	{
//	Console.WriteLine($"{item.Name}: {item.Price}");
//	}
//Console.WriteLine("******************");

//Console.WriteLine(order.GenerateReport());
//Console.WriteLine("******************");

//Console.WriteLine(order.GenerateReport("Mikey"));
//Console.WriteLine("******************");

var subset = new { order.OrderNumber, order.Total, AveragePrice =order.LineItems.Average(item => item.Price) };

//Console.WriteLine($"{subset.AveragePrice}");
//Console.WriteLine("******************");

var instance = new {
	Total = 100,
	AmountOfItems = 10
};


var instance2 = new
{
	Total = 100,
	AmountOfItems = 10
};


Console.WriteLine(instance.Equals(instance2));


// LINQ METHOD SYNTAX
var totals = orders.Select(order => new { order.Total });

// LINK QUERY SYNTAX
var totals2 = from anOrder in orders select new { anOrder.Total };



processor.Process(orders);