using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("The value is out of range and the valid input are between {0} - {1}", i_MinValue, i_MaxValue))
        {
            MaxValue = i_MaxValue;
            MinValue = i_MinValue;
        }

        public float MaxValue { get; }
        public float MinValue { get; }

    }
}
