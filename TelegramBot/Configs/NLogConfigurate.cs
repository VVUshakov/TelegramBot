﻿using ConsoleApp.Models.Enums;
using PRTelegramBot.Extensions;
using System.Reflection;

namespace ConsoleApp.Configs
{
	/// <summary>
	/// Конфигурация системы логирования
	/// </summary>
	public static class NLogConfigurate
	{
		/// <summary>
		/// Базовая директория.
		/// Получает полный путь или расположение UNC загруженного файла, содержащего манифест.
		/// 
		/// Местоположение загруженного файла, содержащего манифест. 
		/// Если сборка загружается из массива байтов, например, при использовании System.Reflection.Assembly.Load(System.Byte[]),
		/// возвращаемое значение представляет собой пустую строку ("").
		/// </summary>
		readonly static string BASEDIR = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

		/// <summary>
		/// Конфигуратор
		/// </summary>
		public static void Configurate()
		{
			var configuration = new NLog.Config.LoggingConfiguration();
			Console.WriteLine(BASEDIR);
			var logsType = Enum.GetValues(typeof(TelegramEvents));

			foreach(var logType in logsType)
			{
				var type = logType.ToString();
				var logfile = new NLog.Targets.FileTarget(type) { FileName = BASEDIR + "/logs/" + type + "/log-${date:format=\\dd.\\MM.\\yyyy}.txt" };
				configuration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, logfile, type);
			}

			string errorType = "Error";
			var logError = new NLog.Targets.FileTarget(errorType) { FileName = BASEDIR + "/logs/" + errorType + "/log-${date:format=\\dd.\\MM.\\yyyy}.txt" };
			configuration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, logError, errorType);

			NLog.LogManager.Configuration = configuration;
		}

		/// <summary>
		/// Метод для записи кастомных логов
		/// </summary>
		/// <param name="message">Сообщение</param>
		/// <param name="eventType">Тип события</param>
		/// <param name="folder">Папка</param>
		/// <returns>Текст сообщения</returns>
		public static string CustomLog(string message, TelegramEvents eventType, long folder)
		{
			string fullPath = Path.Combine(BASEDIR, "logs", eventType.GetDescription(), folder.ToString());
			if(!Directory.Exists(fullPath))
			{
				Directory.CreateDirectory(fullPath);
			}

			using(StreamWriter sw = new StreamWriter(fullPath + $"//log-{DateTime.Now.ToShortDateString()}.txt", true))
			{
				sw.WriteLine(message);
			}
			return message;
		}
	}
}
