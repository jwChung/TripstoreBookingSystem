using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Tripstore.Presentations
{
    public class ReservationsControllerTest
    {
        private readonly MakeReservation makeReservation;

        public ReservationsControllerTest()
        {
            makeReservation = new MakeReservation
            {
                Name = "정진욱",
                NumberOfPeople = 2,
                Destination = "하와이",
                MobileNumber = "010-1234-1234",
                StartDate = DateTimeOffset.Now + TimeSpan.FromDays(2),
                EndDate = DateTimeOffset.Now + TimeSpan.FromDays(4)
            };
        }

        private string JsonFilePath =>
            $"../../../../../{makeReservation.MobileNumber}+{makeReservation.Destination}.json";

        [Fact]
        public void PostWithValidDataReturnsOkResult()
        {   
            try
            {
                // Arrange
                var sut = new ReservationsController();

                // Act
                ActionResult actual = sut.Post(this.makeReservation);

                // Assert
                Assert.IsType<OkResult>(actual);
            }
            finally
            {
                // Teardown
                File.Delete(this.JsonFilePath);
            }
        }

        [Theory]
        [InlineData("010-1234-1234", "하와이")]
        [InlineData("010-1234-2342", "하와이")]
        public void PostWithValidDataSavesReservation(string mobileNumber, string destination)
        {
            try
            {
                // Arrange
                var sut = new ReservationsController();
                this.makeReservation.MobileNumber = mobileNumber;
                this.makeReservation.Destination = destination;

                // Act
                sut.Post(this.makeReservation);

                // Assert
                var json = File.ReadAllText(this.JsonFilePath);
                var actual = JsonConvert.DeserializeObject<MakeReservation>(json);
                Assert.Equal(this.makeReservation.Name, actual.Name);
                Assert.Equal(this.makeReservation.NumberOfPeople, actual.NumberOfPeople);
                Assert.Equal(this.makeReservation.Destination, actual.Destination);
                Assert.Equal(this.makeReservation.MobileNumber, actual.MobileNumber);
                Assert.Equal(this.makeReservation.StartDate, actual.StartDate);
                Assert.Equal(this.makeReservation.EndDate, actual.EndDate);
            }
            finally
            {
                // Teardown
                File.Delete(this.JsonFilePath);
            }
        }

        [Fact]
        public void PostReturnsConflictResultWhenDuplicateReservationIsSaved()
        {
            try
            {
                // Arrange
                var sut = new ReservationsController();
                sut.Post(this.makeReservation);

                // Act
                var actual = sut.Post(this.makeReservation);

                // Assert
                Assert.IsType<ConflictResult>(actual);
            }
            finally
            {
                // Teardown
                File.Delete(this.JsonFilePath);
            }
        }

        [Theory]
        [InlineData("000-1234-1234")] // 010 아닌 시작번호
        [InlineData("010-1234")]
        [InlineData("011-1234-1234")]
        [InlineData("010-12345-1234")]
        [InlineData("010-1234-12346")]
        public void PostWithInvalidMobileNumberReturnsBadRquest(string invalidMobilNumber)
        {
            try
            {
                // Arrange
                var sut = new ReservationsController();
                this.makeReservation.MobileNumber = invalidMobilNumber;

                // Act
                var actual = sut.Post(this.makeReservation);

                // Assert
                Assert.IsType<BadRequestResult>(actual);
            }
            finally
            {
                // Teardown
                File.Delete(this.JsonFilePath);
            }
        }
    }
}
