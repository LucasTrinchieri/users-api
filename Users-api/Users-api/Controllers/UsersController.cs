﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Users_api.Dto;
using Users_api.Models;
using Users_api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Users_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ICRUD<UserDTO, UserInsertDTO, UserUpdateDTO> _users;
        public UsersController([FromKeyedServices("UserService")] ICRUD<UserDTO, UserInsertDTO, UserUpdateDTO> users)
        {
            _users = users;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get() => await _users.Get();

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await _users.GetById(id);

            return user != null ? Ok(user) : NotFound();
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Post([FromBody] UserInsertDTO userInsertDTO)
        {
            try
            {
                var user = await _users.Create(userInsertDTO);

                return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var user = await _users.Update(id, userUpdateDTO);

                return user != null ? Ok(user) : NotFound();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> Delete(int id)
        {
            var user = await _users.Delete(id);

            return user != null ? Ok(user) : NotFound();
        }
    }
}