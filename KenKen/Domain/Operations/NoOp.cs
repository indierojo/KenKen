using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class NoOp : IOperation
    {
        public OperationType GetOperationType()
        {
            return OperationType.NoOp;
        }

        public uint DoOperationOn(IEnumerable<ushort> values)
        {
            return values.First();
        }

        public string GetSymbol()
        {
            return "";
        }
    }
}