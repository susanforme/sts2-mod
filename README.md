# sts2-lol-mod / JhinMod

> 中文 | [English](#english)

一个为《Slay the Spire 2》制作的英雄联盟主题角色 Mod。目前项目聚焦于 **烬 / Jhin**：围绕 4 发弹匣、第四枪节奏、华彩爆发、标记狙杀、莲花陷阱与谢幕终结构建完整角色体验。

本项目为粉丝向非官方 Mod，仅用于学习、交流与个人娱乐。

## 特性

- 新角色：**烬 / Jhin**
- 专属战斗资源：**弹匣 / 子弹**
- 核心关键词：
  - 射击
  - 华彩
  - 标记
  - 莲花陷阱
  - 谢幕
  - 装填
- 初始遗物：**低语**
- 设计卡池：**88 张卡牌**
  - 攻击牌：39 张
  - 技能牌：27 张
  - 能力牌：22 张
- 多种构筑方向：
  - 第四枪爆发流
  - 标记狙杀流
  - 莲花陷阱流
  - 谢幕终结流
  - 装填循环流

## 玩法概览

烬拥有 4 发子弹。每使用一张带有“射击”的牌，通常会消耗 1 发子弹。

当最后一发子弹被消耗时，会触发 **华彩**。华彩会强化当前射击，并与烬的初始遗物“低语”产生额外伤害联动。

你需要规划每一发子弹的使用时机，决定何时打空弹匣、哪一张牌吃到第四枪收益，以及如何配合标记、陷阱和谢幕完成终结。

## 安装

### 方法一：下载 Release

1. 打开本仓库的 **Releases** 页面。
2. 下载最新版本的 Mod 压缩包。
3. 解压后，将 `JhinMod` 文件夹放入《Slay the Spire 2》的 Mods 目录中。

常见路径示例：

```text
...\Steam\steamapps\common\Slay the Spire 2\Mods\JhinMod
````

Mod 文件夹中通常应包含：

```text
JhinMod.dll
JhinMod.pck
JhinMod.json
```

本 Mod 依赖 **BaseLib**。请确保 BaseLib 已正确安装。

### 方法二：从源码构建

环境需求：

* Windows
* .NET 9.0 SDK
* Godot .NET / Mono 对应版本
* Slay the Spire 2
* BaseLib

构建：

```powershell
cd jhin
.\pack.ps1 -Release
```

构建并部署到本地游戏 Mods 目录：

```powershell
cd jhin
.\pack.ps1 -Release -Deploy
```

> 注意：`pack.ps1` 中包含本地路径配置，例如 Godot 路径、项目路径和游戏 Mods 路径。首次使用前请根据自己的电脑环境修改。

## 项目结构

```text
jhin/
  JhinMod/              # Godot 资源、图片、localization 等
  src/                  # C# 源码
    actions/
    cardpools/
    cards/
    characters/
    curtaincall/
    extensions/
    magazine/
    patches/
    potionpools/
    potions/
    powers/
    relicpools/
    relics/
    utils/
    MainFile.cs
  docs/                 # 项目文档
  knowledge/            # 实现笔记
  lib/                  # 外部依赖 DLL
  jhin.csproj           # C# / Godot .NET 项目文件
  pack.ps1              # 打包与部署脚本
```

## 开发说明

* 代码使用 C# 编写。
* 项目基于 Godot .NET SDK。
* Mod 依赖 BaseLib。
* 文案应放在 `JhinMod/localization` 中管理，避免硬编码。
* 默认卡牌图片、角色图片使用占位图。
* 修改后建议执行构建与部署流程，确认游戏内可正常加载。

## 免责声明

本项目是非商业、非官方的粉丝作品。

《League of Legends》、烬 / Jhin 及相关角色、设定、商标归 Riot Games 所有。
《Slay the Spire 2》及相关内容归其对应权利方所有。
本项目与 Riot Games、Mega Crit 或其他权利方没有官方关联。


