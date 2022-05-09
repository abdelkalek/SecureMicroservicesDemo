#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.ApiServices;
using Movies.Client.Models;

namespace Movies.Client.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieApiService _movieApiService;

        public MoviesController(IMovieApiService movieApiService)
        {
            _movieApiService = movieApiService;
        }

        public async Task<IActionResult> Index()
        {
            await LogTokenAndClaims();
            return View(await _movieApiService.GetMovies());
        }
        public async Task LogTokenAndClaims()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            Debug.WriteLine($"Identity token: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> OnlyAdmin()
        {
            var userInfo = await _movieApiService.GetUserInfo();
            return View(userInfo);
        }
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var movie = await _movieApiService.GetMovies()
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return View(movie);
            return View();
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Movie movie)
        {
            //if (ModelState.IsValid)
            //{
            //    movie.Id = Guid.NewGuid();
            //    _movieApiService.Add(movie);
            //    await _movieApiService.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(movie);
            return View();
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var movie = await _movieApiService.Movie.FindAsync(id);
            //if (movie == null)
            //{
            //    return NotFound();
            //}
            //return View(movie);
            return View();

        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Movie movie)
        {
            //if (id != movie.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _movieApiService.Update(movie);
            //        await _movieApiService.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!MovieExists(movie.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(movie);
            return View();
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var movie = await _movieApiService.Movie
            //        .FirstOrDefaultAsync(m => m.Id == id);
            //    if (movie == null)
            //    {
            //        return NotFound();
            //    }
            //    return View(movie);

            return View();
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            return View();

            //var movie = await _movieApiService.Movie.FindAsync(id);
            //_movieApiService.Movie.Remove(movie);
            //await _movieApiService.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(Guid id)
        {
            return true;

          //  return _movieApiService.Movie.Any(e => e.Id == id);
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
