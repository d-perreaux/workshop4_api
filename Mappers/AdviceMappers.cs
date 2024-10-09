using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using api.Dtos.Advice;
using api.Models;


namespace api.Mappers
{
    public static class AdviceMapper
    {
        public static AdviceDto ToAdviceDto(this Advice adviceModel){
            return new AdviceDto{
                AdviceId = adviceModel.AdviceId,
                Content = adviceModel.Content,
                Type = adviceModel.Type,
                DateStart = adviceModel.DateStart,
                DateEnd = adviceModel.DateEnd,
                FlagIsDeleted = adviceModel.FlagIsDeleted
            };
        }

        public static Advice ToAdviceFromCreateDto(this CreateAdviceRequestDto dto)
        {
            return new Advice
            {
                Content = dto.Content,
                Type = dto.Type,
                DateStart = dto.DateStart,
                DateEnd = dto.DateEnd,
                FlagIsDeleted = false
            };
        }

        public static void UpdateFromDto(this Advice advice, UpdateAdviceRequestDto dto)
        {
            advice.Content = dto.Content;
            advice.Type = dto.Type;
            advice.DateStart = dto.DateStart;
            advice.DateEnd = dto.DateEnd;
            advice.FlagIsDeleted = dto.FlagIsDeleted;
        }
    }
}