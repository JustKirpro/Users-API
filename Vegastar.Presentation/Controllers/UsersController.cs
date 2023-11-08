using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Vegastar.Domain.Contracts;
using Vegastar.Domain.Entities;
using Vegastar.Presentation.Models;

namespace Vegastar.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CreateUserResponseModel>> CreateUser(CreateUserRequestModel requestModel)
    {
        var userToAdd = _mapper.Map<User>(requestModel);
        var addedUser = await _userService.AddUserAsync(userToAdd);
        
        return _mapper.Map<CreateUserResponseModel>(addedUser);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GetUserResponseModel>>> GetUsers()
    {
        var foundUsers = await _userService.GetUsersAsync();
        
        return foundUsers
            .Select(_mapper.Map<GetUserResponseModel>)
            .ToList();
    }
    
    [HttpGet("{id:long:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserResponseModel>> GetUser(long id)
    {
        var foundUser = await _userService.GetUserByIdAsync(id);
        
        return _mapper.Map<GetUserResponseModel>(foundUser);
    }
    
    [HttpDelete("{id:long:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveUser(long id)
    {
        await _userService.RemoveUserById(id);
        
        return NoContent();
    }
}