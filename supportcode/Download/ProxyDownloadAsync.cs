namespace GitHub_Proxy.SupportLib.Download;

public class ProxyDownloadAsync
{
public static async Task Run(string url, string savePath)
{
    var fastProxy = await PingProxySourceAsync.GetFastestProxyAsync();
    var sourceUrl = fastProxy.GetValue();
    Console.WriteLine($"已选中代理源为 {fastProxy}（{sourceUrl}）");

    try
    {
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync($"{sourceUrl}/{url}", HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                // 从 URL 中提取文件名
                var fileName = GetFileNameFromUrl(url);
                var fullPath = Path.Combine(savePath, fileName);

                // 获取要下载的文件大小
                var totalBytes = response.Content.Headers.ContentLength ?? -1;

                // 检查剩余磁盘空间
                var driveInfo = new DriveInfo(Path.GetPathRoot(savePath) ?? string.Empty);
                var availableSpace = driveInfo.AvailableFreeSpace;

                // 单位换算函数
                string FormatBytes(long bytes)
                {
                    const long KB = 1024;
                    const long MB = KB * 1024;
                    const long GB = MB * 1024;

                    return bytes switch
                    {
                        >= GB => $"{bytes / (double)GB:F2} GiB",
                        >= MB => $"{bytes / (double)MB:F2} MiB",
                        _ => $"{bytes / (double)KB:F2} KiB"
                    };
                }

                // 显示可用空间和文件大小
                if (totalBytes > availableSpace)
                {
                    Console.WriteLine($"警告：剩余磁盘空间不足，文件需要占用 {FormatBytes(totalBytes)}，但可用空间为 {FormatBytes(availableSpace)}。");
                    return;
                }

                // 显示确认提示
                Console.WriteLine($"此文件下载后会占用约 {FormatBytes(totalBytes)} 的额外磁盘空间，继续下载？[Y/N]");
                var confirmation = Console.ReadLine();
                if (confirmation?.Trim().ToUpper() != "Y")
                {
                    Console.WriteLine("下载已取消。");
                    return;
                }

                var contentStream = await response.Content.ReadAsStreamAsync();
                var buffer = new byte[81920]; // 80KB 缓冲区
                long totalBytesRead = 0;
                var watch = System.Diagnostics.Stopwatch.StartNew();

                await using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    int bytesRead;
                    while ((bytesRead = await contentStream.ReadAsync(buffer)) > 0)
                    {
                        await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                        totalBytesRead += bytesRead;

                        // 更新进度信息
                        if (totalBytes > 0)
                        {
                            var progress = (double)totalBytesRead / totalBytes; // 计算进度
                            var progressBar = new string('#', (int)(progress * 50)) +
                                              new string('-', 50 - (int)(progress * 50)); // 进度条
                            var percent = progress * 100; // 百分比

                            // 计算ETA
                            var elapsedSeconds = watch.Elapsed.TotalSeconds;
                            double estimatedTotalSeconds = (totalBytes / (totalBytesRead / elapsedSeconds)); // 估算总时间
                            var eta = TimeSpan.FromSeconds(estimatedTotalSeconds - elapsedSeconds); // 剩余时间

                            // 打印进度信息
                            Console.Write($"\r[{progressBar}] {percent:F2}% {eta:hh\\:mm\\:ss}");
                        }
                    }
                }
            }

            Console.WriteLine("\n文件下载完成!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"在下载时发生 {ex.Message}");
        Console.WriteLine("下载意外终止时最后一次发出的堆栈信息：");
        Console.WriteLine(ex.StackTrace);
        Console.WriteLine("请不要将出现此错误的截图发给任何人，这解决不了问题");
        Console.WriteLine("你应该将意外出错的全部信息复制并提交到 https://github.com/Lavaver/GhProxySharp/issues/new 当中");
    }
}


    // 从 URL 中获取文件名
    private static string GetFileNameFromUrl(string url)
    {
        return Path.GetFileName(url);
    }

    // 从响应中获取文件名
    private static string? GetFileNameFromResponse(HttpResponseMessage response)
    {
        var fileName = response.Content.Headers.ContentDisposition?.FileName?.Trim('"');
        return fileName;
    }
}
