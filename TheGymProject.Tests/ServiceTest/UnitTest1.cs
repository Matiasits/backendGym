//using Xunit;
//using TheGymProject.Service; // Asegúrate de importar tus servicios
//using Moq;
//using AutoMapper;
//using TheGymProject.Data;

//public class ProfesorServiceTests
//{
//    private readonly GimnasioDbContext _dbContext;
//    private readonly Mock<IMapper> _mapperMock;
//    private readonly ProfesorService _profesorService;

//    public ProfesorServiceTests()
//    {
//        _dbContext = DbContextMock.GetDbContext(); // Usa el mock del DbContext
//        _mapperMock = new Mock<IMapper>();
//        _profesorService = new ProfesorService(_dbContext, _mapperMock.Object);
//    }

//    [Fact]
//    public void ObtenerProfesores_DeberiaRetornarListaVacia_CuandoNoHayProfesores()
//    {
//        // Act
//        var resultado = _profesorService.ObtenerProfesores();

//        // Assert
//        Assert.Empty(resultado);
//    }
//}