﻿namespace ServerApp.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
      
    }
}
