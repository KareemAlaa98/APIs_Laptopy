using Laptopy_APIs.Data;
using Laptopy_APIs.Models;
using Laptopy_APIs.DTO;
using Laptopy_APIs.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Laptopy_APIs.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        ApplicationDbContext context;

        public ContactUsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void SaveMessage(ContactUsDTO contactDTO)
        {
            if (contactDTO != null)
            {
                var messageInfo = new ContactUs()
                {
                     Name = contactDTO.Name,
                     Email = contactDTO.Email,
                     Subject = contactDTO.Subject,
                     Message = contactDTO.Message
                };
                 context.ContactUs.Add(messageInfo);
                 context.SaveChanges();
                contactDTO.Id = messageInfo.Id;
            }
        }
    }
}
