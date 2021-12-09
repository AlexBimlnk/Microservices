using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroserviceStudent.Models;
using System.Net.Http;
using System.Text.Json;

namespace MicroserviceStudent.Controllers
{
    /// <summary>
    /// Контроллер сервиса студентов.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Асинхронно возвращает всех студентов.
        /// </summary>
        /// <returns> Список <see cref="List{T}"/>, где T является <see cref="Student"/>. </returns>
        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        /// <summary>
        /// Асинхронно возвращает объект <see cref="Student"/> по id.
        /// </summary>
        /// <param name="id"> Id студента. </param>
        /// <returns> Объект типа <see cref="Student"/>. </returns>
        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        //[HttpGet("course/{studentId}")]
        //public async Task<string> GetCourse(long studentId)
        //{
        //    var actionResult = await GetStudent(studentId);
        //    var student = actionResult.Value;
        //    HttpClientHandler clientHandler = new HttpClientHandler();
        //    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        //    using (HttpClient client = new HttpClient(clientHandler))
        //    {
        //        HttpResponseMessage response = await client.GetAsync($"https://localhost:44376/api/courses/{student.CourseId}");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            object course = await JsonSerializer.DeserializeAsync<object>(await response.Content.ReadAsStreamAsync());
        //            return course.ToString();
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// Изменяет студента в базе данных по его id.
        /// </summary>
        /// <param name="id"> Id студента. </param>
        /// <param name="student"> Объект <see cref="Student"/>, составленный из Body запроса. </param>
        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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
        /// Добавляет нового студента в базу данных, если полученная модель валидна.
        /// </summary>
        /// <param name="student"> Объект <see cref="Student"/>, составленный из Body запроса. </param>
        /// <returns> <see cref="Index"/>, если объект успешно добавлен. </returns>
        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        /// <summary>
        /// Удаляет студента по его id.
        /// </summary>
        /// <param name="id"> Id студента. </param>
        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(long id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
