﻿using System.ComponentModel.DataAnnotations;

namespace Tiles.Core.DTO.UserDto
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;
    }
}
