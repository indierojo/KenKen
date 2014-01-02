using System.Collections.Generic;
using Domain.Operations;

namespace Domain
{
    public interface IOperation
    {
        OperationType GetOperationType();
        uint DoOperationOn(IEnumerable<ushort> values);
        string GetSymbol();
    }
}