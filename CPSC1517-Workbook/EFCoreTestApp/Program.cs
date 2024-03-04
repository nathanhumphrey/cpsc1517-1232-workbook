using WestWindSystem;
using WestWindSystem.Models;

WestWindContext context = new WestWindContext();

context.Customers.Select(a => a);

foreach (var customer in context.Customers)
{
	Console.WriteLine(customer.ContactName);
}