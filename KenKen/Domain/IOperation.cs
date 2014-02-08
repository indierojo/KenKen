using System.Collections.Generic;
using Domain.Operations;

namespace Domain
{
    public interface IOperation
    {
        string Symbol { get; }
        OperationType Type { get; }
        uint ApplyOperationTo(IEnumerable<ushort> values);

    }
}