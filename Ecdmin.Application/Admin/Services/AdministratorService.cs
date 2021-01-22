using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Application.Common;
using Ecdmin.Application.Utils;
using Ecdmin.Core.Entities.Admin;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.LinqBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecdmin.Application.Admin.Services
{
    public class AdministratorService : IAdministratorService, ITransient 
    {
        private readonly IRepository<Administrator> _administratorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdministratorService(IRepository<Administrator> administratorRepository, IHttpContextAccessor httpContextAccessor)
        {
            _administratorRepository = administratorRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<EntityEntry<Administrator>> Add(Administrator Administrator)
        {
            Administrator.Password = PasswordUtil.HashPassword(Administrator, Administrator.Password);
            Administrator.CreatedTime = DateTimeOffset.Now;
            return await _administratorRepository.InsertAsync(Administrator);
        }

        public async Task<bool> IsExisted(string username)
        {
            return await _administratorRepository.AnyAsync(t => t.Username == username);
        }

        public async Task<string> Login(string username, string password)
        {
            var Administrator = await _administratorRepository.Where(t => t.Username == username).FirstOrDefaultAsync();

            if (Administrator == null || !PasswordUtil.VerifyHashedPassword(Administrator, Administrator.Password, password))
                throw Oops.Oh("用户名或密码错误").StatusCode(400);
            
            var token = JwtUtil.Generate(Administrator.Id.ToString(), Administrator.Username);
            _httpContextAccessor.SigninToSwagger(token);
            return token;
        }

        public async Task<Administrator> Find(int id)
        {
            return await _administratorRepository.FindOrDefaultAsync(id);
        }

        public async Task<PagedList<Administrator>> GetList(AdministratorRequest.Get getParams)
        {
            var query = _administratorRepository.AsQueryable();
            if (!getParams.Name.IsNullOrEmpty())
            {
                query = _administratorRepository.Where(t => t.Name.Contains(getParams.Name));
            }

            return await query.OrderByDescending(t => t.Id)
                .ToPagedListAsync(getParams.Page, getParams.PageSize);
        }

        public async Task<EntityEntry<Administrator>> Update(int id, AdministratorRequest.EditInput editInput)
        {
            var administrator = await _administratorRepository.FindOrDefaultAsync(id);
            if (administrator == null)
            {
                ExceptionService.NotFound();
            }

            if (editInput.Password != null)
            {
                administrator.Password = PasswordUtil.HashPassword(administrator, editInput.Password);
            }

            administrator.Name = editInput.Name;
            administrator.UpdatedTime = DateTimeOffset.Now;
            administrator.Avatar = editInput.Avatar;

            return await administrator.UpdateAsync();
        }

        public async Task Delete(int id)
        {
            await _administratorRepository.FakeDeleteAsync(id);
        }
    }
}