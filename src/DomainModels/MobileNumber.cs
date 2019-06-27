using System;

namespace Tripstore.DomainModels
{
    public class MobileNumber
    {
        private MobileNumber(string value)
        {
            this.Value = value;
        }

        public string Value { get; }

        public static MobileNumber Parse(string value)
        {
            var numberParts = value.Split('-');
            if (numberParts.Length != 3
                || numberParts[0] != "010"
                || numberParts[1].Length != 4
                || numberParts[2].Length != 4)
                throw new ArgumentException();

            return new MobileNumber(value);
        }
    }
}