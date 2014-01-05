namespace Domain
{
    public class GroupDefinition
    {
        public GroupDefinition(ushort groupNumber, IOperation operation, ushort expectedTotal)
        {
            GroupNumber = groupNumber;
            ExpectedTotal = expectedTotal;
            Operation = operation;
        }

        public ushort GroupNumber { get; private set; }
        public IOperation Operation { get; private set; }
        public ushort ExpectedTotal { get; private set; }

        public override string ToString()
        {
            return ExpectedTotal + Operation.Symbol;
        }
    }
}
