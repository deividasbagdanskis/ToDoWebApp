﻿using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using AutoMapper;

namespace ToDoApp.Business.Services.InDbProviders
{
    public class InDbTagProvider : IAsyncDbDataProvider<TagVo>
    {
        private readonly SampleWebAppContext _context;
        private readonly IMapper _mapper;

        public InDbTagProvider(SampleWebAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(TagVo tag)
        {
            TagDao tagDao = _mapper.Map<TagDao>(tag); 
            _context.Add(tagDao);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, string userId)
        {
            var tag = await _context.Tag.Where(t => t.Id == id && t.UserId == userId).FirstOrDefaultAsync();
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<TagVo> Get(int? id, string userId)
        {
            var foundTag = await _context.Tag.Where(t => t.Id == id && t.UserId == userId).FirstOrDefaultAsync();

            foundTag.ToDoItemNumber = _context.ToDoItemTag.Where(t => t.TagId == foundTag.Id).Count();

            return _mapper.Map<TagVo>(foundTag);
        }

        public async Task<IEnumerable<TagVo>> GetAll(string userId)
        {
            IEnumerable<TagDao> tagDaos = await _context.Tag.Where(t => t.UserId == userId).ToListAsync();

            foreach (TagDao tagDao in tagDaos)
            {
                tagDao.ToDoItemNumber = _context.ToDoItemTag.Where(t => t.TagId == tagDao.Id).Count();
            }

            return _mapper.Map<IEnumerable<TagVo>>(tagDaos);
        }

        public async Task Update(TagVo tag)
        {
            TagDao tagDao = _mapper.Map<TagDao>(tag);
            _context.Update(tagDao);
            _context.Entry(tagDao).Property("UserId").IsModified = false;
            await _context.SaveChangesAsync();
        }

        public bool ItemExits(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
