using LanguageService.Application.DTOs.KafkaDto;
using LanguageService.Application.Interfaces;
using LanguageService.Application.Interfaces.Kafka;
using LanguageService.Application.Mapper;

namespace LanguageService.Application.Services
{
    public class KafkaService(ILanguageRepository repository, IKafkaProducer producer)
    {
        private readonly ILanguageRepository _repository = repository;
        private readonly IKafkaProducer _producer = producer;

        public async Task GetLanguageByNameForKafka(GetLanguageRequest getLanguageRequest)
        {
            var languageDto = await _repository.GetLanguageByName(getLanguageRequest.Name);
            var getLanguageResponse = new GetLanguageResponse()
            {
                CorrelationID = getLanguageRequest.CorrelationID,
                Data = languageDto?.ToReadLanguage()
            };
            await _producer.ProduceAsync("get-language-by-name", getLanguageRequest, getLanguageRequest.CorrelationID);
        }
    }
}