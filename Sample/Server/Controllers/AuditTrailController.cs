﻿using AuditTrail.Common;
using EfAudit.Common.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("audit")]
    public class AuditTrailController : ControllerBase, IAuditRecordHandler
    {
        private static readonly List<object> _records = new();

        [NonAction]
        public async Task Handle(IServiceProvider services, AuditRecord auditRecord, CancellationToken cancellationToken)
        {
            using var scope = services.CreateScope();

            var mapper = scope.ServiceProvider.GetRequiredService<IAuditRecordToAuditMessageMapper>();
            var r = await mapper.MapAsync(auditRecord);

            if (r != null)
                _records.Add(r);
        }


        /// <summary>
        /// Gets all audittrail records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            return Ok(_records);
        }

    }
}
