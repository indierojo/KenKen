using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class Multiplication : IOperation
    {
        public string Symbol
        {
            get { return "x"; }
        }

        public OperationType Type
        {
            get { return OperationType.Multiplication; }
        }

        public uint ApplyOperationTo(IEnumerable<ushort> values)
        {
            return values.Aggregate((a, b) => (ushort)(a * b));
        }
    }
}