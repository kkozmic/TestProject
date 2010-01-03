namespace Wawel.Web.Controllers
{
	using System.Collections.Generic;

	using Castle.ActiveRecord;
	using Castle.Components.Pagination;
	using Castle.MonoRail.Framework;
	using Castle.MonoRail.Framework.Helpers;

	using Wawel.DomainModel;

	[Layout("Admin")]
	public class AdminController:SmartDispatcherController
	{
		public void Users()
		{
			ICollection<User> users = ActiveRecordMediator<User>.FindAll();
			IPaginatedPage page = PaginationHelper.CreatePagination(users, 25, 0);
			PropertyBag["users"] = page;
		}

		public void Benchmarks()
		{
			ICollection<Benchmark> benchmarks = ActiveRecordMediator<Benchmark>.FindAll();
			PropertyBag["benchmarks"] = PaginationHelper.CreatePagination(benchmarks, 25, 0);
		}
	}
}