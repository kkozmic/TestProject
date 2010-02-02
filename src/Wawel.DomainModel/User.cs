namespace Wawel.DomainModel
{
	using System;
	using System.Collections.Generic;
	using System.Security.Cryptography;
	using System.Text;

	using NHibernate.Criterion;

	public class User
	{
		private readonly ICollection<BenchmarkResult> benchmarkResults = new HashSet<BenchmarkResult>();

		private string password;

		private Guid id;

		public Guid Id
		{
			get { return id; }
		}


	public IEnumerable<BenchmarkResult> BenchmarkResults
	{
		get
		{
			foreach (var result in benchmarkResults)
			{
				yield return result;
			}
		}
	}

		public BenchmarkResult AddResult()
		{
			var result = new BenchmarkResult(this);
			benchmarkResults.Add(result);
			return result;
		}
	}
}