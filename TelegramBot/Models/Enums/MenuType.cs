using System.ComponentModel;

namespace ConsoleApp.Models.Enums
{
	/// <summary>
	/// Типы меню
	/// </summary>
	public enum MenuType
	{
		[Description("Без меню")]
		None = 0,

		[Description("Обычное кнопочное меню")]
		Reply,

		[Description("Inline меню")]
		Inline
	}
}
