﻿using System.Runtime.Serialization;

namespace Domain
{
    [DataContract]
    public class Cell
    {
        public Cell() { }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        [DataMember]
        public ushort Group { get; set; }
        [DataMember]
        public ushort? Value { get; set; }

        public int Y { get; set; }
        public int X { get; set; }
    }
}
