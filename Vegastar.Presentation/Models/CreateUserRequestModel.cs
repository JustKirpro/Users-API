using System.ComponentModel.DataAnnotations;
using Vegastar.Domain.Entities;

namespace Vegastar.Presentation.Models;

public record CreateUserRequestModel([Required]string Login, [Required]string Password, [Required]UserGroupCode UserGroupCode);