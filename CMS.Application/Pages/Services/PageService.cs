using AutoMapper;
using CMS.Application.Common;
using CMS.Application.Common.Utils;
using CMS.Application.Pages.Dtos;
using CMS.Domain.Admin.Pages;
using CMS.Domain.Admin.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMS.Application.Pages.Services
{
    public class PageService
    {
        private readonly IRepository<Page> _repository;
        private readonly SlugGenerator<Page> _slugGenerator;
        private readonly IMapper _mapper;
        public PageService(IRepository<Page> repository, SlugGenerator<Page> slugGenerator, IMapper mapper)
        {
            _repository = repository;
            _slugGenerator = slugGenerator;
            _mapper = mapper;
        }

        public async Task<ServiceResult> CreatePage(PageDto pageDto)
        {
            if (string.IsNullOrWhiteSpace(pageDto.Title))
                return ServiceResult.Fail("عنوان صفحه اجباری میباشد");

            pageDto.Slug = await _slugGenerator.CheckAndGenerateSlug(pageDto.Title);

            pageDto.Order = 100;

            Page page = _mapper.Map<Page>(pageDto);

            await _repository.AddAsync(page);

            return ServiceResult.Ok("صفحه با موفقیت اضافه شد");
        }


        public async Task<ServiceResult> EditPage(PageDto pageDto)
        {
            if (string.IsNullOrWhiteSpace(pageDto.Title))
                return ServiceResult.Fail("عنوان صفحه اجباری میباشد");

            pageDto.Slug = await _slugGenerator.CheckAndGenerateSlug(pageDto.Title);


            Page page = _mapper.Map<Page>(pageDto);

            await _repository.UpdateAsync(page);

            return ServiceResult.Ok("صفحه با موفقیت ویرایش شد");
        }

        public async Task<ServiceResult> DeletePage(int id)
        {
            var page = await _repository.GetByIdAsync(id);


            if (page is null || id is 1)
                throw new Exception("این صفحه وجود ندارد یا حذف آن مجاز نیست");

            await _repository.DeleteAsync(page);

            return ServiceResult.Ok("صفحه با موفقیت حذف شد");
        }

        public async Task PageOrder(int[] id)
        {
            int count = 1;

            foreach (var pageId in id)
            {
                var page = await _repository.GetByIdAsync(pageId);

                page.Order = count;

                await _repository.SaveAsync();

                count++;
            }
        }


    }
}
