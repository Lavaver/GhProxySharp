using System.Diagnostics;

namespace GitHub_Proxy.SupportLib.Download;

public class ProxyGitClone
{
    /// <summary>
    /// 使用代理源加速 Git Clone <br/> 你的计算机必须安装 Git 才能使用此方法
    /// </summary>
    /// <param name="repository">仓库路径（必须为“持有者/仓库名”）</param>
    /// <param name="savePath">克隆到</param>
    /// <param name="branchName">分支名称（默认为主分支“main”。部分老仓库主分支名称可能为“master”，在传递该形参时需要额外注意）</param>
    /// <param name="repoSource">仓库根来源（默认为 github.com，但你可以选择性更改仓库来源）</param>
    public static async Task RunGitClone(string repository, string savePath, string branchName = "main", string repoSource = "github.com")
    {
        // 判断 repository 是否为 URL
        string truncatedRepository = repository;
        if (Uri.IsWellFormedUriString(repository, UriKind.Absolute))
        {
            var uri = new Uri(repository);
            var segments = uri.Segments;
            if (segments.Length >= 2)
            {
                truncatedRepository = $"{segments[1].Trim('/')}/{segments[2].TrimEnd('/')}";
                Console.WriteLine($"注意：选中仓库 [{truncatedRepository}] 而并非 URL [{repository}]");
            }
        }

        var fastProxy = await PingProxySourceAsync.GetFastestProxyAsync();
        var sourceUrl = fastProxy.GetValue();
        Console.WriteLine($"已选中代理源为 {fastProxy}（{sourceUrl}）");
        
        // 构建 git clone 命令
        var command = $"clone -b {branchName} {sourceUrl}/https://{repoSource}/{truncatedRepository} {savePath}";

        // 创建进程启动信息
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = command,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            // 启动进程
            using var process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();

            // 读取输出
            var output = await process.StandardOutput.ReadToEndAsync();

            await process.WaitForExitAsync();
            Console.WriteLine(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"发生异常: {ex.Message}");
        }
    }
}
