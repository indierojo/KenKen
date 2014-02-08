using System.Runtime.Serialization;

namespace Domain.Operations
{
    [DataContract(Name = "OperationType")]
    public enum OperationType
    {
        [EnumMember]
        NoOp,
        [EnumMember]
        Addition,
        [EnumMember]
        Subtraction,
        [EnumMember]
        Multiplication,
        [EnumMember]
        Division
    }
}