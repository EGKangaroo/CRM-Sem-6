using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM_Contact_Service.Data;
using CRM_Contact_Service.Transfer;

namespace CRM_Contact_Service.Converter
{
    public static class ContactInfoConverter
    {
        public static ContactInfoDAO RequestToDAO(ContactInfoRequest request)
        {
            ContactInfoDAO data = new ContactInfoDAO();

            data.Name = request.Name;
            data.Email = request.Email;
            data.Telephone = request.Telephone;
            if(request.TelephoneAlt != null)
            {
                data.TelephoneAlt = request.TelephoneAlt;
            }
            data.Street = request.Street;
            data.City = request.City;
            data.Country = request.Country;
            data.PostalCode = request.PostalCode;

            return data;
        }

        public static ContactInfoResponse DAOToResponse(ContactInfoDAO data)
        {
            ContactInfoResponse response = new ContactInfoResponse();

            response.ContactInfoId = data.ContactInfoId;
            response.Email = data.Email;
            response.Name = data.Name;
            response.Telephone = data.Telephone;
            if(data.TelephoneAlt != null)
            {
                response.TelephoneAlt = data.TelephoneAlt;
            }
            response.Street = data.Street;
            response.City = data.City;
            response.Country = data.Country;
            response.PostalCode = data.PostalCode;

            return response;
        }

        public static void RequestToExistingDAO(ContactInfoRequest request, ContactInfoDAO dao)
        {
            dao.Email = request.Email;
            dao.Name = request.Name;
            dao.Telephone = request.Telephone;
            dao.TelephoneAlt = request.TelephoneAlt;
            dao.Street = request.Street;
            dao.City = request.City;
            dao.Country = request.Country;
            dao.PostalCode = request.PostalCode;
        }
    }
}
