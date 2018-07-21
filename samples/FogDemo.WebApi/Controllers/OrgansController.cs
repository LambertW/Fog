using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Fog.Dependency;
using FogDemo.Core.Domain;
using FogDemo.EntityFrameworkCore.EntityFrameworkCore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FogDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgansController : ControllerBase
    {
        private readonly IOrganRepository _organRepository;

        public OrgansController(IOrganRepository organRepository)
        {
            _organRepository = organRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Organ>>> GetAll()
        {
            return await _organRepository.GetAllListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organ>> GetById(Guid id)
        {
            using (var scope = IocManager.Instance.IocContainer.BeginLifetimeScope())
            {
                var repository = scope.Resolve<IOrganRepository>();

                return await repository.GetAsync(id);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Organ>> Add(Organ org, Guid parentId)
        {
            var organ = await _organRepository.InsertAsync(org, parentId);

            return new JsonResult(organ);
        }
    }
}