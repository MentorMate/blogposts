//-----------------------------------------------------------------------
// <copyright file="ToDosController.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Api.Data;
using MyApp.Api.Data.Models;

namespace MyApp.Api.Controllers
{
    /// <summary>ToDo enpoint.</summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ToDosController : Controller
    {
        /// <summary>Initializes a new instance of the <see cref="ToDosController"/> class.</summary>
        public ToDosController(ApplicationDbContext applicationDbContext)
        {
            AppDbContext = applicationDbContext;
        }

        /// <summary>Gets the database context.</summary>
        /// <value>The database context.</value>
        protected ApplicationDbContext AppDbContext { get; private set; }

        /// <summary>Return a list of todos.</summary>
        /// <example>GET /api/todos</example>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ToDo>))]
        public async Task<IActionResult> GetAsync() =>
            Ok(await AppDbContext.ToDos.AsNoTracking().ToListAsync());

        /// <summary>Create a new todo item.</summary>
        /// <example>POST /api/todos</example>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(201, Type = typeof(int))]
        public async Task<IActionResult> PostAsync([FromBody] ToDo todo)
        {
            if (ModelState.IsValid)
            {
                var entry = await AppDbContext.ToDos.AddAsync(todo);

                await AppDbContext.SaveChangesAsync();

                return Created($"/api/todos/{entry.Entity.Id}", entry.Entity);
            }

            return BadRequest(
                ModelState.Where(m => m.Value.Errors.Any())
                    .SelectMany(m => m.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .Distinct()
                    .ToArray());
        }

        /// <summary>Update an existing ToDo.</summary>
        /// <example>PUT /api/todos/1 { "status": 1 }</example>
        [HttpPut("{id:int:min(1)}")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(ToDo))]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ToDo todo)
        {
            if (todo == null)
            {
                return BadRequest(new[] { "Todo object is not provided." });
            }

            var currentTodo = await AppDbContext.ToDos.FindAsync(id);

            currentTodo.Status = todo.Status;

            await AppDbContext.SaveChangesAsync();

            return Ok(currentTodo);
        }

        /// <summary>Delete todo by id.</summary>
        /// <example>DELETE /api/todos/1</example>
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var todo = await AppDbContext.ToDos.FindAsync(id);

            AppDbContext.ToDos.Remove(todo);

            await AppDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
