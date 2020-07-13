using System.Collections.Generic;

namespace Bizportal.Api
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
