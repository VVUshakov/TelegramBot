using System.ComponentModel;

namespace ConsoleApp.Models.Enums
{
	/// <summary>
	/// Типы сообщений
	/// </summary>
	public enum MessageType
	{
		[Description("Текст")]
		Text = 0,

		[Description("Фото")]
		Photo,

		[Description("Видео")]
		Video,

		[Description("Документ")]
		Document,
	}
}
