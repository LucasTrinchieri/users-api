﻿
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Users_api.Models;

namespace Users_api.Repository
{
    public class UserRepostory : IRepository<User>
    {
        private UsersContext _context;

        public UserRepostory(UsersContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> Get() =>
            await _context.Users.ToListAsync();

        public async Task<User> GetById(int id) => 
            await _context.Users.FindAsync(id);

        public async Task Create(User entity) => await _context.Users.AddAsync(entity);

        public void Delete(User entity) => _context.Users.Remove(entity);

        public async Task Save() => await _context.SaveChangesAsync();

        public void Update(User user)
        {
            _context.Users.Attach(user);
            _context.Users.Entry(user).State = EntityState.Modified;
        }
    }
}
