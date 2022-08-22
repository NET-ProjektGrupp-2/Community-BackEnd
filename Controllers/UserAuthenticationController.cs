//using Community_BackEnd.Entities;
//using IdentityModel;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace Community_BackEnd.Controllers
//{

//    public class UserAuthenticationController : Controller
//    {
//        [HttpGet("Admins")]
//        [Authorize(Roles = "Admin")]
//       [ Route("[controller]")]
//       // [ApiController]
//        //public IActionResult AdminsEndpoint()
//        //{
//        //    var currentUser = GetCurrentUser();

//        //    return Ok($"Hi {currentUser.Firstname}, you are an {currentUser.Role}");
//        //}


//        [HttpGet("Authors")]
//        [Authorize(Roles = "Author")]
//        public IActionResult SellersEndpoint()
//        {
//            var currentUser = GetCurrentUser();

//            return Ok($"Hi {currentUser.Firstname}, you are a {currentUser.IdentityUserRoles}");
//        }

//        [HttpGet("AdminsAndAuthor")]
//        [Authorize(Roles = "Administrator,Author")]
//        public IActionResult AdminsAndAuthorsEndpoint()
//        {
//            var currentUser = GetCurrentUser();

//            return Ok($"Hi {currentUser.Firstname}, you are an {currentUser.IdentityUserRoles}");
//        }

//        [HttpGet("Public")]
//        public IActionResult Public()
//        {
//            return Ok("Hi, you're on public property");
//        }

//        private User GetCurrentUser()
//        {
//            var identity = HttpContext.User.Identity as ClaimsIdentity;

//            if (identity != null)
//            {
//                var userClaims = identity.Claims;

//                return new User("user")
//                {
//                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
//                  //  EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
//                    Firstname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
//                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
//                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
//                };
//              }

//                return null;
//        }
//    }
//}
