using EShop.Application.Interfaces;
using EShop.Application.Models;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Application.Utilities.Extensions;
using EShop.Application.Utilities.Generators;
using EShop.Application.Utilities.Tools;
using EShop.Domain.Enums.ContactUsEnums;
using EShop.Domain.ViewModels.ContactUsViewModel;
using EShop.Domain.Entities.ContactUs;

namespace EShop.Application.Services;

public class ContactUsService : IContactUsService
{
    #region Field

    private readonly IContactUsRepository _contactUsRepository;
    private readonly IEmailService _emailService;

    #endregion

    #region Constructor

    public ContactUsService(IContactUsRepository contactUsRepository, IEmailService emailService)
    {
        _contactUsRepository = contactUsRepository;
        _emailService = emailService;
    }

    #endregion

    #region Methods

    public async Task<CreateContactUsResult> CreateAsync(CreateContactUsViewModel model)
    {
        if (model.Attachment != null && model.Attachment.Length > 0)
        {
            string imageName = Guid.NewGuid() + Path.GetExtension(model.Attachment.FileName);
            model.Attachment.AddImageToServer(imageName, SiteTools.ContactUsAttachmentPath);
            model.AttachmentPath = imageName;
        }

        var contactUs = new ContactUs
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Mobile = model.Mobile,
            Title = model.Title,
            Description = model.Description,
            AttachmentPath = model.AttachmentPath,
            IsRead = false,
            IsReplied = false,
            CreatedDate = DateTime.Now
        };

        await _contactUsRepository.InsertAsync(contactUs);
        await _contactUsRepository.SaveAsync();

        return CreateContactUsResult.Success;

    }

    public async Task<FilterContactUsViewModel> FilterAsync(FilterContactUsViewModel filter)
    {
        var query = _contactUsRepository.GetAll();

        // اعمال فیلترها
        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(c =>
                c.FirstName.Contains(filter.SearchTerm) ||
                c.LastName.Contains(filter.SearchTerm) ||
                c.Email.Contains(filter.SearchTerm) ||
                c.Mobile.Contains(filter.SearchTerm) ||
                c.Title.Contains(filter.SearchTerm) ||
                c.Description.Contains(filter.SearchTerm) ||
                c.AdminAnswer.Contains(filter.SearchTerm));
        }

        // مرتب‌سازی
        query = query.OrderByDescending(c => c.CreatedDate);

        // پیجینگ
        await filter.Paging(query.Select(c => new ContactUsDetailsViewModel
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Mobile = c.Mobile,
            Title = c.Title,
            Description = c.Description,
            AttachmentPath = c.AttachmentPath,
            AdminAnswer = c.AdminAnswer,
            IsRead = c.IsRead,
            IsReplied = c.IsReplied,
            CreatedDate = c.CreatedDate
        }));

        return filter;
    }

    public async Task<ContactUsDetailsViewModel> GetDetailsByIdAsync(int id)
    {
        var contact = await _contactUsRepository.GetByIdAsync(id);

        if (contact == null) return null;

        // علامت‌گذاری پیام به عنوان خوانده شده
        if (!contact.IsRead)
        {
            contact.IsRead = true;
            _contactUsRepository.Update(contact);
            await _contactUsRepository.SaveAsync();
        }

        return new ContactUsDetailsViewModel
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            Mobile = contact.Mobile,
            Title = contact.Title,
            Description = contact.Description,
            AttachmentPath = contact.AttachmentPath,
            AdminAnswer = contact.AdminAnswer,
            IsRead = contact.IsRead,
            IsReplied = contact.IsReplied,
            CreatedDate = contact.CreatedDate
        };
    }

    public async Task<AnswerResult> AnswerAsync(ContactUsDetailsViewModel model)
    {
        var contactUs = await _contactUsRepository.GetByIdAsync(model.Id);

        if (contactUs == null)
            return AnswerResult.ContactUsNotFound;

        if (string.IsNullOrEmpty(model.AdminAnswer))
            return AnswerResult.AnswerIsNull;

        // آپلود فایل پیوست
        if (model.Attachment != null)
        {
            string imageName = Guid.NewGuid() + Path.GetExtension(model.Attachment.FileName);
            model.Attachment.AddImageToServer(imageName, SiteTools.AdminResponseAttachmentPath);
            contactUs.AttachmentPath = imageName;
        }
        
        contactUs.AdminAnswer = model.AdminAnswer;
        contactUs.IsReplied = true;
        contactUs.IsRead = true;

        _contactUsRepository.Update(contactUs);
        await _contactUsRepository.SaveAsync();

        // ارسال ایمیل به کاربر
        var email = new Email
        {
            To = contactUs.Email,
            Subject = "پاسخ به پیام شما",
            Body = model.AdminAnswer
        };
        _emailService.SendAdminResponseEmail(email);

        return AnswerResult.Success;
    }

    public async Task MarkAsReadAsync(int id)
    {
        var contact = await _contactUsRepository.GetByIdAsync(id);

        if (contact != null && !contact.IsRead)
        {
            contact.IsRead = true;
            _contactUsRepository.Update(contact);
            await _contactUsRepository.SaveAsync();
        }


    }

    #endregion
}