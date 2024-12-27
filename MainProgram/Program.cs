using GitHub_Proxy.SupportLib.Download;

namespace GitHub_Proxy.MainProgram;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        // 检查参数个数
        if (args.Length >= 3)
        {
            switch (args[0])
            {
                case "download":
                    // 调用下载方法
                    await ProxyDownloadAsync.Run(args[1], args[2]);
                    break;
                case "clone":
                    // 检查克隆操作的附加参数
                    if (args.Length == 3)
                    {
                        await ProxyGitClone.RunGitClone(args[1], args[2]);
                    }
                    else if (args.Length == 4)
                    {
                        await ProxyGitClone.RunGitClone(args[1], args[2], args[3]);
                    }
                    else if (args.Length == 5)
                    {
                        await ProxyGitClone.RunGitClone(args[1], args[2], args[3], args[4]);
                    }
                    break;
                default:
                    DisplayUsageInfo();
                    break;
            }
        }
        else
        {
            DisplayUsageInfo();
        }
    }

    // 输出用法信息
    private static void DisplayUsageInfo()
    {
        Console.WriteLine("用法：GhProxy[.exe] <download/clone> <URL/Repository> <SavePath> [BranchName] [repoSource]");
        Console.WriteLine("限制：在下载模式中，不能使用后两个形参。后两个形参为克隆模式可选项。");
    }
}
