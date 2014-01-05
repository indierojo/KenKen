using System.Runtime.Serialization;

namespace Domain
{
    [DataContract]
    public class GroupDefinition
    {
        public GroupDefinition(ushort groupNumber, IOperation operation, ushort expectedTotal)
        {
            GroupNumber = groupNumber;
            ExpectedTotal = expectedTotal;
            Operation = operation;
            Symbol = Operation.Symbol;
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

        public IOperation Operation { get; private set; }

        public override string ToString()
        {
            return ExpectedTotal + Operation.Symbol;
        }
    }
}
