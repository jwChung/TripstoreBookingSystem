using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tripstore.DomainModels
{
    public class MobileNumberTest
    {
        [Theory]
        [InlineData("000-1234-1234")] // 010 아닌 시작번호
        [InlineData("010-1234")]
        [InlineData("011-1234-1234")]
        [InlineData("010-12345-1234")]
        [InlineData("010-1234-12346")]
        public void ParseWithInvalidMobileNumberThrows(string invalidMobileNumber)
        {
            // Arrange

            // Act & Assert
            Assert.Throws<ArgumentException>(() => MobileNumber.Parse(invalidMobileNumber));
        }

        [Theory]
        [InlineData("010-1234-1234")]
        [InlineData("010-1235-1111")]
        public void ParseWithValidMobileNumberReturnsCorrectResult(string validMobileNumber)
        {
            // Arrange

            // Act
            var actual = MobileNumber.Parse(validMobileNumber);

            // Assert
            Assert.Equal(validMobileNumber, actual.Value);
        }
    }
}
