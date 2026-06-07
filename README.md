# 子午計畫主題節奏音遊

> **聲明：本專案為學生期末作業之 Demo 作品，以子午計畫旗下虛擬偶像原創曲為主題製作之二創作品，純屬學術用途，不含任何商業營利行為。**
> **Disclaimer: This project is a fan-made student final assignment based on original songs by the VTuber studio "子午計畫". It is created for academic purposes only. No commercial use or profit is intended.**

---

## 專案簡介

這是一款以 **子午計畫**（VT 工作室）旗下虛擬偶像原創曲為主題製作的二創節奏手機遊戲，適用於 Android 手機與平板觸控設備。

玩家扮演**頻道管理員**，依照音樂節拍點擊下落的 Node，協助 Vtuber 完成歌唱演出。除核心音遊玩法外，遊戲還包含角色養成、SC 資金系統、扭蛋收集，以及一個內建的樂譜編輯器供玩家自製譜面。

- 5 人團隊 · 歷時 4 週 · Demo 完成於 2025/01/06
- 個人負責：核心節奏判定系統、角色解鎖機制、譜面編輯器實作

---

## 功能特色

### 🎵 音樂遊玩
- 收錄 **15 首子午計畫原創歌曲**，涵蓋旗下各角色曲目
- 每首歌提供 **三種難度**：Easy / Normal / Hard
- 判定分為三段：**NEON**（最準）/ **GLOW**（微差）/ **VOID**（未命中）
- 演出結果評級：**LIVE FINISH** / **FULL COMBO** / **MARVELOUS NEON**
- 遊玩時同步播放 **MV 影片**（可於設定中關閉）
- 音符類型：單點（Tap）、滑動（Flick）、長按（Hold）

### 🎭 角色與養成系統
- 以子午計畫旗下虛擬偶像為主角，搭配劇情系統展開互動
- 扭蛋（Gacha）機制抽取角色卡片
- 演出結算 **SC 資金**後可升級配備、開拓收益平台，解鎖新角色

### 🛠️ 樂譜編輯器
- 內建樂譜製作工具，支援自訂 BPM 與音符排列
- 玩家可自製譜面並匯出分享

### ⚙️ 其他設定
- BGM / SFX 音量獨立調整
- 音符下落速度調整
- MV開關

---

## 歌曲列表

| 角色 | 歌曲 | Easy | Normal | Hard |
|------|------|:----:|:------:|:----:|
| 浠 Mizuki | 未知未踏 アルスハイル | — | — | — |
| | Day by Day | — | — | — |
| | 申戀題 | — | — | — |
| | 桜華残響 | 5 | 8 | 10 |
| | Siɹən | — | — | 11 |
| | 潜水花 | — | — | 9 |
| 汐 Seki | 信號 Signal | — | — | — |
| | 夏夢 SummerDream | — | — | — |
| 響 Hibiki | 響念 Missing | — | — | — |
| 霓 NEO(n) | 霓光 NeonLight | 3 | 5 | 7 |
| | 戀夏 Summer | 4 | 6 | 8 |
| 澪 Rei | 漫夜 Sleepless | 2 | — | — |
| 煌 Kirali | Liar 謊癮 | 2 | — | 6 |
| 扉暮 IANVS | 暮光 Gloaming Light | — | — | 8 |
| 玥 Itsuki | 情緒廢物 EmotionalWaste | — | — | — |
| 朔 Sakuro | 迷醉 Sangria | — | — | — |

> 數字為難度標示，— 表示期末前尚未完成製作。

---

## 技術架構

| 項目 | 內容 |
|------|------|
| 引擎 | Unity 2022 |
| 語言 | C# |
| 平台 | Android |
| 版本 | 0.1.0 |
| 渲染管線 | Universal Render Pipeline (URP) |

### 主要場景
- **MainMenu** — 主選單、選歌、角色養成、設定
- **inGameScene** — 核心遊玩場景（音符下落、計分、MV 播放）
- **MusicSheetSetupScene** — 樂譜編輯器

### 程式架構
- **Singleton** 模式管理跨場景全域狀態（`GameSceneManager`）
- **ScriptableObject** 儲存歌曲、角色、音符等資料設定
- 音效、UI、輸入、計分各自獨立管理，職責分離清晰

---

## 版權與聲明

- 本專案為**學生期末作業 Demo**，製作目的為學術學習與展示，**不作任何商業用途**。
- 遊戲內所使用之音樂、影像素材版權歸 **子午計畫** 及各原著作權人所有，本專案不主張任何所有權。
- 本作品為粉絲二創，與子午計畫官方無任何隸屬關係。
- 若有任何版權疑慮，請聯繫作者。

---

*Meridian — 子午計畫主題節奏音遊 · Student Final Project Demo, 2025*
