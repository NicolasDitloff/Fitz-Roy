using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fitz_Roy.Data;
using Fitz_Roy.Models;
using Xunit;
using TodoContext = Fitz_Roy.Models.TodoContext;

public class TareasControllerTests
{
    private TodoContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListTest")
            .Options;

        var context = new TodoContext(options);
        context.Database.EnsureCreated();

        return context;
    }

    [Fact]
    public async Task GetTareas_ReturnsAllTareas()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        context.Tarea.AddRange(new TareaItem { Title = "Tarea 1" }, new TareaItem { Title = "Tarea 2" });
        await context.SaveChangesAsync();
        var controller = new TareasController(context);

        // Act
        var result = await controller.GetTareas();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<TareaItem>>>(result);
        var tareas = Assert.IsType<List<TareaItem>>(actionResult.Value);
        Assert.Equal(2, tareas.Count);
    }

    [Fact]
    public async Task CreateTarea_ValidTarea_ReturnsCreatedTarea()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var controller = new TareasController(context);
        var newTarea = new TareaItem { Title = "New Tarea", Status = "pendiente", Description = "Description" };

        // Act
        var result = await controller.CreateTarea(newTarea);

        // Assert
        var actionResult = Assert.IsType<ActionResult<TareaItem>>(result);
        var createdTarea = Assert.IsType<TareaItem>(actionResult.Value);
        Assert.Equal("New Tarea", createdTarea.Title);
    }

    [Fact]
    public async Task UpdateTarea_ValidTarea_ReturnsNoContent()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var controller = new TareasController(context);
        var existingTarea = new TareaItem { Title = "Old Tarea", Status = "pendiente" };
        context.Tarea.Add(existingTarea);
        await context.SaveChangesAsync();

        existingTarea.Title = "Updated Tarea";

        // Act
        var result = await controller.UpdateTarea(existingTarea.Id, existingTarea);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteTarea_ValidTarea_ReturnsNoContent()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var controller = new TareasController(context);
        var tareaToDelete = new TareaItem { Title = "Tarea to Delete" };
        context.Tarea.Add(tareaToDelete);
        await context.SaveChangesAsync();

        // Act
        var result = await controller.DeleteTarea(tareaToDelete.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
