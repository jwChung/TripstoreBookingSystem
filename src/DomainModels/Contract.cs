using System;

namespace Tripstore.DomainModels
{
    public class Contract
    {
        public Contract(string name, string mobileNumber)
        {
            if (name == "")
                throw new ArgumentException();

            var numberParts = mobileNumber.Split('-');
            if (numberParts.Length != 3 
                || numberParts[0] != "010"
                || numberParts[1].Length != 4
                || numberParts[2].Length != 4)
                throw new ArgumentException();

            this.Name = name;
            this.MobileNumber = mobileNumber;
        }

        public string Name { get; }

        public string MobileNumber { get; set; }
    }
}