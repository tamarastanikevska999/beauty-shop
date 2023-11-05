using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API.DTO
{
    [Serializable]
    [DataContract(Name = "product", Namespace = "products")]
    public class ProductDto
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "price", EmitDefaultValue = false)]
        public decimal Price { get; set; }

        [DataMember(Name = "picture-url", EmitDefaultValue = false)]
        public string PictureUrl { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string ProductType { get; set; }

        [DataMember(Name = "brand", EmitDefaultValue = false)]
        public string ProductBrand { get; set; }
    }
}