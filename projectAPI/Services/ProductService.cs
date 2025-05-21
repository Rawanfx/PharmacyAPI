using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using projectAPI.Core.Domain.Entities;
using projectAPI.Core.DTO;
using projectAPI.Core.ServiceCotract;

namespace projectAPI.Core.Services
{
    public class ProductService : IProductService
    {
       private readonly AppDbContext context;
        private readonly IFileServices fileServices;
        private readonly ILogger<ProductService> logger;
        public ProductService(AppDbContext context, IFileServices fileServices, 
            ILogger<ProductService> logger)
        {
            this.context = context;
            this.fileServices = fileServices;
            this.logger = logger;
        }
        public List<ProductDTO> GetProductWithCategoryId(int id)
        {
            //search in product table
            var result = context.Product.Where(x => x.category_id == id)
                .Select(x=>new ProductDTO{name =x.name,
                    price=x.price,
                  quantity= x.quatity,
                    image= x.ImageUrl,
                    description=x.description
                    
                });
            return result.Any() ? result.ToList() : new List<ProductDTO>();
        }

        public List<TopProductDTO> GetTopProducts()
        {
            var temp = context.Review.Select(x => new TopProductDTO
            { name = x.product.name, 
                rate = (int)x.rate,
                image=x.product.ImageUrl ,
                descriptio=x.product.description}).OrderByDescending(x => x.rate).ToList();
            return temp;
        }

        public List<ProductDTO> Search(string input)
        {
            var res = context.Product
                .Where(x => x.name.Contains(input.ToLower()))
                .Select(x => new ProductDTO { name = x.name, price = x.price, image = x.ImageUrl, quantity = x.quatity }).ToList();
            return res;
        }
        public ProductDetails? GetProductDetails(int id)
        {
            ProductDetails productDetails = new ProductDetails();
            var result = context.Product
                .Where(x => x.id == id)
                .Select(x => new  { desc= x.description , name =x.name,image=x.ImageUrl})
                .FirstOrDefault();
            if (result == null)
                return productDetails;
            productDetails.name = result.name;
            productDetails.desciption = result.desc;
            productDetails.image = result.image;

            return productDetails;

        }
        public Product GetProductWithId(int id)
        {
            return context.Product.Where(x => x.id == id).FirstOrDefault();
        }

        public bool AddProduct(ProductCreateDTO productFromUser)
        {
            var result = context.Product.Any(x => x.name == productFromUser.name);
            if (result) return false;
            try
            {
                IFormFile image = productFromUser.image;
                string imgPath =  fileServices.Upload(image);

                Product product = new Product()
                {
                    description = productFromUser.description,
                    ImageUrl = imgPath,
                    name = productFromUser.name,
                    price = productFromUser.price,
                    quatity = productFromUser.quantity,
                    category_id = productFromUser.category_id,

                };

               context.Product.Add(product);
                 context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public double CalculatePrice(int count, int product_id)
        {
            var result = context.Product.Where(x => x.id == product_id)
                .Select(x => x.price)
                .FirstOrDefault();
            return count * result;
        }

        public bool delete(int id)
        {
            var result = GetProductWithId(id);

            try
            {
               context.Product.Remove(result);
                context.SaveChanges();
                return true; 
            }
            catch
            {
                return false;
            }
        }
        public bool Update(int id, ProductDTO dto)
        {
            Product existingProduct = GetProductWithId(id);
            if (existingProduct == null)
                return false;
            existingProduct.name = dto.name;
            existingProduct.quatity = dto.quantity;
            existingProduct.description = dto.description;

            context.SaveChanges();
            return true;

        }
    }
}
