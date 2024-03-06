﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//***************************************************************
// Required namespaces
using WestWindSystem.DAL;
using WestWindSystem.Models;
//***************************************************************

namespace WestWindSystem.BLL
{
	public class CustomerServices
	{
		// Setup the context connection variable and constructor
		private readonly WestWindContext _context;

		/// <summary>
		/// Constructor to create an instance with the registered context.
		/// The internal keyword prevents access to the constructor from outside.
		/// the assembly. This prevents external projects/assemblies from 
		/// creating a new services object directly.
		/// </summary>
		/// <param name="context">The required WestWindContex</param>
		internal CustomerServices(WestWindContext context)
		{
			_context = context;
		}

		// Create a query method to retrieve a customer
		// This method will be called from the PL of the web application
		// This method will become part of the class library's public interface

		/// <summary>
		/// Retrieves a customer by id from the database
		/// </summary>
		/// <param name="id">Id of the customer to retrieve</param>
		/// <returns>A Customer if found, null otherwise</returns>
		public Customer? GetCustomerById(string id)
		{
			Customer? customer = _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
			return customer;
		}

		/// <summary>
		/// Retrieves a customer by contact name from the database
		/// </summary>
		/// <param name="name">Partial name of the customer to retrieve</param>
		/// <returns>A Customer if found, null otherwise</returns>
		public Customer? GetCustomerByContactName(string name)
		{
			Customer? customer = _context.Customers.Where(c => c.ContactName.Contains(name)).FirstOrDefault();
			return customer;
		}
	}
}