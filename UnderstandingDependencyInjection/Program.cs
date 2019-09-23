using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace UnderstandingDependencyInjection
{
    // STEP 1: Define an interface.
    /// <summary>
    /// Defines how a user is notified. 
    /// </summary>
    public interface INotifier
    {
        void Send(string from, string to, string subject, string body);
    }

    // STEP 2: Implement the interface
    /// <summary>
    /// Implementation of INotifier that notifies users by email.
    /// </summary>
    public class EmailNotifier : INotifier
    {
        public void Send(string from, string to, string subject, string body)
        {
            Debug.WriteLine(body);
        }
    }

    // STEP 3: Create a class that requires an implementation of the interface.
    public class ShoppingCart
    {
        readonly INotifier _notifier;
        private readonly string _content;

        public ShoppingCart(IServiceProvider serviceProvider, string content)
        {
            _notifier = serviceProvider.GetService<INotifier>();
            _content = content;
        }

        public void PlaceOrder(string customerEmail, string orderInfo)
        {
            _notifier.Send("admin@store.com", customerEmail, "Order Placed", $"Thank you for your order of {orderInfo}: {_content}");
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            var serviceProvider = new ServiceCollection().AddSingleton<INotifier, EmailNotifier>().AddSingleton<EmailNotifier>().BuildServiceProvider();
            // DI
            var shoppingCart = new ShoppingCart(serviceProvider, "Khanh");
            serviceProvider.GetService<EmailNotifier>().Send("","",",","OKDS");
            shoppingCart.PlaceOrder("","");
        }
    }
}