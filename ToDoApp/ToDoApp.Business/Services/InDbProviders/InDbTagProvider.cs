using Microsoft.EntityFrameworkCore;
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

        public async Task Delete(int id)
        {
            var tag = await _context.Tag.FindAsync(id);
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<TagVo> Get(int? id)
        {
            var foundTag = await _context.Tag.FirstOrDefaultAsync(t => t.Id == id);

            foundTag.ToDoItemNumber = _context.ToDoItemTag.Where(t => t.TagId == foundTag.Id).Count();

            return _mapper.Map<TagVo>(foundTag);
        }

        public async Task<IEnumerable<TagVo>> GetAll()
        {
            IEnumerable<TagDao> tagDaos = await _context.Tag.ToListAsync();

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
            await _context.SaveChangesAsync();
        }

        public bool ItemExits(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
