# 01-python-app-model

Minimal Aspire example for Python resources.

## Present
- Show `apphost.cs` and `src/app.py`.
- Explain: Aspire runs Python as a first-class app resource.

## Run
```powershell
cd .\examples\01-python-app-model
aspire start --apphost .\apphost.cs
aspire describe
aspire stop --all
```

