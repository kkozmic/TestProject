namespace Wawel.DomainModel.Tests
{
	using System;
	using System.Linq;

	using Castle.ActiveRecord;

	using HibernatingRhinos.Profiler.Appender.NHibernate;

	using Xunit;

	public class UserTests:IDisposable
	{
		public UserTests()
		{

			NHibernateProfiler.Initialize();
			// performs initialization using information from appdomain config file
			ActiveRecordStarter.Initialize();
			// registers all active record types from the assembly
			ActiveRecordStarter.RegisterAssemblies(typeof(User).Assembly);
			// generates database schema
			ActiveRecordStarter.UpdateSchema();
		}

		[Fact]
		public void Can_save_and_read_User()
		{
			var stefan = new User
			{
				Email = "stefan@gmail.com",
				Name = "Stefan",
				Password = "Super compilcated password!",
				About = "Stefan is a very cool."
			};

			stefan.Save();
			var users = User.Queryable
				.Where(u => u.Name.StartsWith("S"))
				.ToList();
			Assert.NotEmpty(users);
			Assert.Equal("Stefan", users.Single().Name);
		}

		[Fact]
		public void Valid_user_can_login()
		{
			var stefan = new User
			{
				Email = "stefan@gmail.com",
				Name = "Stefan",
				Password = "Super compilcated password!",
				About = "Stefan is a very cool."
			};

			stefan.Save();

			var user = User.Login("Stefan","Super compilcated password!");
			Assert.NotNull(user);
		}

		[Fact]
		public void Invalid_user_can_not_login()
		{
			var stefan = new User
			{
				Email = "stefan@gmail.com",
				Name = "Stefan",
				Password = "Super compilcated password!",
				About = "Stefan is a very cool."
			};

			stefan.Save();

			var user = User.Login("Stefan", "oups, that's not it...");
			Assert.Null(user);
		}

		public void Dispose()
		{
			NHibernateProfiler.Stop();
			ActiveRecordStarter.DropSchema();

			// this is only for tests
			ActiveRecordStarter.ResetInitializationFlag();
		}
	}
}
