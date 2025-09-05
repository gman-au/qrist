using System;
using Qrist.Interfaces;

namespace Qrist.Adapters.Todoist
{
    public class TodoistQristCodeBuilder : IQristCodeBuilder
    {
        private const string TodoistProvider = "TODOIST";

        public bool IsApplicable(string provider) =>
            string
                .Equals(
                    provider,
                    TodoistProvider,
                    StringComparison.InvariantCultureIgnoreCase
                );
    }
}