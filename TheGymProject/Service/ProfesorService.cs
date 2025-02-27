using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGymProject.DTO;
using BCrypt.Net;

namespace TheGymProject.InterfacesService
{
    public class ProfesorService : IProfesorService
    {
        private readonly GimnasioDbContext _context;
        private readonly IMapper _mapper;

        public ProfesorService(GimnasioDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfesorDto>> GetProfesores()
        {
            var profesores = await _context.Profesor.ToListAsync();
            return _mapper.Map<IEnumerable<ProfesorDto>>(profesores);
        }

        public async Task<Profesor> CreateProfesor(ProfesorDto profesorDto)
        {
            var profesor = _mapper.Map<Profesor>(profesorDto);
            profesor.PasswordHash = BCrypt.Net.BCrypt.HashPassword(profesorDto.PasswordHash);

            _context.Profesor.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<bool> UpdateProfesor(int id, ProfesorDto profesorDto)
        {
            var profesor = await _context.Profesor.FindAsync(id);
            if (profesor == null) return false;

            _mapper.Map(profesorDto, profesor);

            if (!string.IsNullOrWhiteSpace(profesorDto.PasswordHash))
            {
                profesor.PasswordHash = BCrypt.Net.BCrypt.HashPassword(profesorDto.PasswordHash);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProfesor(int id)
        {
            var profesor = await _context.Profesor.FindAsync(id);
            if (profesor == null) return false;

            _context.Profesor.Remove(profesor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
