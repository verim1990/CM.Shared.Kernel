namespace CM.Shared.Kernel.Others.Kafka
{
    public class KafkaServiceSettings
    {
        public KafkaProducerSettings Producer { get; set; } = new KafkaProducerSettings();

        public KafkaConsumerSettings Consumer { get; set; } = new KafkaConsumerSettings();
    }

    public class KafkaConsumerSettings
    {
        public string GroupId { get; set; } = "";

        public string[] Topics { get; set; } = new string[0];
    }

    public class KafkaProducerSettings
    {
        public string Topic { get; set; }
    }
}
