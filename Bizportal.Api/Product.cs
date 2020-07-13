using System;
using System.Collections.Generic;

namespace Bizportal.Api
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
