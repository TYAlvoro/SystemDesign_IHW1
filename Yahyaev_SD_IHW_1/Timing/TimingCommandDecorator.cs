using System.Diagnostics;

namespace Yahyaev_SD_IHW_1.Timing;

public class TimingCommandDecorator : ICommand
{
    private readonly ICommand _command;

    public TimingCommandDecorator(ICommand command)
    {
        _command = command;
    }

    public void Execute()
    {
        Stopwatch sw = Stopwatch.StartNew();
        _command.Execute();
        sw.Stop();
        Console.WriteLine($"Создание аккаунта заняло: {sw.ElapsedMilliseconds} ms.");
    }
}