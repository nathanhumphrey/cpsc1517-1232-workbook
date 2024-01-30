using FluentAssertions;
using Hockey.Data;

namespace HockeyTestProject
{
	public class HockeyPlayerTest
	{
		//[Fact]
		//public void HockeyPlayer_DefaultConstructor_CreatesGoodPlayer()
		//{
		//	// arrange
		//	HockeyPlayer player;

		//	// act
		//	player = new HockeyPlayer();

		//	//assert
		//	player.Should().BeOfType<HockeyPlayer>();
		//}

		// Constants for test HockeyPlayer
		const string FirstName = "Connor";
		const string LastName = "Brown";
		const string BirthPlace = "Toronto, ON, CAN";
		const int BirthYear = 1994;
		const int BirthMonth = 01;
		const int BirthDay = 14;
		const int HeightInInches = 72;
		const int WeightInPounds = 188;
		const int JerseyNumber = 28;
		const Position PlayerPosition = Position.Center;
		const Shot PlayerShot = Shot.Left;
		// The following relies on our being correct here - not writing a test for the test expected value
		readonly int Age = (DateOnly.FromDateTime(DateTime.Now).DayNumber - new DateOnly(BirthYear, BirthMonth, BirthDay).DayNumber) / 365;
		string ToStringValue = $"{FirstName},{LastName},{JerseyNumber},{PlayerPosition},{PlayerShot},{HeightInInches},{WeightInPounds},Jan-14-1994,{BirthPlace.Replace(", ", "-")}";

		// Can quickly run a test to check our method for AGE above
		//[Fact]
		//public void AGE_Is_Correct()
		//{
		//	Age.Should().Be(30);
		//}

		/// <summary>
		/// Creates a default HockeyPlayer for testing
		/// </summary>
		/// <returns>A new HockeyPlayer</returns>
		public HockeyPlayer CreateTestHockeyPlayer()
		{
			return new HockeyPlayer(FirstName, LastName, BirthPlace, new DateOnly(BirthYear, BirthMonth, BirthDay), WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot);
		}

		// Good HockeyPlayer constructor
		[Theory]
		[InlineData(FirstName, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot)]
		public void HockeyPlayer_GreedyConstructor_ReturnsHockeyPlayer(string firstName, string lastName, string birthPlace,
			int birthYear, int birthMonth, int birthDay, int weightInPounds, int heightInInches, int jerseyNumber, Position position, Shot shot)
		{
			HockeyPlayer actual;

			actual = new HockeyPlayer(firstName, lastName, birthPlace, new DateOnly(birthYear, birthMonth, birthDay), weightInPounds, heightInInches, jerseyNumber, position, shot);

			actual.Should().NotBeNull();
		}

		// Failing HockeyPlayer constructor
		[Theory]
		[InlineData("", LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
		[InlineData(" ", LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
		[InlineData(null, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
		[InlineData(FirstName, "", BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Last name cannot be null or empty.")]
		[InlineData(FirstName, " ", BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Last name cannot be null or empty.")]
		[InlineData(FirstName, null, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Last name cannot be null or empty.")]
		[InlineData(FirstName, LastName, "", BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Birth place cannot be null or empty.")]
		[InlineData(FirstName, LastName, " ", BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Birth place cannot be null or empty.")]
		[InlineData(FirstName, LastName, null, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Birth place cannot be null or empty.")]
		[InlineData(FirstName, LastName, BirthPlace, BirthYear, BirthMonth, -1, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Date of birth cannot be in the future.")]
		[InlineData(FirstName, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, -1, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Weight must be positive.")]
		[InlineData(FirstName, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, 0, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "Weight must be positive.")]
		[InlineData(FirstName, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, -1, JerseyNumber, PlayerPosition, PlayerShot, "Height must be positive.")]
		[InlineData(FirstName, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, HeightInInches, 0, JerseyNumber, PlayerPosition, PlayerShot, "Height must be positive.")]
		public void HockeyPlayer_GreedyConstructor_ThrowsException(string firstName, string lastName, string birthPlace,
			int birthYear, int birthMonth, int birthDay, int weightInPounds, int heightInInches, int jerseyNumber, Position position, Shot shot, string errMsg)
		{
			DateOnly dateOfBirth;

			if (birthDay == -1)
			{
				// Test for tomorrow
				dateOfBirth = DateOnly.FromDateTime(DateTime.Now).AddDays(1);
			}
            else
            {
                dateOfBirth = new DateOnly(birthYear, birthMonth, birthDay);
            }

            // Arrange
            Action act = () => new HockeyPlayer(firstName, lastName, birthPlace, dateOfBirth , weightInPounds, heightInInches, jerseyNumber, position, shot);

			// Act/Assert
			act.Should().Throw<ArgumentException>().WithMessage(errMsg);
		}
		[Theory]
		[InlineData(1)]
		[InlineData(98)]
		public void HockeyPlayer_JerseyNumber_GoodSetAndGet(int expected)
		{
			HockeyPlayer player = CreateTestHockeyPlayer();

			player.JerseyNumber = expected;
			int actual = player.JerseyNumber;

			actual.Should().Be(expected);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(99)]
		public void HockeyPlayer_JerseyNumber_BadSetThrows(int value)
		{
			HockeyPlayer player = CreateTestHockeyPlayer();

			Action act = () => player.JerseyNumber = value;

			act.Should().Throw<ArgumentException>();
		}


		[Fact]
		public void HockeyPlayer_Age_ReturnsCorrectAge()
		{
			// Arrange 
			HockeyPlayer player = CreateTestHockeyPlayer();

			// Act
			int actual = player.Age;

			// Assert
			actual.Should().Be(Age);
		}

		[Fact]
		public void HockeyPlayer_ToString_ReturnsCorrectValue()
		{
			// Arrange 
			HockeyPlayer player = CreateTestHockeyPlayer();
			// Act
			string actual = player.ToString();

			// Assert
			actual.Should().Be(ToStringValue);
		}

		[Fact]
		public void HockeyPlayer_Parse_ParsesCorrectly()
		{
			HockeyPlayer actual;
			string line = ToStringValue;
			actual = HockeyPlayer.Parse(line);

			actual.Should().BeOfType<HockeyPlayer>();
			// TODO: could also check each individual property for correct assignment

		}

		[Theory]
		[InlineData(null, "*Line cannot be null or empty.*")]
		[InlineData("", "*Line cannot be null or empty.*")]
		[InlineData(" ", "*Line cannot be null or empty.*")]

		public void HockeyPlayer_Parse_ThrowsForNullEmptyOrWhiteSpaceLine(string line, string errMsg)
		{
			Action act = () => HockeyPlayer.Parse(line);

			act.Should().Throw<ArgumentNullException>().WithMessage(errMsg);

		}

		[Theory]
		[InlineData("one,two,three,four,five,six,seven,eight", "Incorrect number of fieds.")]
		[InlineData("one,two,three,four,five,six,seven,eight,nine,ten", "Incorrect number of fieds.")]
		public void HockeyPlayer_Parse_ThrowsForInvalidNumberOfFields(string line, string errMsg)
		{
			Action act = () => HockeyPlayer.Parse(line);

			act.Should().Throw<InvalidDataException>().WithMessage(errMsg);

		}

		[Theory]
		[InlineData("one,two,three,four,five,six,seven,eight,nine", "Error parsing line")]
		public void HockeyPlayer_Parse_ThrowsForFormatError(string line, string errMsg)
		{
			Action act = () => HockeyPlayer.Parse(line);

			act.Should().Throw<FormatException>().WithMessage($"*{errMsg}*");

		}

		[Fact]
		public void HockeyPlayer_TryParse_ParsesTrueCorrectly()
		{
			HockeyPlayer? actual = null;
			bool result;

			result = HockeyPlayer.TryParse(ToStringValue, out actual);

			result.Should().BeTrue();
			actual.Should().NotBeNull();
		}

		[Fact]
		public void HockeyPlayer_TryParse_ParsesFalseCorrectly()
		{
			HockeyPlayer? actual = null;
			bool result;

			result = HockeyPlayer.TryParse("", out actual);

			result.Should().BeFalse();
			actual.Should().BeNull();
		}
	}
}