using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Application.Common.Models;
using Application.UseCases.ToDoCases.Dto;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.ToDoCases.Queries
{
    public record GetTodosQuery(TodoStatus? Status) : IRequest<DataResponse<IEnumerable<TodoDto>>>;

    public class GetTodosHandler : IRequestHandler<GetTodosQuery, DataResponse<IEnumerable<TodoDto>>>
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTodosHandler(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<IEnumerable<TodoDto>>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _unitOfWork.ToDoRepository.GetAllAsync(request.Status);
            var result = _mapper.Map<IEnumerable<TodoDto>>(todos);
            return new DataResponse<IEnumerable<TodoDto>> {ResponseData = result, StatusCode = HttpStatusCode.OK };
        }
    }


}
