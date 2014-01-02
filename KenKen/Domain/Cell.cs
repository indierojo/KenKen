namespace Domain
{
    public class Cell
    {
        public Cell(ushort group, ushort? value = null)
        {
            Group = group;
            Value = value;
        }
        public ushort Group { get; private set; }

        public ushort? Value { get; set; }
    }
}
