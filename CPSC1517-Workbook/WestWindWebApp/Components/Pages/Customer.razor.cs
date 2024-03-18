using Microsoft.AspNetCore.Components;
using WestWindSystem.BLL;

namespace WestWindWebApp.Components.Pages
{
    public partial class Customer
    {
		[Parameter]
		public string? CustomerId { get; set; }

		// Have to use the full-qualified name if the class is named same as the component class
		public WestWindSystem.Models.Customer? CurrentCustomer { get; set; }

		// The [Inject] attribute marks a property as being injected by the
		// appliction service provider, which has been updated in the
		// Program.cs file.
		[Inject]
		CustomerServices? CustomerServices { get; set; }

		protected override void OnInitialized()
		{
			if (CustomerId != null && CustomerServices != null)
			{
				CurrentCustomer = CustomerServices.GetCustomerById(CustomerId);
			}

			base.OnInitialized();
		}
	}
}
