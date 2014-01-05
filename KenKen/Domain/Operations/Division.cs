using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class Division : IOperation
    {
        public string Symbol
        {
            get { return "/"; }
        }

        public OperationType Type
        {
            get { return OperationType.Division; }
        }

        public uint DoOperationOn(IEnumerable<ushort> values)
        {
            return values.OrderByDescending(i => i).Aggregate((a, b) => (ushort)(a / b));
        }
    }
}