using LanguageService.Application.DTOs.KafkaDto;

namespace LanguageService.Application.Interfaces.Kafka;

public interface IKafkaService
{
    Task GetLanguageByNameForKafka(GetLanguageRequest getLanguageRequest);
}