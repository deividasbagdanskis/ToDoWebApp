﻿using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToDoApp.Business.Models;
using ToDoApp.Commons.Exceptions;

namespace ToDoApp.Business.Services.InDbProviders
{
    public class InDbCategoryProvider : IAsyncDbDataProvider<CategoryVo>
    {
        private readonly SampleWebAppContext _context;
        private readonly IMapper _mapper;

        public InDbCategoryProvider(SampleWebAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(CategoryVo category)
        {
            ValidateCategoryNameLength(category.Name);

            CategoryDao categoryDao = _mapper.Map<CategoryDao>(category);
            _context.Add(categoryDao);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryVo> Get(int? id)
        {
            var foundCategory = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<CategoryVo>(foundCategory);
        }

        public async Task<IEnumerable<CategoryVo>> GetAll(string userId)
        {
            IEnumerable<CategoryDao> categoryDaos = await _context.Category.Where(c => c.UserId == userId).ToListAsync();

            return _mapper.Map<IEnumerable<CategoryVo>>(categoryDaos);
        }

        public async Task Update(CategoryVo category)
        {
            ValidateCategoryNameLength(category.Name);

            CategoryDao categoryDao = _mapper.Map<CategoryDao>(category);

            _context.Update(categoryDao);
            await _context.SaveChangesAsync();
        }

        public bool ItemExits(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }

        private void ValidateCategoryNameLength(string name)
        {
            if (name.Length <= 2)
            {
                throw new CategoryNameException(name);
            }
        }
    }
}
