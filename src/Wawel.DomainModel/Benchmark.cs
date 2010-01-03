namespace Wawel.DomainModel
{
	using System;
	using System.Collections.Generic;

	using Castle.ActiveRecord;

	[ActiveRecord]
	public class Benchmark
	{
		private Guid id;

		[PrimaryKey(Access = PropertyAccess.NosetterCamelcase, Generator = PrimaryKeyType.GuidComb)]
		public Guid Id
		{
			get { return id; }
		}

		[Property]
		public string ApplicationName { get; set; }

		[Property]
		public string ApplicationVersion { get; set; }

		[Property]
		public string Website { get; set; }

		[Property(Length = 5000)]
		public string Description { get; set; }

		[HasMany(Inverse = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan)]
		public IList<BenchmarkResults> Results { get; set; }
	}
}