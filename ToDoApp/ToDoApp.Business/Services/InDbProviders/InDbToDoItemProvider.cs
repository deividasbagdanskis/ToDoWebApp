﻿using Microsoft.EntityFrameworkCore;
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
            
            ValidateThatThereIsOnlyASingleWipStatusWithPriority1(toDoItem.Status, toDoItem.Priority);
            
            ValidateThatThereIsOnlyThreeToDoItemsWithWipStatusPriority2();
            
            ValidateThatToDoItemHasDeadlineDateAndItsNotLessThanAWeekInFutureWithPriority1(toDoItem.CreationDate, 
                toDoItem.DeadlineDate, toDoItem.Priority);

            ValidateThatToDoItemHasDeadlineDateAndItsNotLessThan2DaysInFutureWithPriority2(toDoItem.CreationDate,
                toDoItem.DeadlineDate, toDoItem.Priority);

            ValidateThatToDoItemHasDescriptionAndItHasAtLeast140CharsWithPriority1(toDoItem.Description, 
                toDoItem.Priority);

            ToDoItemDao toDoItemDao = _mapper.Map<ToDoItemDao>(toDoItem);
            _context.Add(toDoItemDao);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id, string userId)
        {
            var toDoItem = await _context.ToDoItem.Where(t => t.Id == id && t.UserId == userId).FirstOrDefaultAsync();
            _context.ToDoItem.Remove(toDoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<ToDoItemVo> Get(int? id, string userId)
        {
            var foundToDoItem = await _context.ToDoItem.Where(t => t.Id == id && t.UserId == userId)
                .FirstOrDefaultAsync();

            try
            {
                foundToDoItem.Category = _context.Category.Single(c => c.Id == foundToDoItem.CategoryId);
            }
            catch (InvalidOperationException)
            {
                
            }

            return _mapper.Map<ToDoItemVo>(foundToDoItem);
        }

        public async Task<IEnumerable<ToDoItemVo>> GetAll(string userId)
        {
            IEnumerable<ToDoItemDao> toDoItemDaos = await _context.ToDoItem.Include(t => t.Category)
                .Where(td => td.UserId == userId).ToListAsync();

            return _mapper.Map<IEnumerable<ToDoItemVo>>(toDoItemDaos);
        }

        public async Task Update(ToDoItemVo toDoItem)
        {
            ValidatePriority(toDoItem.Priority);

            ToDoItemDao toDoItemForCreationDate = await _context.ToDoItem.FindAsync(toDoItem.Id);
            
            _context.Entry(toDoItemForCreationDate).State = EntityState.Detached;
            
            DateTime creationDate = toDoItemForCreationDate.CreationDate;

            ValidateDeadlineDate(creationDate, toDoItem.DeadlineDate);

            ValidateThatThereIsOnlyASingleWipStatusWithPriority1(toDoItem.Status, toDoItem.Priority);
            
            ValidateThatThereIsOnlyThreeToDoItemsWithWipStatusPriority2();

            ValidateThatToDoItemHasDeadlineDateAndItsNotLessThanAWeekInFutureWithPriority1(toDoItem.CreationDate,
                toDoItem.DeadlineDate, toDoItem.Priority);

            ValidateThatToDoItemHasDeadlineDateAndItsNotLessThan2DaysInFutureWithPriority2(toDoItem.CreationDate,
                toDoItem.DeadlineDate, toDoItem.Priority);

            ValidateThatToDoItemHasDescriptionAndItHasAtLeast140CharsWithPriority1(toDoItem.Description,
                toDoItem.Priority);

            ToDoItemDao toDoItemDao = _mapper.Map<ToDoItemDao>(toDoItem);

            _context.Update(toDoItemDao);
            _context.Entry(toDoItemDao).Property("CreationDate").IsModified = false;
            _context.Entry(toDoItemDao).Property("UserId").IsModified = false;

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

        private void ValidateThatThereIsOnlyASingleWipStatusWithPriority1(StatusEnum status, int priority)
        {
            if (status == StatusEnum.Wip && priority == 1)
            {
                int toDoItemsWithWipStatusAndPriority1 = _context.ToDoItem
                    .Where(td => td.Status == StatusEnum.Wip && td.Priority == 1).Count();

                if (toDoItemsWithWipStatusAndPriority1 >= 1)
                {
                    throw new ToDoItemException("There can only be a single ToDo item with Wip status and priority of 1");
                }
            }
        }

        private void ValidateThatThereIsOnlyThreeToDoItemsWithWipStatusPriority2()
        {
            int toDoItemsWithWipStatusAndPriority2 = _context.ToDoItem
                .Where(td => td.Status == StatusEnum.Wip && td.Priority == 2).Count();

            if (toDoItemsWithWipStatusAndPriority2 > 3)
            {
                throw new ToDoItemException("There can only be three ToDo item with Wip status and priority of 2");
            }
        }

        private void ValidateThatToDoItemHasDeadlineDateAndItsNotLessThanAWeekInFutureWithPriority1(DateTime creationDate, 
            DateTime? deadlineDate, int priority)
        {
            if (priority == 1)
            {
                if (deadlineDate != null)
                {
                    DateTime castedDeadlineDate2 = (DateTime)deadlineDate;

                    if ((castedDeadlineDate2.Date - creationDate.Date).TotalDays > 7)
                    {
                        throw new ToDoItemException("Deadline date must not be less than a week in the future");
                    }
                }
                else
                {
                    throw new ToDoItemException("Deadline date is required");
                }
            }
        }

        private void ValidateThatToDoItemHasDeadlineDateAndItsNotLessThan2DaysInFutureWithPriority2(DateTime creationDate,
            DateTime? deadlineDate, int priority)
        {
            if (priority == 2)
            {
                if (deadlineDate != null)
                {
                    DateTime castedDeadlineDate2 = (DateTime)deadlineDate;

                    if ((castedDeadlineDate2.Date - creationDate.Date).TotalDays > 2)
                    {
                        throw new ToDoItemException("Deadline date must not be less than 2 days in the future");
                    }
                }
                else
                {
                    throw new ToDoItemException("Deadline date is required");
                }
            }
        }

        private void ValidateThatToDoItemHasDescriptionAndItHasAtLeast140CharsWithPriority1(string description, 
            int priority)
        {
            if (priority == 1)
            {
                if (!string.IsNullOrEmpty(description))
                {
                    if (description.Length < 140)
                    {
                        throw new ToDoItemException("Description must have at least 140 characters");
                    }
                }
                else
                {
                    throw new ToDoItemException("Description is required");
                }
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
