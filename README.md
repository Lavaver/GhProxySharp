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

## 如何用这个工具

- 下载模式：使用 `<程序名> download <URL> <下载到何处>`，即可轻松下载！

> 各家代理源对于可下载文件来源不相同。有的只能从 GitHub Releases 下载，有的既可以从 Releases 下载，也可以从其他来源代理下载。具体请参阅各家代理源对于代理下载文件来源的要求。

- 克隆模式：使用  `<程序名> clone <格式为 持有者/仓库名 的仓库 URL> <克隆到何处> [分支] [仓库源]`

> 你的计算机必须安装了 Git ，然后你才能使用此模式。

> 你也可以输入仓库的完整 URL 。但在克隆之前程序会提示 `选中仓库 [持有者/仓库名] 而并非 URL [完整地址]`，就像使用 `apt 或 apt-get` 从本地 deb 安装软件包一样 。程序会自动截断 URL 中的持有者与仓库名，并自动引用它。

> 在程序默认行为下会使用 `main` 分支（即新版 GitHub 所使用的主分支名称）。但有些老仓库仍在使用 `master` 分支。在克隆之前，请先确认该仓库（及其可能的上游仓库）主要分支名称。

## 支持主流的代理源

GitHub Proxy on Sharp 支持主流的代理源，并自动挑选最快的代理源下载。

> 这些代理源均来自和大家一样的爱好者。但本作者不对这些代理源所下载的任何文件（或克隆的任何仓库）做任何担保。

支持的代理源列表：

- [萌歪（Moeyy）](https://github.moeyy.xyz)
- [Doocs 开源社区](https://gh.llkk.cc)
- [永至中国大陆](https://ghproxy.cn)
- [永至国际](https://ghproxy.net)
- [Sianao](https://gitproxy.click)

在此特别感谢这些代理源的开发者与维护者！没有你们，这个项目根本不会成立！