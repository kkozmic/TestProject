namespace Wawel.DomainModel
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

	using Castle.ActiveRecord;
	using Castle.ActiveRecord.Linq;

	using NHibernate.Criterion;

[ActiveRecord]
public class User : ActiveRecordLinqBase<User>
{
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

[Property(Access = PropertyAccess.FieldCamelcase)]
public string Password { set { password = Hash(value); } }

[Property(Length = 10000)]
public string About { get; set; }

	private string Hash(string value)
	{
		var sha1 = SHA1.Create();
		var bytes = Encoding.Unicode.GetBytes(value);
		var hash = sha1.ComputeHash(bytes);
		return Encoding.Unicode.GetString(hash);
	}

	public static User Login(string username, string password)
	{
		var user = new User { Name = username, Password = password };
		return User.FindOne(Example.Create(user).ExcludeNulls());
	}
}
}