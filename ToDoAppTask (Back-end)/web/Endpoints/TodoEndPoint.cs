using AGI.Morn.Application.Common.Models;
using Application.UseCases.ToDoCases.Command;
using Application.UseCases.ToDoCases.Dto;
using Application.UseCases.ToDoCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Morn_Agi.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Morn_Agi.Endpoints
{
    public class TodoEndPoint : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            var group = app.MapGroup(nameof(TodoEndPoint));

            // Register endpoints within the group
            group.MapPost("/CreateTodo", CreateTodo).WithName(nameof(CreateTodo));
            group.MapGet("/GetTodos", GetTodos).WithName(nameof(GetTodos));
            group.MapGet("/GetAllTodos", GetAllTodos).WithName(nameof(GetAllTodos));
            group.MapPut("/Update/{id}", Update).WithName(nameof(Update));
            group.MapDelete("/Delete/{id}", Delete).WithName(nameof(Delete));
            group.MapPut("/complete/{id}", MarkAsComplete).WithName("MarkAsComplete");

        }

        public async Task<IResult> CreateTodo([FromBody] CreateTodoCommand command, ISender sender)
        {
            if (command == null)
            {
                return Results.BadRequest("Command cannot be null");
            }

            try
            {
                var result = await sender.Send(command);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error creating todo: {ex.Message}");
                return Results.Problem($"Error creating todo: {ex.Message}");
            }
        }

        public async Task<DataResponse<IEnumerable<TodoDto>>> GetAllTodos(ISender sender)
        {
            return await sender.Send(new GetAllTodosQuery());
        }

        public async Task<IResult> GetTodos([AsParameters] GetTodosQuery query, ISender sender)
        {
            var result = await sender.Send(query);
            return Results.Ok(result);
        }

        public async Task<DataResponse<bool>> Update(Guid id, [FromBody] UpdateTodoCommand command, ISender sender)
        {
            var updatedCommand = command with { Id = id };
            return await sender.Send(updatedCommand);
        }

        public async Task<DataResponse<bool>> Delete( Guid id, ISender sender)
            => await sender.Send(new DeleteTodoCommand(id));

        public async Task<DataResponse<bool>> MarkAsComplete(Guid id, ISender sender)
        {
            return await sender.Send(new MarkTodoCompleteCommand(id));
        }

    }
}
