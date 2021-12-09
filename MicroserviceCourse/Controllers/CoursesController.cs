using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroserviceCourse.Models;

namespace MicroserviceCourse.Controllers
{
    /// <summary>
    /// Контроллер сервиса курсов.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseContext _context;

        public CoursesController(CourseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Асинхронно возвращает все курсы.
        /// </summary>
        /// <returns> Список <see cref="List{T}"/>, где T является <see cref="Course"/>. </returns>
        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        /// <summary>
        /// Асинхронно возвращает объект <see cref="Course"/> по id.
        /// </summary>
        /// <param name="id"> Id группы. </param>
        /// <returns> Объект типа <see cref="Course"/>. </returns>
        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(long id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        /// <summary>
        /// Изменяет курс в базе данных по его id.
        /// </summary>
        /// <param name="id"> Id курса. </param>
        /// <param name="course"> Объект <see cref="Course"/>, составленный из Body запроса. </param>
        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(long id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Добавляет новый курс в базу данных, если полученная модель валидна.
        /// </summary>
        /// <param name="course"> Объект <see cref="Course"/>, составленный из Body запроса. </param>
        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        /// <summary>
        /// Удаляет курс по его id.
        /// </summary>
        /// <param name="id"> Id курса. </param>
        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(long id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
