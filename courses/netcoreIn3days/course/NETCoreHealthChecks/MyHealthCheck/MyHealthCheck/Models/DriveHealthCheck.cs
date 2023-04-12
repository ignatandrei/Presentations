using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyHealthCheck.Models
{
    public class DriveHealthCheck : IHealthCheck
    {
        private static int OneMB = 1024 * 1024;
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            
            var driveLetter = System.AppContext.BaseDirectory;
            var drive = new DriveInfo(driveLetter);
            
            var free = drive.AvailableFreeSpace /OneMB;
            var driveMessage = $"drive {drive.Name} has  {free} MB;";
            if (free < 100)
                return Task.FromResult(HealthCheckResult.Unhealthy(driveMessage));

            return Task.FromResult( HealthCheckResult.Healthy(driveMessage));
        }
    }
}
