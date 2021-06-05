using System;
using Xunit;


namespace Monitor.Tests
{
    public class MonitorTest
    {
        // Magic arguments
        const string PROCESS_NAME = "chrome";
        const int LIFE_TIME = 1;
        const int CHECK_TIME = 1;

        // Casses for GetMlSeconds()
        const int TWO_MINUTES = 2;
        const int TWO_MINUTES_WAITING_RESULT = 120000;
        const int MINUS_TWO_MINUTES = -2;
        const int MINUS_TWO_MINUTES_WAITING_RESULT = -120000;
        const int TWO_MINUTES_NOT_WAITING_RESULT = 60000;
        const int ZERO_MINUTES = 0;
        const int ZERO_MINUTES_WAITING_RESULT = 0;

        // Cases for IsEmpty()
        string[] EMPTY_ARRAY = new string[] { };
        string[] NOT_EMPTY_ARRAY = new string[1] {"chrome"};

        [Fact]
        public void GetMlSeconds_WithPositiveMinutes_ReturnMlSeconds()
        {
            //Arrange
            Monitor monitor = new(PROCESS_NAME, LIFE_TIME, CHECK_TIME);
            //Act
            int result = monitor.GetMlSeconds(TWO_MINUTES);
            //Assert
            Assert.Equal(TWO_MINUTES_WAITING_RESULT, result);
        }

        [Fact]
        public void GetMlSeconds_WithNegativeMinutes_ReturnNegativMlSeconds()
        {
            //Arrange
            Monitor monitor = new(PROCESS_NAME, LIFE_TIME, CHECK_TIME);
            //Act
            int result = monitor.GetMlSeconds(MINUS_TWO_MINUTES);
            //Assert
            Assert.Equal(MINUS_TWO_MINUTES_WAITING_RESULT, result);
        }

        [Fact]
        public void GetMlSeconds_WithMinutes_NotReturnRightMlSeconds()
        {
            //Arrange
            Monitor monitor = new(PROCESS_NAME, LIFE_TIME, CHECK_TIME);
            //Act
            int result = monitor.GetMlSeconds(TWO_MINUTES);
            //Assert
            Assert.NotEqual(TWO_MINUTES_NOT_WAITING_RESULT, result);
        }

        [Fact]
        public void GetMlSeconds_WithZeroMinutes_ReturnZeroMlSeconds()
        {
            //Arrange
            Monitor monitor = new(PROCESS_NAME, LIFE_TIME, CHECK_TIME);
            //Act
            int result = monitor.GetMlSeconds(ZERO_MINUTES);
            //Assert
            Assert.Equal(ZERO_MINUTES_WAITING_RESULT, result);
        }

        [Fact]
        public void IsEmpty_WithEmpty_ReturnTrue()
        {
            //Arrange
            Monitor monitor = new(PROCESS_NAME, LIFE_TIME, CHECK_TIME);
            //Act
            bool result = monitor.IsEmpty(EMPTY_ARRAY);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmpty_WithNotEmpty_ReturnFalse()
        {
            //Arrange
            Monitor monitor = new(PROCESS_NAME, LIFE_TIME, CHECK_TIME);
            //Act
            bool result = monitor.IsEmpty(NOT_EMPTY_ARRAY);
            //Assert
            Assert.False(result);
        }
    }
}
