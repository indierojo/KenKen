using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class Subtraction : IOperation
    {
        public string Symbol
        {
            get { return "-"; }
        }

        public OperationType Type
        {
            get { return OperationType.Subtraction; }
        }

        public uint DoOperationOn(IEnumerable<ushort> values)
        {
            return values.OrderByDescending(i => i).Aggregate((a, b) => (ushort)(a - b));
        }
    }
}