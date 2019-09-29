using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAccountsAPI.Data;
using UserAccountsAPI.Domain;

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

        //Create New user Account
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<ContactInfo>> AddUser(ContactInfo info)
        {
            try
            {
                await _context.ContactInfos.AddAsync(info);
                await _context.SaveChangesAsync();

                return Ok(info);
            }
            catch
            {
                return BadRequest("Failed to Add User ");
            }
        }
        // Update User's Info
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateEvent([FromBody] ContactInfo info)
        {
            try
            {
                if (_context != null)
                {
                    _context.ContactInfos.Update(info);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                {
                    return NotFound();
                }
                return BadRequest();
            }

            return Ok(info);
        }

        //Delete User Info API
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteUserInfo(int id)
        {
            try
            {
                var itemObj = await _context.ContactInfos.FindAsync(id);
                if (itemObj == null)
                {
                    return NotFound();
                }
                _context.ContactInfos.Remove(itemObj);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}