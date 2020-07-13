using System;
using System.Collections.Generic;
using System.Text;

namespace Bizportal.Api
{
    public class Order : BaseEntity
    {
        public Guid ClientId { get; set; }

        public Client Client { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
