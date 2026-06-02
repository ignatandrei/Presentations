# 04-pipelines-dashboard

Minimal app to show pipeline-driven workflows and dashboard launch.

https://aspire.dev/reference/cli/commands/aspire-do/


## Present
- Show custom pipeline step (`demo-check`) in `apphost.cs`.
- Show dashboard as part of CLI workflow.

## Run
```powershell
cd .\examples\04-pipelines-dashboard
aspire start --apphost .\apphost.cs
aspire do demo-check

```


```powershell
aspire stop --all
```

Also 

aspire docs search "redis"


aspire docs get connect-to-redis

# Telemetry incorporated
aspire run

# telemetry external
dotnet run .\netcoreTel.cs
aspire dashboard run

