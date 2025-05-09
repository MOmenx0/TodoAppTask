using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.ToDoCases.Command
{
    public record UpdateTodoCommand : IRequest<DataResponse<bool>>
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public TodoStatus Status { get; init; }
        public TodoPriority Priority { get; init; }
        public DateTime? DueDate { get; init; }
    }

    public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, DataResponse<bool>>
    {
        private readonly IunitOfWork _unitOfWork;

        public UpdateTodoHandler(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataResponse<bool>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.ToDoRepository.GetByIdAsync(request.Id);
            if (todo == null)
                return new DataResponse<bool> { StatusCode = HttpStatusCode.NotFound, ResponseData = false };

            todo.Title = request.Title;
            todo.Description = request.Description;
            todo.Status = request.Status;
            todo.Priority = request.Priority;
            todo.DueDate = request.DueDate;
            todo.LastModifiedDate = DateTime.UtcNow;

            _unitOfWork.ToDoRepository.Update(todo);
            await _unitOfWork.SaveChangesAsync();

            return new DataResponse<bool> { StatusCode = HttpStatusCode.OK, ResponseData = true };
        }
    }


}
