﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseApp.Models
{
    [Index("Email", IsUnique = true)]
	public class Employee
	{
		public int Id { get; set; }

		[StringLength(30)]
		public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [StringLength(30)]
        public string Password { get; set; } = string.Empty;

        public bool Admin { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal ExpensesDue { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal ExpensesPaid { get; set; }
    }
}

