﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheGymProject.DTO;
using TheGymProject.InterfacesService;

namespace TheGymProject.Service
{
    public class AlumnoService : IAlumnoService
    {
        private readonly GimnasioDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAlumnoPlanService _alumnoPlanService;
        public AlumnoService(GimnasioDbContext context, IMapper mapper, IAlumnoPlanService alumnoPlanService)
        {
            _context = context;
            _mapper = mapper;
            _alumnoPlanService = alumnoPlanService;
        }

        public async Task<IEnumerable<AlumnoDto>> GetAlumnos(int page = 1, int pageSize = 10)
        {
            var alumnos = await _context.Alumno
                .Include(a => a.AlumnoPlanes)
                .Include(ap => ap.Plan)
                .AsSplitQuery()
                .Skip((page - 1) * pageSize)  
                .Take(pageSize)               
                .ToListAsync();

            return _mapper.Map<IEnumerable<AlumnoDto>>(alumnos);
        }

        public async Task<bool> CreateAlumno(AlumnoDto alumnoDto)
        {
            var alumno = _mapper.Map<Alumno>(alumnoDto);
            alumno.FechaRegistro = DateTime.Now;
            _context.Alumno.Add(alumno);
            await _context.SaveChangesAsync();

            var alumnoPlan = new AlumnoPlan
            {
                AlumnoId = alumno.AlumnoId,
                PlanId = alumno.PlanId,
                FHInicio = DateTime.Now,
                FHVencimiento = DateTime.Now.AddMonths(1)
            };

            _context.AlumnoPlan.Add(alumnoPlan);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAlumno(int dni, AlumnoDto alumnoDto)
        {
            var alumno = await _context.Alumno.FirstOrDefaultAsync(a => a.DNI == dni);
            if (alumno == null) return false;

            alumno.Nombre = alumnoDto.Nombre;
            alumno.Apellido = alumnoDto.Apellido;
            alumno.Domicilio = alumnoDto.Domicilio;
            alumno.Telefono = alumnoDto.Telefono;
            alumno.TelefonoEmergencia = alumnoDto.TelefonoEmergencia;
            alumno.NumeroPlan = alumnoDto.NumeroPlan;

            alumno.PlanId = alumnoDto.PlanId;



            await _alumnoPlanService.ActualizarPlanActivo(dni, alumnoDto.PlanId);

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAlumno(int dni)
        {
            var alumno = await _context.Alumno.FirstOrDefaultAsync(a => a.DNI == dni);
            if (alumno == null) return false;

            _context.Alumno.Remove(alumno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AlumnoDto?> ObtenerAlumnoPorDni(int dni)
        {
            var alumno = await _context.Alumno
                .Include(a => a.AlumnoPlanes)
                .Include(ap => ap.Plan)
                .FirstOrDefaultAsync(a => a.DNI == dni);


            var planId = alumno.PlanId;

            var alumnoDto = _mapper.Map<AlumnoDto>(alumno);

            return alumnoDto;
        }

    }
}
