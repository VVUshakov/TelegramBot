





//Ожидание ввода команды
const string EXIT_COMMAND = "exit";
while(true)
	if(Console.ReadLine().ToLower() == EXIT_COMMAND)
		Environment.Exit(0);