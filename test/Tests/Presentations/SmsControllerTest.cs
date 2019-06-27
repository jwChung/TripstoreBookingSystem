using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Tripstore.DomainModels;
using Xunit;

namespace Tripstore.Presentations
{
    public class SmsControllerTest
    {
        [Fact]
        public void PostWithValidDataReturnsOkResult()
        {
            // Arrange
            var sut = new SmsController(new SmsServiceMock());
            var sendSms = new SendSms
            {
                MobileNumber = "010-1234-1234",
                Message = "안녕하세요"
            };

            // Act
            ActionResult actual = sut.Post(sendSms);

            // Assert
            Assert.IsType<OkResult>(actual);
        }

        [Theory]
        [InlineData("000-1234-1234")]
        [InlineData("111-1234-1234")]
        public void PostWithInvalidMobileNumberReturnsBadRequest(string invalidMobileNumber)
        {
            // Arrange
            var sut = new SmsController(new SmsServiceMock());
            var sendSms = new SendSms
            {
                MobileNumber = invalidMobileNumber,
                Message = "안녕하세요"
            };

            // Act
            ActionResult actual = sut.Post(sendSms);

            // Assert
            Assert.IsType<BadRequestResult>(actual);
        }

        [Fact]
        public void PostWithValidDataCorrectlySendsMessage()
        {
            // Arrange
            var smsServiceMock = new SmsServiceMock();
            var sut = new SmsController(smsServiceMock);
            var sendSms = new SendSms
            {
                MobileNumber = "010-1234-1234",
                Message = "안녕하세요"
            };

            // Act
            sut.Post(sendSms);

            // Assert
            Assert.Equal(sendSms.Message, smsServiceMock.Message);
            Assert.Equal(sendSms.MobileNumber, smsServiceMock.Receiver.Value);

        }
    }

    internal class SmsServiceMock : ISmsService
    {
        public MobileNumber Receiver { get; set; }

        public string Message { get; set; }

        public void Send(MobileNumber receiver, string message)
        {
            this.Receiver = receiver;
            this.Message = message;
        }
    }
}
