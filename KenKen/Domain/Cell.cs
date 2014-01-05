using System.Runtime.Serialization;

namespace Domain
{
    [DataContract]
    public class Cell
    {
        public Cell(ushort group, ushort? value = null)
        {
            Group = group;
            Value = value;
        }
        [DataMember]
        public ushort Group { get; private set; }
        [DataMember]
        public ushort? Value { get; set; }
    }
}
