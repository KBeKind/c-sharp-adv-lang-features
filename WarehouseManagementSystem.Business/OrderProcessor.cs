using System.Transactions;
using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {

        public delegate bool OrderInitialized(Order order);
        public delegate OrderInitialized OrderTest();
        public delegate void ProcessCompleted(Order order);

        public OrderInitialized OnOrderInitialized { get; set; }
        public OrderTest OnOrderTest { get; set; }

        

        private void Initialize(Order order)
		{
            ArgumentNullException.ThrowIfNull(order);
            if(OnOrderInitialized?.Invoke(order) == false)
            {
                throw new Exception($"Could not initialize order {order.OrderNumber}");
            }
            
        }

        public void Process(Order order, 
            ProcessCompleted onCompleted = default)
        {
            Initialize(order);
            onCompleted?.Invoke(order);
        }
    }
}