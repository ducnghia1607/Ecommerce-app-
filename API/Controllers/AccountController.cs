using System.Security.Claims;
using API.Controllers;
using API.Extensions;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
    {
        _signInManager = signInManager;
        _userManager = userManager;

        _tokenService = tokenService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    // Get current user : hoạt động khi người dùng vào lại trang web ta sẽ gửi yêu cầu để lấy thông tin UserDto ( lưu trữ lại trạng thái đã đăng nhập  ) : cần yêu cầu token 
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        //    HttpContext.User = User (ClaimPrincipal )

        // var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        // var email = User.FindFirstValue(ClaimTypes.Email);

        // User: Gets the ClaimsPrincipal for user associated with the executing actions

        var user = await _userManager.FindUserByClaimsPrincipal(HttpContext.User);

        return new UserDto
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user)
        };

    }

    [Authorize]
    [HttpGet("address")]
    public async Task<AddressDto> GetUserAddress()
    {
        // var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        // var user = await _userManager.FindByEmailAsync(email);
        // return user.Address;
        var user = await _userManager.FindUserByClaimsPrincipalWithAddress(HttpContext.User);
        return _mapper.Map<Address, AddressDto>(user.Address);
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
    {
        var user = await _userManager.FindUserByClaimsPrincipalWithAddress(HttpContext.User);

        user.Address = _mapper.Map<AddressDto, Address>(addressDto);
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest("Something went wrong while updating address");
        return Ok(_mapper.Map<Address, AddressDto>(user.Address));
    }

    [HttpGet("emailExists")]
    // Phục vụ cho validation Async bên phía client có thể gửi request để check 
    public async Task<bool> CheckEmailExists([FromQuery] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null;
    }


    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

        return new UserDto
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> register(RegisterDto registerDto)
    {
        var user = new AppUser
        {
            Email = registerDto.Email,
            DisplayName = registerDto.DisplayName,
            UserName = registerDto.Email
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        return new UserDto
        {
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user)
        };
    }


}
