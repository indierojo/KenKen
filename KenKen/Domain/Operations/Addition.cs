using System.Collections.Generic;
using System.Linq;

namespace Domain.Operations
{
    public class Addition : IOperation
    {
        public string Symbol
        {
            get { return "+"; }
        }

        public OperationType Type
        {
            get { return OperationType.Addition; }
        }

        public uint DoOperationOn(IEnumerable<ushort> values)
        {
            return values.Aggregate((a, b) => (ushort)(a + b));
        }
    }
}