using Laptopy_APIs.DTO;
using Laptopy_APIs.Models;

namespace Laptopy_APIs.IRepository
{
    public interface ILaptopRepository
    {
        List<Laptop> ReadAll();
        Laptop ReadById(int id);

        void Create(LaptopDTO lapDto);

        void Update(LaptopDTO lapDto);

        bool Delete(int id);
        List<Laptop> Search(string search);
        string GetLaptopBrandName(LaptopDTO dto);
    }
}
