namespace Wawel.DomainModel
{
	using System;

public class BenchmarkResult
{
	protected BenchmarkResult()
	{
	}

	public BenchmarkResult(User user)
	{
		if (user == null)
		{
			throw new ArgumentNullException("user");
		}

		User = user;
	}


		public Guid Id { get; private set; }

		public User User { get; private set; }


	}
}