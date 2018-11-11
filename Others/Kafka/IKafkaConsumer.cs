using System.Threading;
using System.Threading.Tasks;

namespace CM.Shared.Kernel.Others.Kafka
{
    public interface IKafkaConsumer
    {
        Task Listen(CancellationToken stoppingToken);
    }
}
