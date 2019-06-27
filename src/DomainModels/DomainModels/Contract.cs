using System;

namespace Tripstore.DomainModels
{
    public class Contract
    {
        public Contract(string name, MobileNumber mobileNumber)
        {
            if (name == "")
                throw new ArgumentException();

            this.Name = name;
            this.MobileNumber = mobileNumber;
        }

        public string Name { get; }
        
        public MobileNumber MobileNumber { get; set; }
    }
}