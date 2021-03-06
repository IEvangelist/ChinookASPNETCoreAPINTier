﻿using Chinook.Domain.Converters;
using Chinook.Domain.Responses;
using System.Collections.Generic;

namespace Chinook.Domain.Entities
{
    public class Customer : IConvertModel<Customer, CustomerResponse>
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? SupportRepId { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
        public Employee SupportRep { get; set; }

        public CustomerResponse Convert => new CustomerResponse
        {
            CustomerId = CustomerId,
            FirstName = FirstName,
            LastName = LastName,
            Company = Company,
            Address = Address,
            City = City,
            State = State,
            Country = Country,
            PostalCode = PostalCode,
            Phone = Phone,
            Fax = Fax,
            Email = Email,
            SupportRepId = SupportRepId
        };
    }
}