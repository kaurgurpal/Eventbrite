using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAccountsAPI.Data;

namespace UserAccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AccountsContext _context;
        public UsersController(AccountsContext context)
        {
            _context = context;
        }
        // Getting user by id
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> UserDetails(int id)
        {
            Domain.ContactInfo user;
            try
            {
                user = await _context.ContactInfos.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return Ok(user);
        }

    }
}