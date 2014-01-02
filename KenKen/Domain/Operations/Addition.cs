using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class Addition : IOperation
    {
        public OperationType GetOperationType()
        {
            return OperationType.Addition;
        }

        public uint DoOperationOn(IEnumerable<ushort> values)
        {
            return values.Aggregate((a, b) => (ushort)(a + b));
        }

        public string GetSymbol()
        {
            return "+";
        }
    }
}