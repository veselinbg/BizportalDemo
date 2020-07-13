using System.Collections.Generic;

namespace Bizportal.WebApi.Resources
{
    public class ErrorResource
    {
        public IList<string> Messages { get; private set; }

        public ErrorResource(IList<string> messages)
        {
            this.Messages = messages ?? new List<string>();
        }

        public ErrorResource(string message)
        {
            this.Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                this.Messages.Add(message);
            }
        }
    }
}
