//using System;
//using Microsoft.EntityFrameworkCore;

//public static class DbContextMock
//{
//    public static GimnasioDbContext GetDbContext()
//    {
//        var options = new DbContextOptionsBuilder<GimnasioDbContext>()
//            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Base de datos en memoria
//            .Options;

//        var context = new GimnasioDbContext(options);
//        context.Database.EnsureCreated(); // Asegura que la BD en memoria está lista

//        return context;
//    }
//}
