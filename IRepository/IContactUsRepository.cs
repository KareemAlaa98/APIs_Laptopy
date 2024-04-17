using Laptopy_APIs.DTO;

namespace Laptopy_APIs.IRepository
{
    public interface IContactUsRepository
    {
        void SaveMessage(ContactUsDTO contactDTO);
    }
}
