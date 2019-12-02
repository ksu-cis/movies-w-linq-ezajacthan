using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Movie> Movies;

        [BindProperty]
        public string search { get; set; }

        [BindProperty]
        public List<string> mpaa { get; set; } = new List<string>();

        [BindProperty]
        public float? minIMDB { get; set; }

        [BindProperty]
        public float? maxIMDB { get; set; }

        [BindProperty]
        public string order { get; set; }

        public void OnGet()
        {
            Movies = MovieDatabase.All.OrderBy(movie => movie.Title);
        }

        public void OnPost()
        {
            Movies = MovieDatabase.All;

            if (search != null)
            {
                //Movies = MovieDatabase.Search(Movies, search);
                Movies = Movies.Where(movie => movie.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if(mpaa.Count != 0)
            {
                //Movies = MovieDatabase.FilterByMPAA(Movies, mpaa);
                Movies = Movies.Where(movie => mpaa.Contains(movie.MPAA_Rating));
            }

            if(minIMDB != null)
            {
                //Movies = MovieDatabase.FilterByMinIMDB(Movies, (float)minIMDB);
                Movies = Movies.Where(movie => movie.IMDB_Rating != null && minIMDB <= movie.IMDB_Rating);
            }

            if (maxIMDB != null)
            {
                //Movies = MovieDatabase.FilterByMinIMDB(Movies, (float)minIMDB);
                Movies = Movies.Where(movie => movie.IMDB_Rating != null && maxIMDB >= movie.IMDB_Rating);
            }

            if(order != null)
            {
                switch(order)
                {
                    case "title":
                        Movies = MovieDatabase.All.OrderBy(movie => movie.Title);
                        break;
                    case "director":
                        Movies = MovieDatabase.All.OrderBy(movie => movie.Director);
                        break;
                    case "year":
                        Movies = MovieDatabase.All.OrderBy(movie => movie.Release_Year);
                        break;
                    case "imdb":
                        Movies = MovieDatabase.All.OrderBy(movie => movie.IMDB_Rating);
                        break;
                }
            }
        }
    }
}
