namespace Wawel.DomainModel
{
	using System;

	using Castle.ActiveRecord;
	using Castle.ActiveRecord.Linq;

	//[ActiveRecord]
	//public class Benchmark : ActiveRecordLinqBase<Benchmark>
	//{
	//    protected Benchmark()
	//    {
	//    }

	//    public Benchmark(string name, string version, string webpage)
	//    {
	//        Name = name;
	//        Version = version;
	//        Webpage = webpage;
	//    }

	//    public Benchmark(string version, string webpage, string description, string name)
	//    {
	//        Version = version;
	//        Webpage = webpage;
	//        Description = description;
	//        Name = name;
	//    }

	//    [PrimaryKey]
	//    public Guid Id { get; private set; }

	//    [Property(NotNull = true, Access = PropertyAccess.AutomaticProperty)]
	//    public string Name { get; private set; }

	//    [Property(Length = 10000)]
	//    public string Description { get; set; }

	//    [Property(Access = PropertyAccess.AutomaticProperty)]
	//    public string Webpage { get; private set; }

	//    [Property(Access = PropertyAccess.AutomaticProperty)]
	//    public string Version { get; private set; }
	//}
}