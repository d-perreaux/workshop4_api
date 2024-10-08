using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using api.Dtos.Product;
using api.Models;

namespace api.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel){
            return new ProductDto{
                ProductId = productModel.ProductId,
                Name = productModel.Name,
                CIP = productModel.CIP,
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductRequestDto productDto){
            return new Product{
                Name = productDto.Name,
                CIP = productDto.CIP,
            };
        }
    }
}