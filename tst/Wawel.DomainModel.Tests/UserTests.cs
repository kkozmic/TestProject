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
		public void Can_perform_benchmark_runs()
		{
			var stefan = new User
			{
				Email = "stefan@gmail.com",
				Name = "Stefan",
				Password = "Super compilcated password!",
				About = "Stefan is a very cool."
			};

			var result = stefan.RunBenchmark("Foo bar!", "AyeMack Pro", 3.2);
			Assert.NotNull(result);
			Assert.Equal("Foo bar!", result.BenmchmarkName);
			Assert.Equal("AyeMack Pro", result.ComputerModel);
			Assert.Equal(3.2, result.Score);
			Assert.Equal(stefan, result.User);

			stefan.Save();

			var user = User.FindAll().Single();
			Assert.NotEmpty(user.BenchmarkResults);
			Assert.Equal(1, user.BenchmarkResults.Count());
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
