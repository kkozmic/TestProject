namespace Wawel.DomainModel.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using HibernatingRhinos.Profiler.Appender.NHibernate;

	using NHibernate;
	using NHibernate.ByteCode.Castle;
	using NHibernate.Cfg;
	using NHibernate.Connection;
	using NHibernate.Dialect;
	using NHibernate.Driver;
	using NHibernate.Tool.hbm2ddl;

	using Xunit;

	using Environment = NHibernate.Cfg.Environment;

	public class UserTests : IDisposable
	{
		private ISessionFactory factory;
		private SchemaExport schemaExport;

		public UserTests()
		{
			NHibernateProfiler.Initialize();
			InitNH();
		}

		public void Dispose()
		{
			NHibernateProfiler.Stop();
			schemaExport.Drop(true, true);
			// this is only for tests
		}

		[Fact]
		public void Can_perform_benchmark_runs_NH()
		{
			var stefan = new User();
			stefan.AddResult();
			User user;
			BenchmarkResult result;
			using (var session = factory.OpenSession())
			using (var tx = session.BeginTransaction())
			{
				session.Save(stefan);
				tx.Commit();
			}
			using(var session = factory.OpenSession())
			using (var tx = session.BeginTransaction())
			{
				user = session.CreateCriteria<User>().List<User>().Single();
				tx.Commit();
			}
			using(var session = factory.OpenSession())
			using (var tx = session.BeginTransaction())
			{
				result = session.CreateCriteria<BenchmarkResult>().List<BenchmarkResult>().Single();
				tx.Commit();
			}


			Assert.NotEmpty(user.BenchmarkResults);
			Assert.Equal(1, user.BenchmarkResults.Count());

			Assert.NotNull(result);
		}

		private void InitNH()
		{
			var configuration = new Configuration()
				.AddProperties(new Dictionary<string, string>
				{
					{ Environment.ConnectionDriver, typeof(SqlClientDriver).FullName },
					{ Environment.Dialect, typeof(MsSql2005Dialect).FullName },
					{ Environment.ConnectionProvider, typeof(DriverConnectionProvider).FullName },
					{ Environment.ConnectionStringName, "Wawel" },
					{ Environment.ProxyFactoryFactoryClass, typeof(ProxyFactoryFactory).AssemblyQualifiedName },
					{ "hbm2ddl.keywords", "auto-quote" },
				});

			configuration.AddAssembly(typeof(User).Assembly);
			SchemaMetadataUpdater.QuoteTableAndColumns(configuration);
			new SchemaUpdate(configuration).Execute(true, true);
			schemaExport = new SchemaExport(configuration);
			factory = configuration.BuildSessionFactory();
		}
	}
}