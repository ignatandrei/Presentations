# 05-telemetry-operations

Minimal app for telemetry export/import and query workflows.

## Present
- Show this as the smallest target for telemetry commands.
- Focus on CLI + dashboard telemetry operations.

## Run
```powershell
cd .\examples\05-telemetry-operations
aspire start --apphost .\apphost.cs
aspire otel logs -f
aspire otel traces
aspire export --output .\artifacts\telemetry.zip
aspire stop --all
```
