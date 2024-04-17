using Laptopy_APIs.Models;
using Laptopy_APIs.DTO;

namespace Laptopy_APIs.IRepository
{
    public interface IProductImageRepository
    {
        List<ProductImage> GetAll();
        ProductImage GetById(int id);

        void Create(ProductImageDTO image);
        void Edit(ProductImageDTO image);
        bool Delete(int id);
        public string GetLaptopName(ProductImageDTO prodImgDto);
    }
}
