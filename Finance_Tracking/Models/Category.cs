﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_Tracking.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Column(TypeName = "nvarchar(50)")]
		public string Title { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string Icon { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Type { get; set; } = "Expense";

    }
}

