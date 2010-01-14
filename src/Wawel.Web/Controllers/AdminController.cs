namespace Wawel.Web.Controllers
{
	using System;
	using System.Collections.Generic;

	using Castle.ActiveRecord;
	using Castle.MonoRail.Framework;
	using Castle.MonoRail.Framework.Helpers;

	using Wawel.DomainModel;

	[Layout("Admin")]
	public class AdminController:SmartDispatcherController
	{
		public void Users()
		{
			ICollection<User> users = ActiveRecordMediator<User>.FindAll();
			PropertyBag["users"] = PaginationHelper.CreatePagination(users, 25, 0);
		}

		public void EditUser(Guid id)
		{
			var user = ActiveRecordMediator<User>.FindByPrimaryKey(id, false);
			if (user == null)
			{
				RenderView("NoSuchUser");
				return;
			}
			PropertyBag["user"] = user;

		}

		public void Benchmarks()
		{
			ICollection<Benchmark> benchmarks = ActiveRecordMediator<Benchmark>.FindAll();
			PropertyBag["benchmarks"] = PaginationHelper.CreatePagination(benchmarks, 25, 0);
		}
	}
}