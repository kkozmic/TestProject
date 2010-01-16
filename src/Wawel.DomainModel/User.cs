namespace Wawel.DomainModel
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;

	using Castle.ActiveRecord;
	using Castle.ActiveRecord.Linq;

	using NHibernate.Criterion;

	[ActiveRecord]
	public class User : ActiveRecordLinqBase<User>
	{
		private readonly ICollection<BenchmarkResult> benchmarkResults = new List<BenchmarkResult>();

		private string password;

		private Guid id;

		[PrimaryKey(Access = PropertyAccess.NosetterCamelcase)]
		public Guid Id
		{
			get { return id; }
		}

		[Property(Unique = true, NotNull = true)]
		public string Name { get; set; }

		[Property]
		public string Email { get; set; }

		[Property(Access = PropertyAccess.FieldCamelcase, NotNull = true)]
		public string Password { set { password = Hash(value); } }

		[Property(Length = 10000)]
		public string About { get; set; }


		[HasMany(Access = PropertyAccess.FieldCamelcase, 
			Cascade = ManyRelationCascadeEnum.SaveUpdate, 
			RelationType = RelationType.Set,
			Inverse = true)]
		public IEnumerable<BenchmarkResult> BenchmarkResults
		{
			get { return benchmarkResults.Select(r => r); }
		}

		private string Hash(string value)
		{
			var sha1 = SHA1.Create();
			var bytes = Encoding.Unicode.GetBytes(value);
			var hash = sha1.ComputeHash(bytes);
			return Encoding.Unicode.GetString(hash);
		}

		public BenchmarkResult RunBenchmark(string benchmarkName, string computerModel, double score)
		{
			var result = new BenchmarkResult(this, benchmarkName, computerModel, score);
			benchmarkResults.Add(result);
			return result;
		}

		public static User Login(string username, string password)
		{
			var user = new User { Name = username, Password = password };
			return User.FindOne(Example.Create(user).ExcludeNulls());
		}
	}
}