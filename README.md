# GitHub Proxy on Sharp

一款基于命令行（CLI）的 GitHub Proxy 代理下载器。使用 C# + DotNet 8 编写。

## 经典的代理下载

我，和其他爱好者一样，致力于解决 GitHub Releases / 仓库克隆的世纪性下载难题。

现在基于网页端的代理下载网站已经很多，但每次下载都需要打开浏览器，输入代理地址才能开始下载

这好吗？这不好！

GitHub Proxy on Sharp 在**保留原汁原味的代理下载体验**时尽可能的**从本地出发**。你只需下载并安装（解压）它，输入 `ghproxy`  、下载地址等基本信息，剩下的就交给程序吧！

## 主流系统与体系架构咱都有

得益于 DotNet Core 的多系统支持，从大家广泛使用的 Windows ，再到部分用户使用的 macOS ，这款软件都支持！

### 支持的操作系统
- Microsoft Windows
- Linux
- macOS

### 支持的体系架构
- amd64
- i386
- ARM64
- Apple Silicon (ARM64)

## 支持 Git 克隆整个仓库

我们将软件与 Git 紧密连接，实现不用额外输入 Git 命令就可克隆整个仓库中的内容，又快又稳。

> 你的计算机需要安装 Git 才能使用此功能。

## 没了（逃）