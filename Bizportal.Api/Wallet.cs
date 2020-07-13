using System;

namespace Bizportal.Api
{
    public class Wallet : BaseEntity
    {
        public decimal Amount { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
