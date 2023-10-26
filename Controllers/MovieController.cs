using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WEBAPI_ASP_NET_CORE.Models;
using Microsoft.EntityFrameworkCore;

namespace WEBAPI_ASP_NET_CORE.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DataContext _context;

        public MovieController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_context.movie == null)
            {
                return NotFound();
            }
            return await _context.movie.ToListAsync();

        }


        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie mov){
          
            var mvv= new Movie{
                Title=mov.Title,
                Genre=mov.Genre,
                ReleaseDate=mov.ReleaseDate
            };
          
            _context.movie.Add(mvv);
            await _context.SaveChangesAsync();
        //    await _context.movie.ToListAsync();
            //  return NoContent();
            return CreatedAtAction(nameof (GetMovie), new {id = mov.Id},mov);
            //   return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> PutMovie(int id , Movie mov){
            if(id != mov.Id){
                return BadRequest();
            }
            _context.Entry(mov).State=EntityState.Modified;
            try{
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException){
                if(!MovieExists(id)){
                    return NotFound();
                }else{
                    throw;
                }
            }
            return NoContent();
        }


        private bool MovieExists(long id){
            return (_context.movie?.Any(x=> x.Id==id)).GetValueOrDefault();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_context.movie == null)
            {
                return NotFound();
            }
            var moviex= await _context.movie.FindAsync(id);
            if (moviex == null)
            {
                return NotFound();
            }
            return moviex;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DelMovie(int id){
           var moviex= await _context.movie.FindAsync(id);
           if(moviex== null){
             return NotFound();
           }
           _context.movie.Remove(moviex);
           await _context.SaveChangesAsync();
           return NoContent();
        }

    }
}
