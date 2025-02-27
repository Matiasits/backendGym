//using Xunit;
//using Moq;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TheGymProject.Services;
//using TheGymProject.DTO;;
//using TheGymProject.InterfacesService;
//using System.Linq;

//namespace TheGymProject.Tests.ServicesTests
//{
//    public class AlumnoServiceTests
//    {
//        private readonly Mock<GimnasioDbContext> _mockContext;
//        private readonly Mock<IMapper> _mockMapper;
//        private readonly AlumnoService _alumnoService;

//        public AlumnoServiceTests()
//        {
//            _mockContext = new Mock<GimnasioDbContext>();
//            _mockMapper = new Mock<IMapper>();
//            _alumnoService = new AlumnoService(_mockContext.Object, _mockMapper.Object);
//        }

//        [Fact]
//        public async Task GetAlumnos_ReturnsAlumnosList()
//        {
//            var alumnos = new List<Alumno> { new Alumno { AlumnoId = 1, Nombre = "Juan" } };
//            var alumnosDto = new List<AlumnoDto> { new AlumnoDto { AlumnoId = 1, Nombre = "Juan" } };

//            var dbSetMock = new Mock<DbSet<Alumno>>();
//            dbSetMock.As<IQueryable<Alumno>>().Setup(m => m.Provider).Returns(alumnos.AsQueryable().Provider);
//            dbSetMock.As<IQueryable<Alumno>>().Setup(m => m.Expression).Returns(alumnos.AsQueryable().Expression);
//            dbSetMock.As<IQueryable<Alumno>>().Setup(m => m.ElementType).Returns(alumnos.AsQueryable().ElementType);
//            dbSetMock.As<IQueryable<Alumno>>().Setup(m => m.GetEnumerator()).Returns(alumnos.AsQueryable().GetEnumerator());

//            _mockContext.Setup(c => c.Alumno).Returns(dbSetMock.Object);
//            _mockMapper.Setup(m => m.Map<IEnumerable<AlumnoDto>>(It.IsAny<IEnumerable<Alumno>>())).Returns(alumnosDto);

//            var result = await _alumnoService.GetAlumnos();

//            Assert.NotNull(result);
//            Assert.Single(result);
//        }
//    }

//    public class PlanServiceTests
//    {
//        private readonly Mock<GimnasioDbContext> _mockContext;
//        private readonly Mock<IMapper> _mockMapper;
//        private readonly PlanService _planService;

//        public PlanServiceTests()
//        {
//            _mockContext = new Mock<GimnasioDbContext>();
//            _mockMapper = new Mock<IMapper>();
//            _planService = new PlanService(_mockContext.Object, _mockMapper.Object);
//        }

//        [Fact]
//        public async Task GetPlanes_ReturnsPlanList()
//        {
//            var planes = new List<Plan> { new Plan { PlanId = 1, Nombre = "Mensual" } };
//            var planesDto = new List<PlanDto> { new PlanDto { PlanId = 1, Nombre = "Mensual" } };

//            var dbSetMock = new Mock<DbSet<Plan>>();
//            dbSetMock.As<IQueryable<Plan>>().Setup(m => m.Provider).Returns(planes.AsQueryable().Provider);
//            dbSetMock.As<IQueryable<Plan>>().Setup(m => m.Expression).Returns(planes.AsQueryable().Expression);
//            dbSetMock.As<IQueryable<Plan>>().Setup(m => m.ElementType).Returns(planes.AsQueryable().ElementType);
//            dbSetMock.As<IQueryable<Plan>>().Setup(m => m.GetEnumerator()).Returns(planes.AsQueryable().GetEnumerator());

//            _mockContext.Setup(c => c.Planes).Returns(dbSetMock.Object);
//            _mockMapper.Setup(m => m.Map<IEnumerable<PlanDto>>(It.IsAny<IEnumerable<Plan>>())).Returns(planesDto);

//            var result = await _planService.GetPlanes();

//            Assert.NotNull(result);
//            Assert.Single(result);
//        }
//    }

//    public class ProfesorServiceTests
//    {
//        private readonly Mock<GimnasioDbContext> _mockContext;
//        private readonly Mock<IMapper> _mockMapper;
//        private readonly ProfesorService _profesorService;

//        public ProfesorServiceTests()
//        {
//            _mockContext = new Mock<GimnasioDbContext>();
//            _mockMapper = new Mock<IMapper>();
//            _profesorService = new ProfesorService(_mockContext.Object, _mockMapper.Object);
//        }

//        [Fact]
//        public async Task GetProfesores_ReturnsProfesorList()
//        {
//            var profesores = new List<Profesor> { new Profesor { ProfesorId = 1, Nombre = "Carlos" } };
//            var profesoresDto = new List<ProfesorDto> { new ProfesorDto { ProfesorId = 1, Nombre = "Carlos" } };

//            var dbSetMock = new Mock<DbSet<Profesor>>();
//            dbSetMock.As<IQueryable<Profesor>>().Setup(m => m.Provider).Returns(profesores.AsQueryable().Provider);
//            dbSetMock.As<IQueryable<Profesor>>().Setup(m => m.Expression).Returns(profesores.AsQueryable().Expression);
//            dbSetMock.As<IQueryable<Profesor>>().Setup(m => m.ElementType).Returns(profesores.AsQueryable().ElementType);
//            dbSetMock.As<IQueryable<Profesor>>().Setup(m => m.GetEnumerator()).Returns(profesores.AsQueryable().GetEnumerator());

//            _mockContext.Setup(c => c.Profesor).Returns(dbSetMock.Object);
//            _mockMapper.Setup(m => m.Map<IEnumerable<ProfesorDto>>(It.IsAny<IEnumerable<Profesor>>())).Returns(profesoresDto);

//            var result = await _profesorService.GetProfesores();

//            Assert.NotNull(result);
//            Assert.Single(result);
//        }
//    }

//    public class AsistenciaServiceTests
//    {
//        private readonly Mock<GimnasioDbContext> _mockContext;
//        private readonly Mock<IMapper> _mockMapper;
//        private readonly AsistenciaService _asistenciaService;

//        public AsistenciaServiceTests()
//        {
//            _mockContext = new Mock<GimnasioDbContext>();
//            _mockMapper = new Mock<IMapper>();
//            _asistenciaService = new AsistenciaService(_mockContext.Object, _mockMapper.Object);
//        }

//        [Fact]
//        public async Task RegistrarAsistenciaAlumno_ReturnsTrue()
//        {
//            var asistenciaDto = new AsistenciaDto { DNIAlumno = "12345678" };
//            var alumno = new Alumno { AlumnoId = 1, DNI = "12345678" };

//            _mockContext.Setup(c => c.Alumno.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Alumno, bool>>>(), default)).ReturnsAsync(alumno);
//            _mockMapper.Setup(m => m.Map<Asistencia>(It.IsAny<AsistenciaDto>())).Returns(new Asistencia());
//            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

//            var result = await _asistenciaService.RegistrarAsistenciaAlumno(asistenciaDto);

//            Assert.True(result);
//        }
//    }
//}
