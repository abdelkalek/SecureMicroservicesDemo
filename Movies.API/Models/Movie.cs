﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Movies.API.Models
{
    public class Movie
    {
		[Key, Column("id")]
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Genre { get; set; }
		public string Rating { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string ImageUrl { get; set; }
		public string Owner { get; set; }
	}
}
