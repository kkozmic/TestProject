namespace Wawel.DomainModel.Tests
{
	using System;
	using System.Linq;

	using Castle.ActiveRecord;
	using Castle.ActiveRecord.Framework;

	using HibernatingRhinos.Profiler.Appender.NHibernate;

	using NHibernate.Tool.hbm2ddl;

	using Xunit;

	public class UserTests:IDisposable
	{
		private ISessionFactoryHolder sessionFactoryHolder;

		public UserTests()
		{

			NHibernateProfiler.Initialize();
			ActiveRecordStarter.SessionFactoryHolderCreated += SaveHolder;
			ActiveRecordStarter.Initialize();

			// registers all active record types from the assembly
			ActiveRecordStarter.RegisterAssemblies(typeof(User).Assembly);

			// performs initialization using information from appdomain config file
			// generates database schema
			SetAutoQuoteIdentifiers();
			ActiveRecordStarter.UpdateSchema();
		}

		private void SaveHolder(ISessionFactoryHolder holder)
		{
			sessionFactoryHolder = holder;
		}

		private void SetAutoQuoteIdentifiers()
		{
			var configurations = sessionFactoryHolder.GetAllConfigurations();
			foreach (var configuration in configurations)
			{
				SchemaMetadataUpdater.QuoteTableAndColumns(configuration);
			}
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
			stefan.RunBenchmark("Foo bar!", "AyeMack Pro", 3.2);
			stefan.Save();

			var user = User.FindAll().Single();

			Assert.NotEmpty(user.BenchmarkResults);
			Assert.Equal(1, user.BenchmarkResults.Count());

			var result = user.BenchmarkResults.Single();

			Assert.NotNull(result);
			Assert.Equal("Foo bar!", result.BenmchmarkName);
			Assert.Equal("AyeMack Pro", result.ComputerModel);
			Assert.Equal(3.2, result.Score);
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
