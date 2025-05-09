using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGI.Morn.Application.Common.Interfaces;
using AGI.Morn.Application.Common.Models;
using Application.UseCases.ToDoCases.Dto;
using AutoMapper;
using MediatR;

namespace Application.UseCases.ToDoCases.Queries
{
    public record GetAllTodosQuery : IRequest<DataResponse<IEnumerable<TodoDto>>>;

    public class GetAllTodosHandler : IRequestHandler<GetAllTodosQuery, DataResponse<IEnumerable<TodoDto>>>
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTodosHandler(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<IEnumerable<TodoDto>>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _unitOfWork.ToDoRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<TodoDto>>(todos);
            return new DataResponse<IEnumerable<TodoDto>>
            {
                StatusCode = HttpStatusCode.OK,
                ResponseData = result
            };
        }
    }


}
