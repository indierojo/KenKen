using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class Multiplication : IOperation
    {
        public OperationType GetOperationType()
        {
            return OperationType.Multiplication;
        }

        public uint DoOperationOn(IEnumerable<ushort> values)
        {
            return values.Aggregate((a, b) => (ushort)(a * b));
        }

        public string GetSymbol()
        {
            return "x";
        }
    }
}