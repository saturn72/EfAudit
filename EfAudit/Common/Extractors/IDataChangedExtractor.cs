﻿namespace EfAudit.Common.Extractors
{
    public interface IDataChangedExtractor
    {
        AuditMessage? Extract(AuditRecord record);
    }
}
