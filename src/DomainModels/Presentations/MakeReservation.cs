using System;

namespace Tripstore
{
    public class MakeReservation
    {
        public string Name { get; set; }
        public int NumberOfPeople { get; set; }
        public string Destination { get; set; }
        public string MobileNumber { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}