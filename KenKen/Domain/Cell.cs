namespace Domain
{
    public class Cell
    {
        public Cell(ushort x, ushort y, ushort group, ushort? value = null)
        {
            X = x;
            Y = y;
            Group = group;
            Value = value;
        }

        public ushort X { get; private set; }
        public ushort Y { get; private set; }
        public ushort Group { get; private set; }

        public ushort? Value { get; set; }
    }
}
