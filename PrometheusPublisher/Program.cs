using PrometheusPublisher;
using PrometheusPublisher.Configuration;

ConfigFactory factory = new ConfigFactory();

var publisher = new PublishPerformance(factory);

CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
CancellationToken token = cancellationTokenSource.Token;

Task publishTask = Task.Run(() => publisher.Publish());
Task publishConfigTask = Task.Run(() => publisher.PublishConfigMetrics());

Task.WaitAll(publishTask, publishConfigTask);

#region Starting methods

void RunInfiniteLoop(CancellationToken cancellationToken)
{
    while (!cancellationToken.IsCancellationRequested)
    {
        publisher.Publish();
    }

    Console.WriteLine("Loop stopped gracefully.");
}

#endregion
