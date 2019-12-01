﻿using System;
using System.Collections.Generic;

namespace APIProject.Models
{
    public partial class FavMovie
    {

        public FavMovie(string title, int releaseYear, string genre, string runtime, string actor, string plot) 
        {
            Title = title;
            ReleaseYear = releaseYear;
            Genre = genre;
            Runtime = runtime;
            Actor = actor;
            Plot = plot;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Genre { get; set; }
        public string Runtime { get; set; }
        public string Actor { get; set; }
        public string Plot { get; set; }
    }
}
