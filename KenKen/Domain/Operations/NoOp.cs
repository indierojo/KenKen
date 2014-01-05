using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class NoOp : IOperation
    {
        public string Symbol
        {
            get { return ""; }
        }

        public OperationType Type
        {
            get { return OperationType.NoOp; }
        }

        public uint DoOperationOn(IEnumerable<ushort> values)
        {
            return values.First();
        }
    }
}