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
            if (makeReservation.MobileNumber == "000-1234-1234")
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