namespace HandHistoryParser;
public interface ICommand;

[Command(name: "getallinfo", description:"Вывести общее количество раздач и игроков в базе данных")]
public record ShowAllHandsInformationCommand : ICommand;

[Command(name: "showplayer", description: "Вывести количество раздач в в базе данных на игрока и последние 10 раздач")]
public record ShowPlayerInformationCommand : ICommand {
    [Name("nickname")]
    [Alias("p")]
    [Required]
    public string PlayerNickname { get; init; }
}

[Command(name: "deletehand", description: "Удалить раздачу с выбранным ID")]
public record DeleteHandCommand : ICommand {
    [CommandParameter(name:"handid", shortName: "h")]
    public long HandId { get; init; }
    public DeleteHandCommand(long handId) => HandId = handId;
}

[Command(name: "showdeletedhands", description: "Показать список удаленных раздач")]
public record ShowDeletedHandsCommand : ICommand;

[Command(name: "importfile", description: "Импортировать раздачи из файла")]
public record ImportFileCommand : ICommand {
    [Alias("f")]
    public string Path { get; init; }
    public ImportFileCommand(string path) => Path = path;
}

public class 
InvalidUserInputException: Exception {
    public InvalidUserInputException(string message) : base(message) { }
}
