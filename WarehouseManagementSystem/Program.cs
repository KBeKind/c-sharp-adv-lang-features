
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
	},
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


//Console.WriteLine(instance.Equals(instance2));

// LINQ METHOD SYNTAX
var totals = orders.Select(order => new { order.Total });

// LINK QUERY SYNTAX
var totals2 = from anOrder in orders select new { anOrder.Total };


//processor.Process(orders);


//Console.WriteLine("******************");

var aTuple = (order.OrderNumber, order.LineItems, Sum: order.LineItems.Sum(item => item.Price) );

//Console.WriteLine(aTuple);

(Guid orderNumber, IEnumerable<Item> items, decimal Sum) aTuple2 = (order.OrderNumber, order.LineItems, order.LineItems.Sum(item => item.Price));

//Console.WriteLine(aTuple2);

//Console.WriteLine("******************");

//Console.WriteLine($"order number:{aTuple.OrderNumber}, total:{aTuple.Sum}");

//Console.WriteLine($"order number:{aTuple2.orderNumber}, total:{aTuple2.Sum}");

var aAnonymousType = new { order.OrderNumber, order.LineItems, Sum = order.LineItems.Sum(item => item.Price) };




//Console.WriteLine("******************");

var json = JsonSerializer.Serialize(aTuple, options: new() { IncludeFields = true });

//Console.WriteLine(json);

//(Guid orderNumber, IEnumerable<Item> items, decimal Sum) = (order.OrderNumber, order.LineItems, order.LineItems.Sum(item => item.Price));

//(var orderNumber, var items, var Sum) = (order.OrderNumber, order.LineItems, order.LineItems.Sum(item => item.Price));

//var (orderNumber, items, Sum) = (order.OrderNumber, order.LineItems, order.LineItems.Sum(item => item.Price));

//Guid orderNumber;
//decimal sum;

//(orderNumber, var items, sum) = (order.OrderNumber, order.LineItems, order.LineItems.Sum(item => item.Price));

//Console.WriteLine($"order number:{orderNumber}, items:{items}, total:{sum}");

//(orderNumber, _, sum) = (order.OrderNumber, order.LineItems, order.LineItems.Sum(item => item.Price));

//Console.WriteLine($"order number:{orderNumber}, total:{sum}");


//Console.WriteLine("******************");


var result = processor.Process(orders);
//var result2 = processor.Process(orders);
//var result2 = result with { total = 10 };

//Console.WriteLine("******************");

//Console.WriteLine($"Are these equal? {result == result2}");

//Console.WriteLine($"ORDER NUMBER:{result.orderNumber}, ITEM COUNT:{result.itemCount}, TOTAL:{result.total}, ITEMS:{result.items}");

//Console.WriteLine("******************");

//var (id, items, total, _) = processor.Process(orders);

//Action<(Guid id, int itemCount)> log = (tuple) =>
//{
//	Console.WriteLine(tuple.id.ToString());
//};
//onsole.WriteLine("******************");

//Console.WriteLine(result.GenerateReport());

//Console.WriteLine("******************");

//(Guid, int, decimal, IEnumerable<Item>) aTuple3 = (Guid.Empty, 0, 0m, Enumerable.Empty<Item>());

//Console.WriteLine(aTuple3.GenerateReport());

//Console.WriteLine("******************");


//foreach (var summary in result.ToList())
//{
//    Console.WriteLine(summary.GenerateReport());
//	Console.WriteLine("******************");

//}

//Console.WriteLine("******************");

//foreach (var summary in processor.Process(orders))
//{
//    Console.WriteLine(summary.GenerateReport());
//	Console.WriteLine("******************");

//}


//var (total, isReady) = order;

//var (total, isReady, items) = order;

//var (total, isReady, _) = order;


//Console.WriteLine($"TOTAL:{total}, READY:{isReady}");

//Console.WriteLine($"TOTAL:{total}, READY:{isReady}, ITEMS:{items}");

//Console.WriteLine("******************");


//if (order is (total: > 0, true))
//{
//	Console.WriteLine("Order is ready");
//}


Console.WriteLine("******************");


//var (orderNumber, total, items, averagePrice) = order;

//Console.WriteLine($"ORDER NUMBER:{orderNumber}, TOTAL:{total}, ITEMS:{items}, AVERAGE PRICE:{averagePrice}");


//var aDictionary = new Dictionary<string, Order>();

//foreach (var (orderNumber, theOrder) in aDictionary)
//{

//}