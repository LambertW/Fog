using Fog.Domain.Repositories;
using FogDemo.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FogDemo.WebApi.Repositories
{
    public interface IOrganRepository : ITreeRepository<Organ>
    {

    }
}
