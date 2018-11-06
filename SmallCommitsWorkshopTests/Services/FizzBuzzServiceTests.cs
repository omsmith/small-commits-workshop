using System;
using NUnit.Framework;
using SmallCommitsWorkshop.Services;

namespace SmallCommitsWorkshopTests.Services {
	[TestFixture]
	internal sealed class FizzBuzzServiceTests {
		private readonly IFizzBuzzService m_fizzBuzzService = new FizzBuzzService();

		[Test]
		public void Calculate_IsNotDivisibleBy3Or5(
			[Values( 1, 2, 13, 9998 )] int number,
			[Values( null, "cats" )] string fizz,
			[Values( null, "dogs" )] string buzz
		) {
			Assert.AreEqual(
				expected: number.ToString(),
				actual: RunCalculate( number: number, fizz: fizz, buzz: buzz )
			);
		}

		[Test]
		public void Calculate_IsOnlyDivisibleBy3_ReturnsFizz(
			[Values( 3, 123, 999999 )] int number,
			[Values( null, "cats" )] string fizz,
			[Values( null, "dogs" )] string buzz
		) {
			Assert.AreEqual(
				expected: fizz ?? "Fizz",
				actual: RunCalculate( number: number, fizz: fizz, buzz: buzz )
			);
		}

		[Test]
		public void Calculate_IsOnlyDivisibleBy5_ReturnsBuzz(
			[Values( 5, 125, 55555 )] int number,
			[Values( null, "cats" )] string fizz,
			[Values( null, "dogs" )] string buzz
		) {
			Assert.AreEqual(
				expected: buzz ?? "Buzz",
				actual: RunCalculate( number: number, fizz: fizz, buzz: buzz )
			);
		}

		[Test]
		public void Calculate_IsDivisibleBy3And5_ReturnsFizzBuzz(
			[Values( 0, 15, 135 ) ] int number,
			[Values( null, "cats" )] string fizz,
			[Values( null, "dogs" )] string buzz
		) {
			string expected = "";
			expected += fizz ?? "Fizz";
			expected += buzz ?? "Buzz";

			Assert.AreEqual(
				expected: expected,
				actual: RunCalculate( number: number, fizz: fizz, buzz: buzz )
			);
		}

		[Test]
		[TestCase( null )]
		[TestCase( "" )]
		[TestCase( " " )]
		public void BadFizzThrows(
			string fizz
		) {
			var e = Assert.Throws<ArgumentNullException>(
				() => m_fizzBuzzService.Calculate( number: 1, fizz: fizz )
			);
			Assert.AreEqual( "fizz", e.ParamName );
		}

		[Test]
		[TestCase( null )]
		[TestCase( "" )]
		[TestCase( " " )]
		public void BadBuzzThrows(
			string buzz
		) {
			var e = Assert.Throws<ArgumentNullException>(
				() => m_fizzBuzzService.Calculate( number: 1, buzz: buzz )
			);
			Assert.AreEqual( "buzz", e.ParamName );
		}

		private string RunCalculate(
			int number,
			string fizz,
			string buzz
		) {
			if( fizz == null && buzz == null ) {
				return m_fizzBuzzService.Calculate( number: number );
			}

			if( fizz == null ) {
				return m_fizzBuzzService.Calculate( number: number, buzz: buzz );
			}

			if( buzz == null ) {
				return m_fizzBuzzService.Calculate( number: number, fizz: fizz );
			}

			return m_fizzBuzzService.Calculate( number: number, fizz: fizz, buzz: buzz );
		}
	}
}
