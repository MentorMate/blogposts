//-----------------------------------------------------------------------
// <copyright file="ToDo.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Api.Data.Models
{
    /// <summary>To do item.</summary>
    public class ToDo
    {
        /// <summary>Gets or sets the identifier.</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Gets or sets the title.</summary>
        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        /// <summary>Gets or sets the status.</summary>
        public ToDoStatus Status { get; set; }
    }
}