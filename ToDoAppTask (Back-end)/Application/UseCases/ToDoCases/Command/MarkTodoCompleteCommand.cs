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
    public record MarkTodoCompleteCommand(Guid Id) : IRequest<DataResponse<bool>>;

    public class MarkTodoCompleteHandler : IRequestHandler<MarkTodoCompleteCommand, DataResponse<bool>>
    {
        private readonly IunitOfWork _unitOfWork;

        public MarkTodoCompleteHandler(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataResponse<bool>> Handle(MarkTodoCompleteCommand request, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.ToDoRepository.GetByIdAsync(request.Id);
            if (todo == null)
            {
                return new DataResponse<bool>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ResponseMessage = "Todo not found",
                    ResponseData = false
                };
            }

            todo.Status = TodoStatus.Completed;
            todo.LastModifiedDate = DateTime.UtcNow;

            _unitOfWork.ToDoRepository.Update(todo);
            await _unitOfWork.SaveChangesAsync();

            return new DataResponse<bool>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseData = true
            };
        }
    }


}
