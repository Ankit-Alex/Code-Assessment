using Core.Configuration;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Zeiss_TakeHome.DataAccess.Data;

namespace Infrastructure.Services
{
    public class DbSequenceProductIdGenerator : IProductIdGenerator
    {
        private readonly DbContext _context;
        private readonly int _nodeId;
        private const int NodeIdMax = 9;

        public DbSequenceProductIdGenerator(
            AppDbContext context, 
            IOptions<DistributedSystemOptions> options)
        {
            _context = context;
            _nodeId = options.Value.NodeId;

            if (_nodeId < 0 || _nodeId > NodeIdMax)
                throw new ArgumentException(
                    $"Node ID in configuration must be between 0 and {NodeIdMax}. Current value: {_nodeId}", 
                    nameof(options));
        }

        public async Task<string> GenerateProductIdAsync()
        {
            // Get next value from sequence
            var sequenceValue = await _context.Database
                .SqlQuery<long>($"""
                                    SELECT nextval('productidsequence') AS "Value"
                                """)
                .FirstAsync();

            // Format as nodeId + 5-digit sequence with leading zeros
            return $"{_nodeId}{sequenceValue:D5}";
        }
    }
} 