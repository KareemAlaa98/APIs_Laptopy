using Laptopy_APIs.DTO;
using Laptopy_APIs.Models;

namespace Laptopy_APIs.IRepository
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();
        Brand GetById(int id);

        void Create(BrandDTO brandDto);
        void Edit(BrandDTO brandDto);
        bool Delete(int id);

        List<LaptopDTO> GetLaptopsInBrand(BrandDTO brandDto);
    }
}
