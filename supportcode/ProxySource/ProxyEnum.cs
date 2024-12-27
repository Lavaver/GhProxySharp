/// <summary>
/// GitHub 代理源枚举用法<br/>请注意：这些代理源均来自第三方或爱好者。本作者不对滥用这些服务造成包括但不限于限制使用、封禁 IP 等后果做任何担保。
/// </summary>
public enum ProxyEnum
{
    /// <summary>
    /// 萌歪代理源（https://moeyy.xyz）<br/>延迟较为平衡（校园网平均 185 ms），且下载速度快，适合一般场景下使用
    /// </summary>
    [ProxyEnum("https://github.moeyy.xyz")] 
    Moeyy,
    
    /// <summary>
    /// Doocs 开源社区代理源<br/>延迟较为平衡（与萌歪代理源一致），下载速度平衡，适合一般场景下使用
    /// </summary>
    [ProxyEnum("https://gh.llkk.cc")]
    Llkk,
    
    /// <summary>
    /// 永至中国大陆代理源（https://www.znnu.com）<br/>延迟较为平衡（校园网平均 170 ms），下载速度极快（基本能跑满宽带上限），适合校园网或急需下载环境下使用
    /// </summary>
    [ProxyEnum("https://ghproxy.cn")]
    GitProxyCN,
    
    /// <summary>
    /// 永至国际代理源（https://www.znnu.com）<br/>延迟较高（校园网平均 232 ms），下载速度平衡，适合当中国大陆代理源不可用时备用
    /// </summary>
    [ProxyEnum("https://ghproxy.net")]
    GitProxyNet,
    
    /// <summary>
    /// Sianao 代理源<br/>延迟秒杀其他代理源（校园网平均 49.1 ms，在某些情况下最低可达到 10.3ms），下载速度极快（与永至中国大陆代理源一致），适合校园网或急需下载环境下使用
    /// </summary>
    [ProxyEnum("https://gitproxy.click")]
    GitProxyClick
}

[AttributeUsage(AttributeTargets.All)]
internal class ProxyEnumAttribute(string? value) : Attribute
{
    public string? Value { get; } = value;
}

public static class ProxyEnumExtensions
{
    public static string? GetValue(this ProxyEnum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo == null) return null; // 检查 fieldInfo 是否为 null
        var attributes = fieldInfo.GetCustomAttributes(typeof(ProxyEnumAttribute), false);
        return (attributes.Length > 0) ? ((ProxyEnumAttribute)attributes[0]).Value : null;
    }
}
