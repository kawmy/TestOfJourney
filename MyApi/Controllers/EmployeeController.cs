
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Exceptions;
using Data.Repositories;
using Entities;
using Entities.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace MyApi.Controllers.v1
{
    [ApiVersion("1")]
    [AllowAnonymous]
    public class EmployeeController : BaseController
    {
        private readonly IRepository<Employee> CategoryRepository;

        private readonly IMapper mapper;
        public EmployeeController(IRepository<Employee> categoryRepository, IMapper mapper)
        {
            CategoryRepository = categoryRepository;
            this.mapper = mapper;
        }


        [HttpGet("[Action]")]
        public async Task<ApiResult<List<EmployeeDto>>> Get(CancellationToken cancellationToken)
        {
            var Employees =await CategoryRepository.TableNoTracking.ProjectTo<EmployeeDto>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return Ok(Employees);
        }
        
        [HttpGet("[Action]")]
        public async Task<ApiResult<EmployeeDto>> GetById(Guid Id,CancellationToken cancellationToken)
        {
            var Employees =await CategoryRepository.TableNoTracking.Where(x=>x.Id == Id).ProjectTo<EmployeeDto>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            if (Employees.Any())
            {
                return Ok(Employees.First());

            }
            return BadRequest("Error the id you provided is not valid");
        }


        [HttpPost("[Action]")]
        public async Task<ApiResult> Create(EmployeeDto dto, CancellationToken cancellationToken)
        {
            var model = dto.ToEntity(mapper);
            await CategoryRepository.AddAsync(model, cancellationToken);

            return Ok();
        }

        [HttpPut("[Action]")]
        public async Task<ApiResult> Update(EmployeeDto dto, CancellationToken cancellationToken)
        {
            var findOldModel = CategoryRepository.TableNoTracking.FirstOrDefault(x => x.Id == dto.Id);
            if (findOldModel != null)
            {
                var model = dto.ToEntity(mapper,findOldModel);
                await CategoryRepository.UpdateAsync(model, cancellationToken);
                return Ok();
            }
            return BadRequest("Error the id you provided is not valid");
        }
        
        
        [HttpDelete("[Action]")]
        public async Task<ApiResult> Delete(Guid Id, CancellationToken cancellationToken)
        {
            var findModel = CategoryRepository.TableNoTracking.FirstOrDefault(x => x.Id == Id);
            if (findModel != null)
            {
                await CategoryRepository.DeleteAsync(findModel, cancellationToken);
                return Ok();
            }
            return BadRequest("Error the id you provided is not valid");
        }
    }
}
