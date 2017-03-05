using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Matriculas.Queries.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace Matriculas.Queries.Persistence.Repositories
{
	public class MatriculasRepository : IRepository<Matricula>, IMatriculasRepository
	{
		private MatriculasContext _context;

        public MatriculasRepository(MatriculasContext context)
        {
            _context = context;
        }

        public void Add(Matricula entity)
        {
            entity.Seccion = ChooseSeccion(entity.Grado);
            entity.AnioAcademico = new AniosAcademicosRepository(_context).GetAnioAcademico(DateTime.Now.Year);
            entity.Fecha = DateTime.Now;

            if (entity.Alumno.Id == 0)
                _context.Alumnos.Add(entity.Alumno);
            else
            {
                _context.Entry(entity.Alumno).State = EntityState.Modified;
                _context.Entry(entity.Alumno.Apoderado).State = EntityState.Modified;
            }

            _context.Entry(entity).State = EntityState.Added;
        }

        public Seccion ChooseSeccion(Grado grado)
        {
            var secciones = new SeccionesRepository(_context).GetAll()
                .Where(t => t.Grado.Id == grado.Id)
                .ToList();

            if (secciones.Count > 0)
            {
                Random randomizer = new Random();
                return secciones.ElementAt(randomizer.Next(secciones.Count));
            }
            return null;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Matricula Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Matricula> GetAll()
        {
            return _context.Matriculas
                .Include(t => t.Alumno)
                .Include(t => t.Seccion)
                    .ThenInclude(t => t.Grado)
                        .ThenInclude(t => t.Nivel)
                .Where(t => t.AnioAcademico.FechaInicio.Value.Year == DateTime.Now.Year)
                .ToList();
        }

        public void Update(Matricula entity)
        {
            throw new NotImplementedException();
        }
    }
}
