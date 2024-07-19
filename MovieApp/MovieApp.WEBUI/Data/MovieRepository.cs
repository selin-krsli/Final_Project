using MovieApp.WEBUI.Models;

namespace MovieApp.WEBUI.Data
{
    public static class MovieRepository
    {
        private static List<Movie> _movies = null;
        static MovieRepository()
        {
            _movies = new List<Movie>
            {
                new Movie{ MovieId=1, Name="Iphone 8", Price=6000, Description="iyi telefon", IsApproved=false, Image="batman-2-kara-sovalye.jpg", CategoryId=1},
                new Movie{ MovieId=2, Name="Iphone X", Price=1000, Description="Müko", IsApproved=true, Image="Cizgili-pijamali-cocuk.jpg", CategoryId=2},
                new Movie{ MovieId=3, Name="Iphone 10", Price=1500, Description="Müko ötesi", IsApproved=true, Image="Guguk-kusu.jpg", CategoryId = 3},
                new Movie{ MovieId=4, Name="Iphone 13", Price=2000, Description="Mükooooo", IsApproved=false, Image="Dovus-kulubu-fight-club.jpg", CategoryId=1}
            };
        }
        public static List<Movie> Movies
        {
            get
            {
                return _movies;
            }
        }
        public static void AddMovie(Movie movie)
        {
            _movies.Add(movie);
        }
        public static Movie GetMovieById(int id)
        {
            return _movies.FirstOrDefault(m=>m.MovieId == id);
        }
        public static void UpdateMovie(Movie movie)
        {
            foreach (var item in _movies)
            {
                if(item.MovieId == movie.MovieId)
                {
                    item.Name = movie.Name;
                    item.Price = movie.Price;
                    item.Description = movie.Description;
                    item.IsApproved = movie.IsApproved;
                    item.Image = movie.Image;
                    item.CategoryId = movie.CategoryId;
                }
            }
        }
        public static void DeleteMovie(int id)
        {
            var item = GetMovieById(id);
            if(item != null)
            {
                _movies.Remove(item);
            }
        }
    }
}
