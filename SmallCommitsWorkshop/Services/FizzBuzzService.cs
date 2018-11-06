using System;
using System.Text;

namespace SmallCommitsWorkshop.Services {
	public class FizzBuzzService : IFizzBuzzService {
		string IFizzBuzzService.Calculate(
			int number,
			string fizz,
			string buzz
		) {
			if( string.IsNullOrWhiteSpace( fizz ) ) {
				throw new ArgumentNullException( nameof( fizz ) );
			}

			if( string.IsNullOrWhiteSpace( buzz ) ) {
				throw new ArgumentNullException( nameof( buzz ) );
			}

			StringBuilder response = new StringBuilder();

			if( number % 3 == 0 ) {
				response.Append( fizz );
			}

			if( number % 5 == 0 ) {
				response.Append( buzz );
			}

			return response.Length == 0
				? number.ToString()
				: response.ToString();
		}
	}
}
