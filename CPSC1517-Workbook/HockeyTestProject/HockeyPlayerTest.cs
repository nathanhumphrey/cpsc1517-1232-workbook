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

		// Failing HockeyPlayer constructor - TODO: create InlineData lines for remaining properties
		[Theory]
		[InlineData("", LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
		[InlineData(" ", LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
		[InlineData(null, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
		public void HockeyPlayer_GreedyConstructor_ThrowsException(string firstName, string lastName, string birthPlace,
			int birthYear, int birthMonth, int birthDay, int weightInPounds, int heightInInches, int jerseyNumber, Position position, Shot shot, string errMsg)
		{

			// Arrange
			Action act = () => new HockeyPlayer(firstName, lastName, birthPlace, new DateOnly(birthYear, birthMonth, birthDay), weightInPounds, heightInInches, jerseyNumber, position, shot);

			// Act/Assert
			act.Should().Throw<ArgumentException>().WithMessage(errMsg);
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
	}
}