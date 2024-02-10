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
			Console.WriteLine(args.NewTotal);

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

	}
}


