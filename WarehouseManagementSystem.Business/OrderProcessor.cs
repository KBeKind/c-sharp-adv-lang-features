using System.Numerics;
using System.Runtime.CompilerServices;
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {

        //public delegate bool OrderInitialized(Order order);
        //public delegate OrderInitialized OrderTest();
        //public delegate void ProcessCompleted(Order order);

        //public OrderInitialized OnOrderInitialized { get; set; }
        //public OrderTest OnOrderTest { get; set; }

        
        public Func<Order, bool> OnOrderInitialized { get; set; }


        public event EventHandler<OrderProcessCompletedEventArgs> OrderProcessCompleted;
        protected virtual void OnOrderProcessCompleted(OrderProcessCompletedEventArgs args)
        {
            OrderProcessCompleted?.Invoke(this, args);
        }


        public event EventHandler<OrderCreatedEventArgs> OrderCreated;
        protected virtual void OnOrderCreated(OrderCreatedEventArgs args)
        {
            // Console.WriteLine for test only
			//Console.WriteLine(args.NewTotal);

			OrderCreated?.Invoke(this, args);
        }


        private void Initialize(Order order)
		{
            ArgumentNullException.ThrowIfNull(order);
            if(OnOrderInitialized?.Invoke(order) == false)
            {
                throw new Exception($"Could not initialize order {order.OrderNumber}");
            }
            
        }

        public void Process(Order order)
        {
            Initialize(order);

            OnOrderCreated(new OrderCreatedEventArgs
            {
                Order = order,
                OldTotal = 100,
                NewTotal = 80
            });

            OnOrderProcessCompleted(new OrderProcessCompletedEventArgs
            {
                Order = order
            });
        }

		public void Process(Order order, decimal discount)
		{
			Initialize(order);

            OnOrderCreated(new OrderCreatedEventArgs
            {
                Order = order,
                OldTotal = 100,
                NewTotal = 100 - discount
            });

            

            OnOrderProcessCompleted(new OrderProcessCompletedEventArgs
			{
				Order = order
			});
		}


        public IEnumerable<(Guid orderNumber, int itemCount , decimal total, IEnumerable<Item> items)> Process(IEnumerable<Order> orders)
        {
            // AS ANON TYPE
            //var summaries = orders.Select(order =>
            //{
            //    return new {

            //        Order = order.OrderNumber,
            //        Items = order.LineItems.Count(),
            //        Total = order.LineItems.Sum(item => item.Price),
            //        LineItems = order.LineItems

            //    };
            //});

            // AS TUPLE
            var summaries = orders.Select(order =>
            {
                return (
                    Order : order.OrderNumber,
                    Items : order.LineItems.Count(),
                    Total : order.LineItems.Sum(item => item.Price),
                    LineItems : order.LineItems
                );
            });

            var orderedSummaries = summaries.OrderBy(summary => summary.Total);

            var aSummary = orderedSummaries.First();

            var aSummaryWithTax = aSummary with {
                Total = aSummary.Total * 1.25m
            };

            // IF USING AN ANON TYPE THEN IF IT IS A COLLECTION IT IS PASSED BY REFERENCE
            // STRINGS AND NUMBERS ARE PASSED BY VALUE
            var item = aSummaryWithTax.LineItems.First();
            item.Name = "testname";

            //Console.WriteLine($"original: {aSummary.LineItems.First().Name} , copy: {item.Name}");

            //        foreach (var summary in orderedSummaries)
            //        {
            //Console.WriteLine("**********************");
            //Console.WriteLine($"Order {summary.Order} has {summary.Items} items and a total of {summary.Total}");

            //        }

            // return aSummaryWithTax;
            return orderedSummaries;
        }
	}
}


