namespace Wawel.Web.Controllers
{
	using Castle.ActiveRecord;
	using Castle.MonoRail.Framework;

	using Wawel.DomainModel;

	public class HomeController:Controller
	{
		public void Default()
		{
			var benchmarks = ActiveRecordMediator<Benchmark>.FindAll();
			PropertyBag["benchmarks"] = benchmarks;
		}
	}
}