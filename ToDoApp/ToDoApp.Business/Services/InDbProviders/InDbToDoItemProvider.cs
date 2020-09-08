using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToDoApp.Business.Models;
using ToDoApp.Commons.Exceptions;
using ToDoApp.Commons.Enums;

namespace ToDoApp.Business.Services.InDbProviders
{
    public class InDbToDoItemProvider : IAsyncDbDataProvider<ToDoItemVo>, IInDbProjectToDoItemProvider
    {
        private readonly SampleWebAppContext _context;
        private readonly IMapper _mapper;

        public InDbToDoItemProvider(SampleWebAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(ToDoItemVo toDoItem)
        {
            ValidateName(toDoItem.Name);
            ValidatePriority(toDoItem.Priority);
            ValidateDeadlineDate(toDoItem.CreationDate, toDoItem.DeadlineDate);
            ValidateThatThereIsOnlyASingleWipStatusWithPriority1();

            ToDoItemDao toDoItemDao = _mapper.Map<ToDoItemDao>(toDoItem);
            _context.Add(toDoItemDao);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var toDoItem = await _context.ToDoItem.FindAsync(id);
            _context.ToDoItem.Remove(toDoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<ToDoItemVo> Get(int? id)
        {
            var foundToDoItem = await _context.ToDoItem.FindAsync(id);

            try
            {
                foundToDoItem.Category = _context.Category.Single(c => c.Id == foundToDoItem.CategoryId);
            }
            catch (InvalidOperationException)
            {
                
            }

            return _mapper.Map<ToDoItemVo>(foundToDoItem);
        }

        public async Task<IEnumerable<ToDoItemVo>> GetAll()
        {
            IEnumerable<ToDoItemDao> toDoItemDaos = await _context.ToDoItem.Include(t => t.Category).ToListAsync();

            return _mapper.Map<IEnumerable<ToDoItemVo>>(toDoItemDaos);
        }

        public async Task Update(ToDoItemVo toDoItem)
        {
            ValidatePriority(toDoItem.Priority);
            ValidateDeadlineDate(toDoItem.CreationDate, toDoItem.DeadlineDate);
            ValidateThatThereIsOnlyASingleWipStatusWithPriority1();

            ToDoItemDao toDoItemDao = _mapper.Map<ToDoItemDao>(toDoItem);
            
            _context.Update(toDoItemDao);
            _context.Entry(toDoItemDao).Property("CreationDate").IsModified = false;
            
            await _context.SaveChangesAsync();
        }
        public bool ItemExits(int id)
        {
            return _context.ToDoItem.Any(e => e.Id == id);
        }

        public IEnumerable<ToDoItemVo> GetToDoItemsByProjectId(int projectId)
        {
            IEnumerable<ToDoItemDao> toDoItems = _context.ToDoItem.Where(td => td.ProjectId == projectId)
                .Include(td => td.Category).ToList();

            return _mapper.Map<IEnumerable<ToDoItemVo>>(toDoItems);
        }

        private void ValidateThatThereIsOnlyASingleWipStatusWithPriority1()
        {
            int toDoItemsWithWipStatusAndPriority1 = _context.ToDoItem
                .Where(td => td.Status == StatusEnum.Wip && td.Priority == 1).Count();

            if (toDoItemsWithWipStatusAndPriority1 > 0)
            {
                throw new ToDoItemException("There can only be a single ToDo item with Wip status and priority of 1");
            }
        }

        private void ValidatePriority(int priority)
        {
            if (priority < 1 || priority > 5)
            {
                throw new ToDoItemPriorityException(priority);
            }
        }

        private void ValidateDeadlineDate(DateTime creationDate, DateTime? deadlineDate)
        {
            if (deadlineDate != null)
            {
                if (DateTime.Compare(creationDate, (DateTime)deadlineDate) >= 0)
                {
                    throw new ToDoItemDeadlineDateException();
                }
            }
        }

        private void ValidateName(string name)
        {
            List<ToDoItemDao> toDoItemsWithTheSameName = _context.ToDoItem.Where(td => td.Name == name).ToList();

            if (toDoItemsWithTheSameName.Count > 0)
            {
                throw new ToDoItemUniqueNameException(name);
            }
        }
    }
}
