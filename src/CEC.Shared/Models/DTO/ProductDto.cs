﻿using CEC.Domain.Entities;

using System.ComponentModel.DataAnnotations;

namespace CEC.Shared.Models.DTO
{
    public class ProductDto
    {
        public ProductDto(long id, string name, string brief, string thumbnailUrl)
        {
            Id = id;
            Name = name;
            Brief = brief;
            ThumbnailUrl = thumbnailUrl;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Brief { get; set; }
        public string ThumbnailUrl { get; set; }
    }

    public class ProductCreateModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Brief { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }

        public Product ToProduct()
        {
            return new Product
            {
                Name = this.Name,
                Brief = this.Brief,
                ThumbnailUrl = this.ThumbnailUrl,
                Description = this.Description,
            };
        }
    }
}