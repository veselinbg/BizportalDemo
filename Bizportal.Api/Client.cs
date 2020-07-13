using System;
using System.Collections.Generic;

namespace Bizportal.Api
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Guid WalletId { get; set; }

        public Wallet Wallet { get; set; }
    }
}
