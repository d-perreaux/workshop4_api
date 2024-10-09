using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using api.Dtos.Prescription;
using api.Models;

namespace api.Mappers
{
    public static class PrescriptionMapper
    {
        public static PrescriptionDto ToPrescriptionDto(this Prescription prescription)
        {
            return new PrescriptionDto
            {
                PrescriptionId = prescription.PrescriptionId,
                PharmacyId = prescription.PharmacyId,
                Date = prescription.Date
            };
        }

        public static Prescription ToPrescriptionFromCreateDto(this CreatePrescriptionRequestDto dto)
        {
            return new Prescription
            {
                PharmacyId = dto.PharmacyId,
                Date = dto.Date
            };
        }

        public static void UpdateFromDto(this Prescription prescription, UpdatePrescriptionRequestDto dto)
        {
            prescription.PharmacyId = dto.PharmacyId;
            prescription.Date = dto.Date;
        }
    }
}
