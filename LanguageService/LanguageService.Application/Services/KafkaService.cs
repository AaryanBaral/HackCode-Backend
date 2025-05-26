using System.Text.Json;
using LanguageService.Application.Constant;
using LanguageService.Application.DTOs.KafkaDto;
using LanguageService.Application.Interfaces;
using LanguageService.Application.Interfaces.Kafka;
using LanguageService.Application.Mapper;

namespace LanguageService.Application.Services
{
    public class KafkaService(ILanguageRepository repository, IKafkaProducer producer):IKafkaService
    {
        private readonly ILanguageRepository _repository = repository;
        private readonly IKafkaProducer _producer = producer;

        public async Task GetLanguageByNameForKafka(GetLanguageRequest getLanguageRequest)
        {
            Console.WriteLine(JsonSerializer.Serialize(getLanguageRequest));
            Console.WriteLine("------------------------");
            var languageDto = await _repository.GetLanguageByName(getLanguageRequest.Name);
            Console.WriteLine(JsonSerializer.Serialize(languageDto));
            var getLanguageResponse = new GetLanguageResponse()
            {
                CorrelationID = getLanguageRequest.CorrelationID,
                Data = languageDto?.ToReadLanguage()
            };
            Console.WriteLine("ResposneDto");
            Console.WriteLine(JsonSerializer.Serialize(getLanguageResponse));
            await _producer.ProduceAsync(KafkaTopics.GetLanguageByNameResponse, getLanguageResponse, getLanguageRequest.CorrelationID);
        }
    }
}