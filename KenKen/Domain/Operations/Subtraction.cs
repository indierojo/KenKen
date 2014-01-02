using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class Subtraction : IOperation
    {
        public OperationType GetOperationType()
        {
            return OperationType.Subtraction;
        }

        public uint DoOperationOn(IEnumerable<ushort> values)
        {
            return values.OrderByDescending(i => i).Aggregate((a, b) => (ushort)(a - b));
        }

        public string GetSymbol()
        {
            return "-";
        }
    }
}