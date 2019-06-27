using System;
using Microsoft.AspNetCore.Mvc;
using Tripstore.DomainModels;

namespace Tripstore
{
    public class SmsController
    {
        public SmsController(ISmsService smsService)
        {
            this.SmsService = smsService;
        }

        public ISmsService SmsService { get; }

        public ActionResult Post(SendSms sendSms)
        {
            try
            {
                var mobileNumber = MobileNumber.Parse(sendSms.MobileNumber);
                this.SmsService.Send(mobileNumber, sendSms.Message);
                return new OkResult();
            }
            catch(ArgumentException)
            {
                return new BadRequestResult();
            }
            
        }
    }
}