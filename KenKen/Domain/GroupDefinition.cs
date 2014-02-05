using System.Runtime.Serialization;
using Domain.Operations;

namespace Domain
{
    [DataContract]
    public class GroupDefinition
    {

        private static ushort _counter;

        public GroupDefinition(ushort groupNumber, IOperation operation, ushort expectedTotal)
        {
            Group = groupNumber;
            ExpectedTotal = expectedTotal;
            Operation = operation;
            Symbol = Operation.Symbol;
        }

        public GroupDefinition() : this(_counter++, new NoOp(), 0)
        {
        }

        [DataMember]
        public string Symbol 
        {
            get { return Operation.Symbol; }
            private set { /* do nothing. */ }
        }

        [DataMember]
        public ushort Group { get; private set; }
        
        [DataMember]
        public ushort ExpectedTotal { get; private set; }

        public IOperation Operation { get;  set; }

        public override string ToString()
        {
            return ExpectedTotal + Operation.Symbol;
        }
    }
}
