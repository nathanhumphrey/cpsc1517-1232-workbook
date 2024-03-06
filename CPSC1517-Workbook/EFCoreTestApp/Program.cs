using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WestWindSystem;
using WestWindSystem.BLL;
using WestWindSystem.DAL;
using WestWindSystem.Models;

IServiceCollection serviceCollection = new ServiceCollection().AddDbContext<WestWindContext>();
serviceCollection.WWBackEndDependencies(options => options.UseSqlServer("Data Source=.;TrustServerCertificate=True;Initial Catalog=WestWind;Integrated Security=True;Encrypt=True"));
ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
CustomerServices customerServices = serviceProvider.GetService<CustomerServices>();

string partialName = "Maria";

// Using services manager
Console.WriteLine($"First customer with partial name {partialName} is {customerServices.GetCustomerByContactName(partialName).ContactName}");

// Using context directly
/*
WestWindContext context = new WestWindContext();
context.Customers.Select(a => a);

foreach (var customer in context.Customers)
{
	Console.WriteLine(customer.ContactName);
}
*/
