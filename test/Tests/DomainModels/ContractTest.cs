using System;
using Xunit;

namespace Tripstore.DomainModels
{
    public class ContractTest
    {
        [Fact]
        public void ConstructorWithInvalidNameThrows()
        {
            // Arrange
            var invalidName = "";
            var dummyMobileNumber = "010-1234-1234";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Contract(invalidName, dummyMobileNumber));
        }

        //[Fact]
        //public void ConstructorWithValidNameCorrectlyInitializesName()
        //{
        //    // Arrange
        //    var validName = "정진욱";
        //    var dummyMobileNumber = "010-1234-1234";

        //    // Act
        //    var sut = new Contract(validName, dummyMobileNumber);

        //    // Assert
        //    string actual = sut.Name;
        //    Assert.Equal(validName, actual);
        //}

        [Theory]
        [InlineData("정진욱")]
        [InlineData("홍길동")]
        public void ConstructorWithValidNameCorrectlyInitializesName(string validName)
        {
            // Arrange
            var dummyMobileNumber = "010-1234-1234";

            // Act
            var sut = new Contract(validName, dummyMobileNumber);

            // Assert
            string actual = sut.Name;
            Assert.Equal(validName, actual);
        }

        [Theory]
        [InlineData("000-1234-1234")] // 010 아닌 시작번호
        [InlineData("010-1234")]
        [InlineData("011-1234-1234")]
        [InlineData("010-12345-1234")]
        [InlineData("010-1234-12346")]
        public void ConstructorWithInvalidMobileNumberThrows(string invalidMobileNumber)
        {
            // Arrange
            var dummyName = "정진욱";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Contract(dummyName, invalidMobileNumber));
        }

        [Theory]
        [InlineData("010-1234-1234")]
        [InlineData("010-1235-1111")]
        public void ConstructorWithValidMobileNumberCorrectlyInitializesMobileNumber(string validMobileNumber)
        {
            // Arrange
            var dummyName = "정진욱";

            // Act
            var sut = new Contract(dummyName, validMobileNumber);

            // Assert
            string actual = sut.MobileNumber;
            Assert.Equal(validMobileNumber, actual);
        }
    }
}
