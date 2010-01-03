namespace Wawel.DomainModel
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

	using Castle.ActiveRecord;
	using Castle.Components.Validator;

	[ActiveRecord]
	public class User
	{
		private Guid id;
		private string password;

		[PrimaryKey(Access = PropertyAccess.NosetterCamelcase, Generator = PrimaryKeyType.GuidComb)]
		public Guid Id
		{
			get { return id; }
		}

		[ValidateNonEmpty]
		[Property(Unique = true)]
		public string Name { get; set; }

		[ValidateNonEmpty]
		[Property(Access = PropertyAccess.FieldCamelcase)]
		public string Password { set { password = Hash(value); } }

		[ValidateEmail]
		[Property]
		public string Email { get; set; }

		private string Hash(string value)
		{
			var sha1 = SHA1.Create();
			var bytes = Encoding.Unicode.GetBytes(value);
			var hash = sha1.ComputeHash(bytes);
			return Encoding.Unicode.GetString(hash);
		}
	}
}