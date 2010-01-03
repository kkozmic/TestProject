using System;

namespace Wawel.DomainModel
{
	using Castle.ActiveRecord;

	[ActiveRecord]
	public class BenchmarkResults
	{
		private Guid id;

		[PrimaryKey(Access = PropertyAccess.NosetterCamelcase)]
		public Guid Id
		{
			get { return id; }
		}

		[BelongsTo("BenchmarkId")]
		public Benchmark Benchmark { get; set; }

	}
}
