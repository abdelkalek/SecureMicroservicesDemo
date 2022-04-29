using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.Models;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class MoviesController : ControllerBase
    {
		private readonly MoviesContext _context;

		public MoviesController(MoviesContext context)
		{
			_context = context;
		}

		// GET: api/Movies
		[HttpGet(Name = nameof(GetMovies))]
		public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
		{
			return await _context.Movies.ToListAsync();
		}

		// GET: api/Movies/5
		[HttpGet("{id}", Name = nameof(GetMovie))]
		public async Task<ActionResult<Movie>> GetMovie(Guid id)
		{
			var movie = await _context.Movies.FindAsync(id);

			if (movie == null)
			{
				return NotFound();
			}

			return movie;
		}

		// PUT: api/Movies/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}", Name = nameof(PutMovie))]
		public async Task<IActionResult> PutMovie(Guid id, Movie movie)
		{
			if (id != movie.Id)
			{
				return BadRequest();
			}

			_context.Entry(movie).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!MovieExists(id))
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

		// POST: api/Movies
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost(Name = nameof(PostMovie))]
		public async Task<ActionResult<Movie>> PostMovie(Movie movie)
		{
			_context.Movies.Add(movie);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
		}

		// DELETE: api/Movies/5
		[HttpDelete("{id}", Name = nameof(DeleteMovie))]
		public async Task<ActionResult<Movie>> DeleteMovie(Guid id)
		{
			var movie = await _context.Movies.FindAsync(id);
			if (movie == null)
			{
				return NotFound();
			}

			_context.Movies.Remove(movie);
			await _context.SaveChangesAsync();

			return movie;
		}

		private bool MovieExists(Guid id)
		{
			return _context.Movies.Any(e => e.Id == id);
		}
	}
}