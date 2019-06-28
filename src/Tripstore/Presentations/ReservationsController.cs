using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Tripstore
{
    public class ReservationsController
    {
        public ReservationsController()
        {
        }

        public ActionResult Post(MakeReservation makeReservation)
        {
            var numberParts = makeReservation.MobileNumber.Split('-');
            if (numberParts.Length != 3
                || numberParts[0] != "010"
                || numberParts[1].Length != 4
                || numberParts[2].Length != 4)
            {
                return new BadRequestResult();
            }

            string filePath = $"../../../../../{makeReservation.MobileNumber}+{makeReservation.Destination}.json";
            if (File.Exists(filePath))
            {
                return new ConflictResult();
            }

            File.WriteAllText(
                filePath,
                JsonConvert.SerializeObject(makeReservation));

            return new OkResult();
        }
    }
}