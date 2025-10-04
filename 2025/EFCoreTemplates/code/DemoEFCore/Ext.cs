
namespace DemoEFCore;
public static  class Ext
{
    public static IResourceBuilder<ProjectResource> ExecuteScaffoldEF(this IResourceBuilder<ProjectResource> prj,IResourceBuilder< IResourceWithConnectionString> db)
    {
        ;
        var pathFile = prj.Resource.GetProjectMetadata().ProjectPath;
        var folder = Path.GetDirectoryName(pathFile);

        prj.WithCommand("scaffold", $"Scaffold {db.Resource.Name}",async(ecc) => {

            var log = ecc.ServiceProvider.GetService(typeof(ResourceLoggerService)) as ResourceLoggerService;
            if (log == null) return new ExecuteCommandResult()
            {
                Success = false,
                ErrorMessage = $"no logging available"
            };

            // Get a logger instance for this specific database resource
            var lg = log.GetLogger(prj.Resource);

            var con = await db.Resource.GetConnectionStringAsync();
            var cmd= $"""
            ef dbcontext scaffold "{con}" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Data --context ApplicationDbContext --force --no-onconfiguring --project "{pathFile}" --startup-project "{pathFile}" --nullable --use-database-names
            """;
            lg.LogInformation ($"Running in folder {folder} the command: dotnet {cmd} ");
            var psi=new ProcessStartInfo("dotnet",cmd);
            psi.WorkingDirectory=folder;
            var p=new Process();
            p.StartInfo=psi;
            p.Start();
            await p.WaitForExitAsync();
            if(p.ExitCode!=0)
            {
                return new ExecuteCommandResult()
                {
                    Success = false,
                    ErrorMessage = $"Scaffold failed with exit code {p.ExitCode}"
                };
            }

            return new ExecuteCommandResult()
            {
                Success = true
            };
    });
        return prj;
    }
}
