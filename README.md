# Meridian — Rhythm Music Game

> **聲明：本專案為學生期末作業作品，純屬學術用途，不作任何商業營利使用。**
> **Disclaimer: This project is a student final assignment created for academic purposes only. No commercial use or profit is intended.**

---

## 專案簡介

**Meridian** 是一款以 Unity 開發的手機節奏音樂遊戲，玩家需在音樂節拍中點擊、滑動與長按下落的音符，挑戰各種難度的樂譜。遊戲除核心的音遊玩法外，還包含角色養成、扭蛋收集系統，以及一個內建的樂譜編輯器，讓玩家得以自製譜面。

---

## 功能特色

### 🎵 音樂遊玩
- 支援 **16 首歌曲**，風格多元（流行、電音、日系等）
- 每首歌提供 **三種難度**：Easy / Normal / Hard
- 遊玩時同步播放 **MV 影片**（可於設定中關閉）
- 音符類型包含：**單點（Tap）**、**滑動（Flick）**、**長按（Hold）**

### 🎭 角色系統
- 扭蛋（Gacha）機制抽取角色卡片
- 角色養成與技能卡升級
- 玩家經驗值與星數成長系統

### 🛠️ 樂譜編輯器
- 內建樂譜製作工具（Music Sheet Setup Scene）
- 支援自訂 BPM、節拍分割與音符排列
- 即時預覽與播放功能

### ⚙️ 其他設定
- BGM / SFX 音量獨立調整
- 下落速度調整
- 幀率管理（FPS Manager）

---

## 歌曲列表

| # | 歌曲名稱 |
|---|----------|
| 1 | Day by Day |
| 2 | Liar 謊癮 |
| 3 | Siɹən |
| 4 | 信號 Signal |
| 5 | 夏夢 SummerDream |
| 6 | 情緒廢物 EmotionalWaste |
| 7 | 戀夏 Summer |
| 8 | 暮光 Gloaming Light |
| 9 | 未知未踏 アルスハイル |
| 10 | 桜華残響 |
| 11 | 漫夜 Sleepless |
| 12 | 潜水花 |
| 13 | 申戀題 |
| 14 | 迷醉 Sangria |
| 15 | 霓光 NeonLight |
| 16 | 響念 Missing |

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
- 採用 **Singleton** 模式管理跨場景的全域狀態（`GameSceneManager`）
- 以 **ScriptableObject** 儲存歌曲、角色、音符等資料設定
- 音效、UI、輸入、計分各自獨立管理，職責分離清晰

---

## 專案結構

```
Assets/
├── script/          # 所有 C# 腳本（核心、主選單、遊戲內、編輯器）
├── Scenes/          # Unity 場景檔案
├── Media/           # 歌曲音樂、影片、封面圖片
├── Ui/              # UI 元件與 Prefab
├── Character/       # 角色圖片資源
├── Animations/      # 動畫資源
├── VFX/             # 視覺特效
├── Materials/       # 材質
├── Font/            # 字型
├── Settings/        # URP 渲染設定
└── Resources/       # 執行時動態載入資源
```

---

## 版權與聲明

- 本專案為**學生期末作業**，製作目的為學術學習與展示，**不作任何商業用途**。
- 遊戲內所使用之音樂、影像素材版權歸原著作權人所有，本專案不主張任何所有權。
- 若有任何版權疑慮，請聯繫作者。

---

*Meridian Rhythm Music Game — Student Final Project, 2026*
