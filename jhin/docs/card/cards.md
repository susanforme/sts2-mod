# 已实现卡牌一览

当前根据 `src/cards/*.cs` 中的具体卡牌类整理，不包含 `AbstractJhinCard`、`AbstractShootCard`、`JhinKeywords` 等非卡牌定义文件。

| ID | 中文名 | 英文名 | 类型 | 费用 | 稀有度 | 目标 | 文件 | 当前效果摘要 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `JHIN-COMMON_SHOT` | 普通射击 | Common Shot | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/CommonShot.cs` | 消耗 1 层子弹造成基础伤害；华彩时追加一段固定伤害。 |
| `JHIN-PERFECT_SHOT` | 精准一枪 | Perfect Shot | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/PerfectShot.cs` | 射击造成更高单体伤害；华彩时回能，升级后还能抽牌。 |
| `JHIN-WHISPER_BURST` | 低语点射 | Whisper Burst | 攻击 / 射击 | `0` | 普通 | 单体敌人 | `src/cards/WhisperBurst.cs` | 0 费射击；华彩时追加高额补伤。 |
| `JHIN-GRACEFUL_FOOTWORK` | 优雅走位 | Graceful Footwork | 技能 | `1` | 普通 | 自身 | `src/cards/GracefulFootwork.cs` | 获得格挡；若本回合已射击则再获得额外格挡。 |
| `JHIN-AIM_SHOT` | 瞄准射击 | Aim Shot | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/AimShot.cs` | 先进行射击伤害，再给予目标标记；升级后改为 2 层。 |
| `JHIN-PIERCING_ROUND` | 穿透弹 | Piercing Round | 攻击 / 射击 | `1` | 普通 | 全体敌人 | `src/cards/PiercingRound.cs` | 消耗 1 发子弹对所有敌人射击；华彩时全体追加固定伤害。 |
| `JHIN-DANCING_GRENADE` | 曼舞手雷 | Dancing Grenade | 攻击 | `1` | 普通 | 单体敌人 | `src/cards/DancingGrenade.cs` | 非射击伤害；若击杀目标，会随机弹跳到另一名敌人并提高伤害。 |
| `JHIN-FINISH_OFF` | 收尾 | Finish Off | 攻击 | `1` | 普通 | 单体敌人 | `src/cards/FinishOff.cs` | 非射击终结牌；对半血以下目标造成额外伤害。 |
| `JHIN-SET_THE_STAGE` | 布置舞台 | Set the Stage | 技能 | `1` | 普通 | 自身 | `src/cards/SetTheStage.cs` | 获得格挡，并给随机敌人施加标记；升级后改为 2 层。 |
| `JHIN-CALCULATED_PLAN` | 精密计算 | Calculated Plan | 技能 | `1` | 普通 | 自身 | `src/cards/CalculatedPlan.cs` | 抽 2 张牌；在弹匣接近打空时返还 1 点能量。 |
| `JHIN-BACKSTEP` | 后撤步 | Backstep | 技能 | `0` | 普通 | 自身 | `src/cards/Backstep.cs` | 获得少量格挡；若本回合已射击则抽 1 张牌。 |
| `JHIN-SET_TRAP` | 设下陷阱 | Set a Trap | 技能 | `1` | 普通 | 单体敌人 | `src/cards/SetTrap.cs` | 获得格挡，并给予目标 1 层莲花陷阱。 |
| `JHIN-CALM_RELOAD` | 冷静装填 | Calm Reload | 技能 | `1` | 普通 | 自身 | `src/cards/CalmReload.cs` | 装填到满弹匣并获得格挡。 |
| `JHIN-LONG_RANGE_SNIPE` | 远距狙击 | Long-Range Snipe | 攻击 / 射击 | `2` | 罕见 | 单体敌人 | `src/cards/LongRangeSnipe.cs` | 高额单体射击；按标记层数追加伤害，华彩时再追加一段固定补伤。 |
| `JHIN-FOURTH_ACT` | 第四乐章 | Fourth Act | 攻击 / 射击 | `2` | 罕见 | 单体敌人 | `src/cards/FourthAct.cs` | 只能在最后一发子弹时打出；高额第四枪并施加易伤。 |
| `JHIN-GRAND_STAGE` | 盛大布景 | Grand Stage | 技能 | `2` | 罕见 | 自身 | `src/cards/GrandStage.cs` | 获得较高格挡，并给予所有敌人 1 层莲花陷阱。 |
| `JHIN-DEADLY_FLOURISH` | 致命华彩 | Deadly Flourish | 攻击 / 射击 | `1` | 罕见 | 单体敌人 | `src/cards/DeadlyFlourish.cs` | 造成射击伤害；若目标命中前带有标记，则施加易伤，华彩时层数更高。 |
| `JHIN-ART_SLICE` | 艺术切割 | Art Slice | 攻击 | `1` | 普通 | 单体敌人 | `src/cards/ArtSlice.cs` | 造成伤害；若目标带有虚弱，额外造成伤害。 |
| `JHIN-SIDESTEP_FIRE` | 侧身开火 | Sidestep Fire | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/SidestepFire.cs` | 射击造成伤害并获得格挡。 |
| `JHIN-QUICK_DRAW` | 快速拔枪 | Quick Draw | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/QuickDraw.cs` | 射击造成伤害并抽 1 张牌。 |
| `JHIN-REHEARSAL_SHOT` | 试演一枪 | Rehearsal Shot | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/RehearsalShot.cs` | 射击造成伤害；若不是华彩则抽 1 张牌。 |
| `JHIN-GUNFLAME_GRAZE` | 枪焰擦伤 | Gunflame Graze | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/GunflameGraze.cs` | 射击造成伤害并施加虚弱；华彩时施加更多层。 |
| `JHIN-INTERMISSION_KILL` | 幕间点杀 | Intermission Kill | 攻击 | `1` | 普通 | 单体敌人 | `src/cards/IntermissionKill.cs` | 造成伤害；若目标带有标记，获得格挡。 |
| `JHIN-ENCORE_BULLET` | 余兴弹 | Encore Bullet | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/EncoreBullet.cs` | 射击造成伤害；若本回合已使用过技能牌，额外造成伤害。 |
| `JHIN-WHISPERED_THREAT` | 低声威胁 | Whispered Threat | 攻击 | `1` | 普通 | 单体敌人 | `src/cards/WhisperedThreat.cs` | 造成伤害并给予标记；消耗。 |
| `JHIN-OBSERVE_WEAKNESS` | 观察弱点 | Observe Weakness | 技能 | `1` | 普通 | 单体敌人 | `src/cards/ObserveWeakness.cs` | 给予目标标记；若目标生命低于 50%，抽 1 张牌。 |
| `JHIN-PERFECT_STANCE` | 完美姿态 | Perfect Stance | 技能 | `1` | 普通 | 自身 | `src/cards/PerfectStance.cs` | 获得格挡；若子弹为 4 则额外获得大量格挡。 |
| `JHIN-AWAIT_APPLAUSE` | 等待掌声 | Await Applause | 技能 | `1` | 普通 | 自身 | `src/cards/AwaitApplause.cs` | 获得格挡；下个回合开始时获得 1 点能量。 |
| `JHIN-COUNT_BEATS` | 数拍 | Count Beats | 技能 | `1` | 普通 | 自身 | `src/cards/CountBeats.cs` | 抽牌；若子弹为 1 额外抽 1 张牌；消耗。 |
| `JHIN-COMPOSED` | 从容不迫 | Composed | 能力 | `1` | 普通 | 自身 | `src/cards/Composed.cs` | 本场战斗中首次触发华彩时，抽 2 张牌。 |
| `JHIN-MUZZLE_RHYTHM` | 枪口节奏 | Muzzle Rhythm | 能力 | `1` | 普通 | 自身 | `src/cards/MuzzleRhythm.cs` | 每打出 4 张攻击牌，获得 1 点力量（升级后还获得敏捷）。 |
| `JHIN-AUDIENCE_SEATED` | 观众入席 | Audience Seated | 能力 | `1` | 普通 | 自身 | `src/cards/AudienceSeated.cs` | 所有敌人获得 1 层标记。 |
| `JHIN-SHOW_PLAN` | 演出计划 | Show Plan | 能力 | `1` | 普通 | 自身 | `src/cards/ShowPlan.cs` | 每当装填时，获得格挡。 |
| `JHIN-BLOODY_STAGE` | 血色舞台 | Bloody Stage | 能力 | `1` | 普通 | 自身 | `src/cards/BloodyStage.cs` | 当敌人首次生命降到 50% 以下时，给予标记。 |
| `JHIN-INTERMISSION` | 幕间休息 | Intermission | 能力 | `1` | 普通 | 自身 | `src/cards/Intermission.cs` | 每回合首次使用非射击攻击牌时，额外造成伤害。 |
| `JHIN-GUN_MAINTENANCE` | 枪械保养 | Gun Maintenance | 能力 | `1` | 普通 | 自身 | `src/cards/GunMaintenance.cs` | 每当装填时，抽 1 张牌（升级后还获得格挡）。 |
| `JHIN-ACTORS_INSTINCT` | 演员本能 | Actor's Instinct | 能力 | `1` | 普通 | 自身 | `src/cards/ActorsInstinct.cs` | 若本回合未触发华彩，下回合获得力量（升级后还获得敏捷）。 |
| `JHIN-STAGE_CONTROL` | 控场艺术 | Stage Control | 能力 | `1` | 普通 | 自身 | `src/cards/StageControl.cs` | 每当给敌人施加虚弱时，同时给予标记。 |
| `JHIN-RELOAD` | 装填 | Reload | 技能 | `0` | 普通 | 自身 | `src/cards/Reload.cs` | 将当前子弹装填至 4，并抽牌；本回合禁用华彩且会消耗。 |
| `JHIN-FLOURISH_TEMPO` | 华彩节奏 | Flourish Tempo | 能力 | `2` | 罕见 | 自身 | `src/cards/FlourishTempo.cs` | 持续强化华彩收益；每次华彩回能，升级后额外抽牌。 |
| `JHIN-CURTAIN_CALL` | 谢幕 | Curtain Call | 攻击 / 谢幕 | `2` | 稀有 | 随机敌人 | `src/cards/CurtainCall.cs` | 只能在子弹为 0 时使用；随机攻击 4 次，每段根据目标当前生命损失独立提高伤害，使用后消耗。 |

## 备注

| 项目 | 说明 |
| --- | --- |
| 射击牌统一入口 | 当前 MVP 射击牌统一继承 `AbstractShootCard`，包含 `普通射击`、`精准一枪`、`低语点射`、`瞄准射击`、`穿透弹`、`侧身开火`、`快速拔枪`、`试演一枪`、`枪焰擦伤`、`余兴弹`、`远距狙击`、`第四乐章`、`致命华彩`。 |
| 标记联动 | 当前已接入标记系统的牌为 `瞄准射击`、`布置舞台`、`远距狙击`、`致命华彩`、`观察弱点`、`低声威胁`、`幕间点杀`、`观众入席`、`血色舞台`、`控场艺术`。 |
| 莲花陷阱联动 | 当前已接入莲花陷阱系统的牌为 `设下陷阱`、`盛大布景`。 |
| 华彩联动 | `普通射击`、`精准一枪`、`低语点射`、`穿透弹`、`远距狙击`、`第四乐章`、`致命华彩`、`枪焰擦伤` 和能力牌 `华彩节奏`、`从容不迫`、`演员本能` 都会响应统一华彩判定。 |
| 装填联动 | 能力牌 `演出计划`、`枪械保养` 通过 `ReloadEventBus` 响应装填事件。 |
| 谢幕联动 | 当前 `谢幕` 使用独立的谢幕条件与四段随机攻击 Action，不消耗子弹，也不触发普通射击华彩判定。 |
| 占位图规则 | 当前卡牌默认使用 `JhinMod/Images/card_placeholder.png`。 |
