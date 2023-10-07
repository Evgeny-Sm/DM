using AutoMapper;
using DM.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.BLL.Services
{
    public class ReleaseService
    {
        private readonly IMapper? _mapper;
        private readonly IDbContextFactory<DMContext>? _contextFactory;
        private readonly IConfiguration _configuration;
        public ReleaseService(IMapper? mapper, IDbContextFactory<DMContext>? contextFactory, IConfiguration configuration)
        {
            _mapper = mapper;
            _contextFactory = contextFactory;
            _configuration = configuration;
        }
        public async Task<IEnumerable<ReleaseDTO>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var releases = await context.Releases.Include(p => p.FileUnits).ToListAsync();
            var result = _mapper.Map<IEnumerable<ReleaseDTO>>(releases);
            return result;
        }
    }
}
