using Application.DTOs.Users;
using Application.IRepositories;
using AutoMapper;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Api;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("/api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUserRepository _usersRepository;


    public UsersController(IMapper mapper, IUserRepository usersRepository, AppDbContext appDbContext)
    {
        _mapper = mapper;
        _appDbContext = appDbContext;
        _usersRepository = usersRepository;
    }

    //Paginated list of users
    // [HttpGet]
    // public IActionResult GetAllUsers(int page, int perPage, string? name = null)
    // {
    //     var result = _usersRepository.GetAllUsers(page, perPage, name);
    //
    //     return Ok(new { message = "success", data = result });
    // }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var result = _usersRepository.GetAllUsers();

        return Ok(new { message = "success", data = result });
    }


    [HttpPut("{id}")]
    public ActionResult Put(Guid Id, UserUpdateDto usersUpdateDto)
    {
        var _user =
            _appDbContext
                .Set<Users>()
                .FirstOrDefault(b => b.Id == Id);

        if (_user == null)
        {
            return NotFound();
        }

        var result = _usersRepository.Update(Id, usersUpdateDto);
        Console.WriteLine(result);
        return Ok("success");
    }


    // [HttpDelete("{id}")]
    // public ActionResult Delete(Guid id)
    // {
    //     var _user = _usersRepository.Delete(id);
    //     if (_user == null) return BadRequest();
    //
    //     return Ok("delete success");
    // }
}