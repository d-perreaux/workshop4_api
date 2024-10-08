using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using api.Dtos.Pharmacy;
using api.Models;

namespace api.Mappers
{
    public static class PharmacyMapper
    {
        public static PharmacyDto ToPharmacyDto(this Pharmacy pharmacyModel){
            return new PharmacyDto{
                PharmacyId = pharmacyModel.PharmacyId,
                Name = pharmacyModel.Name,
            };
        }

        public static Pharmacy ToPharmacyFromCreateDto(this CreatePharmacyRequestDto dto)
        {
            return new Pharmacy
            {
                Name = dto.Name
            };
        }
    }
}