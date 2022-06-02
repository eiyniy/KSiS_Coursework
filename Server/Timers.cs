using System.Collections.Concurrent;
namespace Server;

public static class Timers
{
    private static ConcurrentQueue<(TimerCallback, object, int, int)> _timerTemplates =
        new ConcurrentQueue<(TimerCallback, object, int, int)>();


    public static void Start()
    {
        foreach (var template in _timerTemplates)
        {
            Timer timer = new Timer(template.Item1, template.Item2, template.Item3, template.Item4);
        }
    }

    public static void Free() => _timerTemplates =
        new ConcurrentQueue<(TimerCallback, object, int, int)>();

    public static void Add(TimerCallback timer, object param, int offset, int timespan) =>
        _timerTemplates.Enqueue((timer, param, offset, timespan));
}