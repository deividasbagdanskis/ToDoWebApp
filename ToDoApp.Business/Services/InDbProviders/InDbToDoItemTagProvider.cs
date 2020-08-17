using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Data;
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
            _context.Add(toDoItemTagDao);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? toDoItemId, int? tagId)
        {
            ToDoItemTagDao toDoItemTag = await _context.ToDoItemTag.FindAsync(toDoItemId, tagId);
            _context.ToDoItemTag.Remove(toDoItemTag);
            await _context.SaveChangesAsync();
        }

        public async Task<ToDoItemTagVo> Get(int? toDoItemId, int? tagId)
        {
            ToDoItemTagDao foundToDoItemTag = await _context.ToDoItemTag
                .Include(t => t.Tag)
                .Include(t => t.ToDoItem)
                .FirstOrDefaultAsync(m => m.ToDoItemId == toDoItemId && m.TagId == tagId);

            return _mapper.Map<ToDoItemTagVo>(foundToDoItemTag);
        }

        public async Task<IEnumerable<ToDoItemTagVo>> GetAll()
        {
            IEnumerable<ToDoItemTagDao> toDoItemTagDaos = await _context.ToDoItemTag.Include(t => t.Tag)
                .Include(t => t.ToDoItem).ToListAsync();

            return _mapper.Map<IEnumerable<ToDoItemTagVo>>(toDoItemTagDaos);
        }

        public async Task Update(ToDoItemTagVo toDoItemTag)
        {
            ToDoItemTagDao toDoItemTagDao = _mapper.Map<ToDoItemTagDao>(toDoItemTag);

            _context.Update(toDoItemTagDao);
            await _context.SaveChangesAsync();
        }

        public bool ItemExits(int toDoItemId, int tagId)
        {
            return _context.ToDoItemTag.Any(e => e.ToDoItemId == toDoItemId && e.TagId == tagId);
        }
    }
}
