using System.ComponentModel;

namespace ConsoleApp.Models.Enums
{
	public enum TelegramEvents
	{
		[Description(nameof(Initialization))]
		Initialization,

		[Description(nameof(Register))]
		Register,

		[Description(nameof(Message))]
		Message,

		[Description(nameof(Server))]
		Server,

		[Description(nameof(BlockedBot))]
		BlockedBot,

		[Description(nameof(CommandExecute))]
		CommandExecute,

		[Description(nameof(GroupAction))]
		GroupAction,
	}
}
