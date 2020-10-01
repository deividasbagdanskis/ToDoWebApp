using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InDbProviders
{
    public class InDbToDoItemTagProvider : IInDbToDoItemTagProvider
    {
        private readonly SampleWebAppContext _context;
        private readonly IMapper _mapper;

        public InDbToDoItemTagProvider(SampleWebAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(ToDoItemTagVo toDoItemTag)
        {
            ToDoItemTagDao toDoItemTagDao = _mapper.Map<ToDoItemTagDao>(toDoItemTag);
            toDoItemTagDao.Tag = await _context.Tag.FindAsync(toDoItemTagDao.TagId);
            toDoItemTagDao.ToDoItem = await _context.ToDoItem.FindAsync(toDoItemTagDao.ToDoItemId);

            _context.Add(toDoItemTagDao);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? toDoItemId, int? tagId, string userId)
        {
            ToDoItemTagDao toDoItemTag = await _context.ToDoItemTag
                .Where(t => t.ToDoItemId == toDoItemId && t.TagId == tagId && t.UserId == userId).FirstOrDefaultAsync();
            _context.ToDoItemTag.Remove(toDoItemTag);
            await _context.SaveChangesAsync();
        }

        public async Task<ToDoItemTagVo> Get(int? toDoItemId, int? tagId, string userId)
        {
            ToDoItemTagDao foundToDoItemTag = await _context.ToDoItemTag
                .Include(t => t.Tag)
                .Include(t => t.ToDoItem)
                .FirstOrDefaultAsync(t => t.ToDoItemId == toDoItemId && t.TagId == tagId && t.UserId == userId);

            return _mapper.Map<ToDoItemTagVo>(foundToDoItemTag);
        }

        public async Task<IEnumerable<ToDoItemTagVo>> GetAll(string userId)
        {
            IEnumerable<ToDoItemTagDao> toDoItemTagDaos = await _context.ToDoItemTag.Where(t => t.UserId == userId)
                .Include(t => t.Tag).Include(t => t.ToDoItem).ToListAsync();

            return _mapper.Map<IEnumerable<ToDoItemTagVo>>(toDoItemTagDaos);
        }

        public async Task Update(ToDoItemTagVo toDoItemTag)
        {
            ToDoItemTagDao toDoItemTagDao = _mapper.Map<ToDoItemTagDao>(toDoItemTag);

            _context.Update(toDoItemTagDao);
            _context.Entry(toDoItemTagDao).Property("UserId").IsModified = false;

            await _context.SaveChangesAsync();
        }

        public bool ItemExits(int toDoItemId, int tagId)
        {
            return _context.ToDoItemTag.Any(e => e.ToDoItemId == toDoItemId && e.TagId == tagId);
        }
    }
}
