using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradesparq.Dto.ResponsiveDto;
using Tradesparq.Model.Company;
using Tradesparq.Model.EnumModel;
using Tradesparq.Model.Info;
using Tradesparq.Respostory.Abtrac;
using TradesparqDBContext;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tradesparq.Respostory
{
    public class CompanyRespostory : ICompanyRespostory
    {
        protected readonly AppDbContext _db;
        public CompanyRespostory(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<int?> CreateCompany(SearchCompanyResponsivDto.Bucket company)
        {
            try
            {
                bool isHas = (from c in _db.Company 
                              where c.Uuid == company.info.uuid
                              select c).Any();
                if (isHas)
                {
                    throw new Exception("Đã tồn tại Company");
                }
                Companies Company = new Companies()
                {
                    Address = company.info.address,
                    Country = company.info.country,
                    Name = company.info.name,
                    Uid = company.info.uid,
                    Uname = company.info.uname,
                    Uuid = company.info.uuid,
                    Total = company.total,
                    Count = company.count,
                    Var = company.val,
                };
                _db.Company.Add(Company);
                await _db.SaveChangesAsync();
                return Company.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> CreateCitiesForCompany(int CompanyId, List<string> cities)
        {
            try
            {
                if (cities == null)
                    throw new ArgumentNullException("City is Null");
                List<Cities> listCity = new List<Cities>();
                foreach (var city in cities)
                {
                    listCity.Add(new Cities()
                    {
                        CompanyId = CompanyId,
                        TypeInfo = (int)TypeInfoEnum.Company,
                        City = city
                    });
                }
                _db.City.AddRange(listCity);
                await _db.SaveChangesAsync(); 
                return true;
            }
            catch (Exception ex)
            {
                return false;
            } 
        }

        public async Task<bool> CreateCityForCompany(int CompanyId, string city)
        {
            try
            {
                if (string.IsNullOrEmpty(city))
                    throw new ArgumentNullException("City is Null");
                Cities citys = new Cities()
                {
                    CompanyId = CompanyId,
                    TypeInfo = (int)TypeInfoEnum.Company,
                    City = city,
                };
                _db.City.Add(citys);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
        public async Task<bool> CreateEmailForCompany(int CompanyId, string email)
        {
            try
            {
                if(string.IsNullOrEmpty(email))
                    throw new ArgumentNullException("Email Empty");
                Emails emails = new Emails()
                {
                    CompanyId = CompanyId,
                    TypeInfo = (int)TypeInfoEnum.Company,
                    Email = email,
                };
                _db.Email.Add(emails);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateEmailsForCompany(int CompanyId, List<string> emails)
        {
            try
            {
                if (emails == null)
                    throw new ArgumentNullException("Emails is Null");
                List<Emails> listEmails = new List<Emails>();
                foreach (var email in emails)
                {
                    listEmails.Add(new Emails()
                    {
                        CompanyId = CompanyId,
                        TypeInfo = (int)TypeInfoEnum.Company,
                        Email = email
                    });
                }
                _db.Email.AddRange(listEmails);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateFaxForCompany(int CompanyId, string fax)
        {
            try
            {
                if (string.IsNullOrEmpty(fax))
                    throw new ArgumentNullException("Fax is Null");
                Faxs faxs = new Faxs()
                {
                    CompanyId = CompanyId,
                    TypeInfo = (int)TypeInfoEnum.Company,
                    Fax = fax
                };
                _db.Fax.Add(faxs);
                await _db.SaveChangesAsync();
                return true; 
            }
            catch (Exception ex)
            {
                return false;
            } 
        }

        public async Task<bool> CreateFaxsForCompany(int CompanyId, List<string> faxs)
        {
            try
            {
                if (faxs == null)
                    throw new ArgumentNullException("Faxs is Null");
                List<Faxs> listFaxs = new List<Faxs>();
                foreach (var fax in faxs)
                {
                    listFaxs.Add(new Faxs()
                    {
                        CompanyId = CompanyId,
                        TypeInfo = (int)TypeInfoEnum.Company,
                        Fax = fax
                    });
                }
                _db.Fax.AddRange(listFaxs);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreatePhoneNumberForCompany(int CompanyId, string phoneNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phoneNumber))
                    throw new ArgumentNullException("PhoneNumber is Null");
                PhoneNumbers phoens = new PhoneNumbers()
                {
                    CompanyId = CompanyId,
                    TypeInfo = (int)TypeInfoEnum.Company,
                    Phone = phoneNumber,
                };
                _db.PhoneNumber.Add(phoens);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreatePhoneNumbersForCompany(int CompanyId, List<string> phoneNumbers)
        {
            try
            {
                if (phoneNumbers == null)
                    throw new ArgumentNullException("PhoneNumber is Null");
                List<PhoneNumbers> listPhoneNumbers = new List<PhoneNumbers>();
                foreach (var phoneNumber in phoneNumbers)
                {
                    listPhoneNumbers.Add(new PhoneNumbers()
                    {
                        CompanyId = CompanyId,
                        TypeInfo = (int)TypeInfoEnum.Company,
                        Phone = phoneNumber
                    });
                }
                _db.PhoneNumber.AddRange(listPhoneNumbers);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreatePostalCodeForCompany(int CompanyId, string postalCodes)
        {
            try
            {
                if (string.IsNullOrEmpty(postalCodes))
                    throw new ArgumentNullException("PostalCodes is Null");
                PostalCodes postal = new PostalCodes()
                {
                    CompanyId = CompanyId,
                    TypeInfo = (int)TypeInfoEnum.Company,
                    PostalCode = postalCodes,
                };
                _db.PostalCode.Add(postal);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreatePostalCodesForCompany(int CompanyId, List<string> postalCodes)
        {
            try
            {
                if (postalCodes == null)
                    throw new ArgumentNullException("PostalCodes is Null");
                List<PostalCodes> listPostalCodes = new List<PostalCodes>();
                foreach (var postalCode in postalCodes)
                {
                    listPostalCodes.Add(new PostalCodes()
                    {
                        CompanyId = CompanyId,
                        TypeInfo = (int)TypeInfoEnum.Company,
                        PostalCode = postalCode
                    });
                }
                _db.PostalCode.AddRange(listPostalCodes);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
