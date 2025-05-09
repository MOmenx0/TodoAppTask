
using Application.UseCases.ToDoCases.Command;
using Application.UseCases.ToDoCases.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGI.Morn.Application.Common.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {

            CreateMap<CreateTodoCommand, Todo>();
            CreateMap<Todo, TodoDto>().ReverseMap();


        }
    }
}
