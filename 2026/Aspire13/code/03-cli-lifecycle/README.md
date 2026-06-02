# 03-cli-lifecycle

Minimal app to demo Aspire lifecycle commands.

## Present
- Show one simple resource (`cache`).
- Focus on lifecycle UX from CLI.

## Run
```powershell
cd .\examples\03-cli-lifecycle
aspire start --apphost .\apphost.cs
aspire ps
aspire describe
aspire wait cache --status healthy --timeout 120
aspire stop --all
```


https://aspire.dev/whats-new/aspire-13-2/#%EF%B8%8F-cli-enhancements

https://aspire.dev/whats-new/aspire-13-3/#%EF%B8%8F-cli-enhancements

Also 

aspire docs search "redis"


aspire docs get connect-to-redis

aspire dashboard run

# Starter templates
aspire new aspire-py-starter


aspire logs
