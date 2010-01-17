namespace Wawel.DomainModel
{
	using System;

	using Castle.ActiveRecord;
	using Castle.ActiveRecord.Linq;

[ActiveRecord]
public class BenchmarkResult : ActiveRecordLinqBase<BenchmarkResult>
{
	protected BenchmarkResult()
	{
	}

	public BenchmarkResult(User user, string benmchmarkName, string computerModel, double score)
	{
		if (user == null)
		{
			throw new ArgumentNullException("user");
		}
		if (benmchmarkName == null)
		{
			throw new ArgumentNullException("benmchmarkName");
		}
		if (computerModel == null)
		{
			throw new ArgumentNullException("computerModel");
		}

		User = user;
		BenmchmarkName = benmchmarkName;
		ComputerModel = computerModel;
		Score = score;
	}


		[PrimaryKey]
		public Guid Id { get; private set; }

		[BelongsTo]
		public User User { get; private set; }

		[Property]
		public string BenmchmarkName { get; private set; }

		[Property]
		public string ComputerModel { get; private set; }

		[Property]
		public double Score { get; private set; }
	}
}