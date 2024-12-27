using System.Diagnostics;

namespace GitHub_Proxy.SupportLib.Download;

public static class PingProxySourceAsync
{
    public static async Task<ProxyEnum> GetFastestProxyAsync()
    {
        var tasks = (from ProxyEnum proxy in Enum.GetValues(typeof(ProxyEnum)) select PingProxyAsync(proxy)).ToList();

        var results = await Task.WhenAll(tasks);
        var fastest = results.OrderBy(result => result.duration).First();
        return fastest.proxy;
    }

    private static async Task<(ProxyEnum proxy, long duration)> PingProxyAsync(ProxyEnum proxy)
    {
        var url = proxy.GetValue();
        if (url == null) return (proxy, long.MaxValue);

        var stopwatch = Stopwatch.StartNew();

        using (var httpClient = new HttpClient())
        {
            try
            {
                // 尝试发送一个简单的 GET 请求以进行 Ping
                var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                if (response.IsSuccessStatusCode)
                {
                    stopwatch.Stop();
                    return (proxy, stopwatch.ElapsedMilliseconds);
                }
            }
            catch
            {
                // 捕获异常，返回最大值以标识失败
            }
        }

        stopwatch.Stop();
        return (proxy, long.MaxValue); // 如果请求失败，返回最大值
    }
}