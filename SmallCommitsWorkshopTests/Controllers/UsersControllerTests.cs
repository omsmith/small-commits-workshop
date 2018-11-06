using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SmallCommitsWorkshop;
using SmallCommitsWorkshop.Models;
using SmallCommitsWorkshopTests.Extensions;

namespace SmallCommitsWorkshopTests.Controllers {
	[TestFixture]
	internal sealed class UsersControllerTests {

		private static User USER_A = new User() { Id = 42, UserName = "JaneSmith" };
		private static User USER_B = new User() { Id = 156, UserName = "JoeSmith" };

		private WebApplicationFactory<Startup> m_factory;
		private HttpClient m_client;
		private IServiceScope m_scope;
		private UsersContext m_usersContext;

		[SetUp]
		public void SetUp() {
			m_factory = new WebApplicationFactory<Startup>();
			m_client = m_factory.CreateClient();
			m_scope = m_factory.Server?.Host.Services.CreateScope();
			m_usersContext = m_scope.ServiceProvider.GetService<UsersContext>();
		}

		[TearDown]
		public void TearDown() {
			m_scope.Dispose();
			m_client.Dispose();
			m_factory.Dispose();
			m_usersContext.Dispose();
		}

		private async Task<IEnumerable<User>> SetupUsers( User[] users = null ) {
			users = users ?? new User[] { USER_A, USER_B };

			m_usersContext.Users.AddRange( users );

			await m_usersContext.SaveChangesAsync().ConfigureAwait( false );

			return users;
		}

		[Test]
		public async Task GetAll_ReturnsUsers() {
			IEnumerable<User> users = await SetupUsers().ConfigureAwait( false );

			using( HttpResponseMessage response = await m_client.GetAsync( "/api/users" ).ConfigureAwait( false ) ) {
				CollectionAssert.AreEquivalent(
					users.ToDictionary(
						user => user.Id,
						user => new Dictionary<string, object>( 2 ) {
							{ "id", user.Id },
							{ "userName", user.UserName },
						}
					),
					await response.Content.ReadAsJsonAsync<IDictionary<long, IDictionary<string, object>>>().ConfigureAwait( false )
				);
			}
		}
	}
}
