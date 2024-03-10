using NLog;
using PRTelegramBot.Core;
using PRTelegramBot.Extensions;


#region [запуск telegram бота]

var telegram = new PRBot(options =>
{
	// Токен telegram бота берется из BotFather
	options.Token = "6605743715:AAHjxHXOVDCA0dB0ruWWzFwzW1fBr7uUEyY";
	// Перед запуском очищает список обновлений, которые накопились когда бот не работал.
	options.ClearUpdatesOnStart = true;
	// Если есть хоть 1 идентификатор telegram пользователя, могут пользоваться только эти пользователи
	options.WhiteListUsers = new List<long>() { };
	// Идентификатор telegram пользователя
	options.Admins = new List<long>() { };
	// Уникальных идентификатор для бота, используется, чтобы в одном приложение запускать несколько ботов
	options.BotId = 0;
});

//Подписка на простые логи
telegram.OnLogCommon += Telegram_OnLogCommon;
//Подписка на логи с ошибками
telegram.OnLogError += Telegram_OnLogError;

// Запуск работы бота
await telegram.Start();

#endregion [запуск telegram бота]



#region [Логгирование]

//Словарик для логгеров
Dictionary<string, Logger> LoggersContainer = new Dictionary<string, Logger>();

void Telegram_OnLogError(Exception ex, long? id = null)
{
	Console.ForegroundColor = ConsoleColor.Red;
	string errorMsg = $"{DateTime.Now}: {ex.ToString()}";

	if(ex is Telegram.Bot.Exceptions.ApiRequestException apiEx)
	{
		errorMsg = $"{DateTime.Now}: {apiEx.ToString()}";
		if(apiEx.Message.Contains("Запрещено: бот заблокирован пользователем"))
		{
			string msg = $"Пользователь {id.GetValueOrDefault()} заблокировал бота - " + apiEx.ToString();
			Telegram_OnLogCommon(msg, TelegramEvents.BlockedBot, ConsoleColor.Red);
			return;
		}
		else if(apiEx.Message.Contains("BUTTON_USER_PRIVACY_RESTRICTED"))
		{
			string msg = $"Пользователь {id.GetValueOrDefault()} заблокировал бота - " + apiEx.ToString();
			Telegram_OnLogCommon(msg, TelegramEvents.BlockedBot, ConsoleColor.Red);
			return;
		}
		else if(apiEx.Message.Contains("Групповой чат был повышен до супергруппового чата"))
		{
			errorMsg += $"\n newChatId: {apiEx?.Parameters?.MigrateToChatId.GetValueOrDefault()}";
		}
	}

	if(LoggersContainer.TryGetValue("Error", out var logger))
	{
		logger.Error(errorMsg);
	}
	else
	{
		var nextLogger = LogManager.GetLogger("Error");
		nextLogger.Error(errorMsg);
		LoggersContainer.Add("Error", nextLogger);
	}
	Console.WriteLine(errorMsg);
	Console.ResetColor();
}

void Telegram_OnLogCommon(string msg, Enum? eventType, ConsoleColor color = ConsoleColor.Blue)
{
	Console.ForegroundColor = color;
	string formatMsg = $"{DateTime.Now}: {msg}";
	Console.WriteLine(formatMsg);
	Console.ResetColor();

	if(eventType != null)
	{
		if(LoggersContainer.TryGetValue(eventType.GetDescription(), out var logger))
		{
			logger.Info(formatMsg);
		}
		else
		{
			var nextLogger = LogManager.GetLogger(eventType.GetDescription());
			nextLogger.Info(formatMsg);
			LoggersContainer.Add(eventType.GetDescription(), nextLogger);
		}
	}


}

#endregion [Логгирование]

#region [Держать консоль открытой]

//Команда для завершения приложения
const string EXIT_COMMAND = "exit";

//Запуск программы
Console.WriteLine("Запуск программы");
Console.WriteLine($"Для закрытия программы напишите {EXIT_COMMAND}");

//Ожидание ввода команды
while(true)
{
	if(Console.ReadLine().ToLower() == EXIT_COMMAND)
	{
		Environment.Exit(0);
	}
}

#endregion [Держать консоль открытой]