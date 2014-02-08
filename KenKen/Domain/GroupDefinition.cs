using System.Runtime.Serialization;
using Domain.Operations;

namespace Domain
{
    [DataContract]
    public class GroupDefinition
    {
        public GroupDefinition(ushort groupNumber, OperationType operationType, ushort expectedTotal)
        {
            Group = groupNumber;
            ExpectedTotal = expectedTotal;
            Operation = GetFor(operationType);
        }

        [DataMember]
        public OperationType OperationType
        {
            get { return Operation.Type; }
            set { Operation = GetFor(value); }
        }

        private IOperation GetFor(OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.Addition:
                {
                    return new Addition();
                }
                case OperationType.Division:
                {
                    return new Division();
                }
                case OperationType.Multiplication:
                {
                    return new Multiplication();
                }
                case OperationType.NoOp:
                {
                    return new NoOp();
                }
                case OperationType.Subtraction:
                {
                    return new Subtraction();
                }
            }

            return null;
        }

        [DataMember]
        public string Symbol 
        {
            get { return Operation.Symbol; }
// ReSharper disable once ValueParameterNotUsed
            private set { /* do nothing. */ }
        }

        [DataMember]
        public ushort Group { get; private set; }
        
        [DataMember]
        public ushort ExpectedTotal { get; set; }

        public IOperation Operation { get;  set; }

        public override string ToString()
        {
            return ExpectedTotal + Operation.Symbol;
        }
    }
}
