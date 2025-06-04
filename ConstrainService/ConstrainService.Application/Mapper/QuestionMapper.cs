using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConstrainService.Application.DTOs.Constrain;
using ConstrainService.Domain.Entities;

namespace ConstrainService.Application.Mapper
{
    public static class QuestionMapper
    {
        public static Constrain ToConstrain(this AddConstrainDto addConstrainDto)
        {
            return new Constrain()
            {
                QuestionId = addConstrainDto.QuestionId,
                InputDescription = addConstrainDto.InputDescription,
                OutputDescription = addConstrainDto.OutputDescription,
                AdditionalNotes = addConstrainDto.AdditionalNotes,
                MemoryLimit = addConstrainDto.MemoryLimit,
                TimeLimit = addConstrainDto.TimeLimit,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static ReadConstrainDto ToReadConstrain(this Constrain constrain)
        {
            return new ReadConstrainDto()
            {
                ConstrainId = constrain.ConstrainId,
                QuestionId = constrain.QuestionId,
                InputDescription = constrain.InputDescription,
                OutputDescription = constrain.OutputDescription,
                AdditionalNotes = constrain.AdditionalNotes,
                MemoryLimit = constrain.MemoryLimit,
                TimeLimit = constrain.TimeLimit,
                UpdatedAt = constrain.UpdatedAt,
                CreatedAt = constrain.CreatedAt
            };
        }
    }
}