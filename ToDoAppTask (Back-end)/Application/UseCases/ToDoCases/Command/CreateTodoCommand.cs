using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.ToDoCases.Command
{
    public record CreateTodoCommand : IRequest<DataResponse<Guid>>
    {
        public string Title { get; init; }
        public string? Description { get; init; }
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public TodoPriority Priority { get; init; }
        public DateTime? DueDate { get; init; }
    }

    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, DataResponse<Guid>>
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTodoHandler(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<Guid>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = _mapper.Map<Todo>(request);
            todo.CreatedDate = DateTime.UtcNow;
            todo.LastModifiedDate = DateTime.UtcNow;

            await _unitOfWork.ToDoRepository.AddAsync(todo);
            await _unitOfWork.SaveChangesAsync();

            return new DataResponse<Guid>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseData = todo.Id
            };
        }
    }

}
