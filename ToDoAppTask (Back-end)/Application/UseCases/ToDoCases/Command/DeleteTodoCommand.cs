using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Application.Common.Models;
using MediatR;

namespace Application.UseCases.ToDoCases.Command
{
    public record DeleteTodoCommand(Guid Id) : IRequest<DataResponse<bool>>;
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, DataResponse<bool>>
    {
        private readonly IunitOfWork _unitOfWork;

        public DeleteTodoHandler(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DataResponse<bool>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _unitOfWork.ToDoRepository.GetByIdAsync(request.Id);
            if (todo == null)
                return new DataResponse<bool> { StatusCode = HttpStatusCode.NotFound, ResponseData = false };

            _unitOfWork.ToDoRepository.Delete(todo);
            await _unitOfWork.SaveChangesAsync();

            return new DataResponse<bool> { StatusCode = HttpStatusCode.OK, ResponseData = true };
        }
    }

}
