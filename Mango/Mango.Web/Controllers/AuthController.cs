using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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

        public IActionResult Logout()
        {
            return View();
        }
    }
}
