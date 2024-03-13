using System.ComponentModel;

namespace ConsoleApp.Models.Enums
{
	/// <summary>
	/// Статус/Привилегии пользователей
	/// </summary>
	[Flags]
	public enum UserPrivilege
	{
		[Description("Гость")]
		Guest = 1,

		[Description("Зарегистрированный")]
		Registered = 2,

		[Description("Администратор")]
		Admin = 4,

		[Description("VIP")]
		VIP = 8,

		[Description("Модератор")]
		Moderator = 16,
	}
}
