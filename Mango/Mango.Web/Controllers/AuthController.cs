using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider, TokenValidationParameters tokenValidationParameters)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
            _tokenValidationParameters = tokenValidationParameters;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
            return View(loginRequestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            ResponseDTO responseDTO = await _authService.LoginAsync(obj);
            //ResponseDTO assignRole;

            if (responseDTO == null)
            {
                ModelState.AddModelError("CustomError", "Login service returned no response");
                TempData["error"] = "Login failed";
                return View(obj);
            }

            if  (!responseDTO.IsSuccess)
            {
                ModelState.AddModelError("CustomError", responseDTO.Message ?? "Login failed");
                TempData["error"] = "Login failed";
                return View(obj);
            }

            if (responseDTO.Result == null)
            {
                ModelState.AddModelError("CustomError", "Login failed: empty result return");
                TempData["error"] = "Login failed";
                return View(obj);
            }


            var loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(responseDTO.Result.ToString());
            await SignInUser(loginResponseDTO);
            _tokenProvider.SetToken(loginResponseDTO.Token);
            

            TempData["success"] = "Login successfully";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            { 
                new SelectListItem
                {
                    Text = SD.RoleAdmin,
                    Value = SD.RoleAdmin,
                },
                new SelectListItem
                {
                    Text = SD.RoleCustomer,
                    Value = SD.RoleCustomer,
                }
            };

            ViewBag.RoleList = roleList;
            
            return View();
        }
        
        [HttpPost]
        public async Task <IActionResult> Register(RegisterationRequestDTO obj)
        {
            ViewBag.RoleList = GetRoleList();

            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            //ResponseDTO result = await _authService.RegisterAsync(obj);
            var result = await _authService.RegisterAsync(obj);
            //ResponseDTO assignRole;

            if (result == null || !result.IsSuccess)
            {
                ModelState.AddModelError("", result?.Message ?? "Registration failed");
                Debug.WriteLine("failed");
                TempData["error"] = "FAILED";
                return View(obj);
            }

            obj.Role ??= SD.RoleCustomer;

            var assginRole = await _authService.AssignRoleAsync(obj);

            if (assginRole == null || !assginRole.IsSuccess)
            {
                ModelState.AddModelError("", assginRole?.Message ?? "Assign role failed");
                Debug.WriteLine("failed");
                TempData["error"] = "FAILED";
                return View(obj);
            }

            TempData["success"] = "Registration successful";
            Debug.WriteLine("successful");
            return RedirectToAction(nameof(Login));
        }

        private List<SelectListItem> GetRoleList()
        {
            return new List<SelectListItem>
            {
                    new SelectListItem
                    {
                        Text = SD.RoleAdmin, Value = SD.RoleAdmin
                    },
                    new SelectListItem
                    {
                        Text = SD.RoleCustomer,
                        Value= SD.RoleCustomer,
                    },
            };
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            //return View();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDTO model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            
            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            //identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            //    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            foreach (var role in jwt.Claims.Where(c => c.Type == ClaimTypes.Role))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Value));
            }


            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }


        //private async Task SignInUser(LoginResponseDTO model)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    //var jwt = handler.ReadJwtToken(model.Token);
        //    var jwt = handler.ValidateToken(
        //        model.Token,
        //        tokenValidationParameters,
        //        out SecurityToken validatedToken
        //        );

        //    var claims = new List<Claim>();

        //    var email = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        //    var sub = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        //    var name = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;

        //    if (!string.IsNullOrEmpty(email))
        //        claims.Add(new Claim(ClaimTypes.Email, email));
        //    if (!string.IsNullOrEmpty(sub))
        //        claims.Add(new Claim(ClaimTypes.NameIdentifier, sub));
        //    if (!string.IsNullOrEmpty(name))
        //        claims.Add(new Claim(ClaimTypes.Name, name));

        //    var identity = new ClaimsIdentity(
        //        claims,
        //        CookieAuthenticationDefaults.AuthenticationScheme
        //        );

        //    var principal = new ClaimsPrincipal(identity);

        //    await HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        principal
        //    );
        //}
    }
}
