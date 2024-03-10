




#region [запуск telegram бота]
using PRTelegramBot.Core;

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

// запус работы бота
await telegram.Start();

#endregion [запуск telegram бота]


#region [Держать консоль открытой]

//Команда для завершения приложения
const string EXIT_COMMAND = "exit";
//Запуск программы
Console.WriteLine("Запуск программы");
Console.WriteLine($"Для закрытия программы напишите {EXIT_COMMAND}");
//Ожидание ввода команды
while(true)
	if(Console.ReadLine().ToLower() == EXIT_COMMAND)
		Environment.Exit(0);

#endregion [Держать консоль открытой]