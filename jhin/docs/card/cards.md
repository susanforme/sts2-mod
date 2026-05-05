# 已实现卡牌一览

当前根据 `src/cards/*.cs` 中的具体卡牌类整理，不包含 `AbstractJhinCard`、`AbstractShootCard`、`JhinKeywords` 等非卡牌定义文件。

| ID | 中文名 | 英文名 | 类型 | 费用 | 稀有度 | 目标 | 文件 | 当前效果摘要 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `JHIN-COMMON_SHOT` | 普通射击 | Common Shot | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/CommonShot.cs` | 消耗 1 层子弹，造成基础伤害；命中时会走统一射击流程，可消耗目标标记。 |
| `JHIN-GRACEFUL_FOOTWORK` | 优雅走位 | Graceful Footwork | 技能 | `1` | 普通 | 自身 | `src/cards/GracefulFootwork.cs` | 获得格挡。 |
| `JHIN-AIM_SHOT` | 瞄准射击 | Aim Shot | 攻击 / 射击 | `1` | 普通 | 单体敌人 | `src/cards/AimShot.cs` | 先进行射击伤害，再给予目标 1 层标记；自己施加的标记不会被本次命中消耗。 |
| `JHIN-SET_THE_STAGE` | 布置舞台 | Set the Stage | 技能 | `1` | 普通 | 自身 | `src/cards/SetTheStage.cs` | 获得格挡，并给随机敌人 1 层标记。 |
| `JHIN-SET_TRAP` | 设下陷阱 | Set a Trap | 技能 | `1` | 普通 | 单体敌人 | `src/cards/SetTrap.cs` | 获得格挡，并给予目标 1 层莲花陷阱。 |
| `JHIN-LONG_RANGE_SNIPE` | 远距狙击 | Long-Range Snipe | 攻击 / 射击 | `2` | 罕见 | 单体敌人 | `src/cards/LongRangeSnipe.cs` | 造成高额单体伤害；除统一标记规则增伤外，额外按目标标记层数每层再加 3 点伤害。 |
| `JHIN-GRAND_STAGE` | 盛大布景 | Grand Stage | 技能 | `2` | 罕见 | 自身 | `src/cards/GrandStage.cs` | 获得格挡，并给予所有敌人 1 层莲花陷阱。 |
| `JHIN-DEADLY_FLOURISH` | 致命华彩 | Deadly Flourish | 攻击 / 射击 | `2` | 稀有 | 单体敌人 | `src/cards/DeadlyFlourish.cs` | 造成射击伤害；若目标在命中前带有标记，则施加易伤，华彩时易伤层数更高。 |
| `JHIN-RELOAD` | 装填 | Reload | 技能 | `0` | 罕见 | 自身 | `src/cards/Reload.cs` | 将当前子弹装填至 4。 |
| `JHIN-CURTAIN_CALL` | 谢幕 | Curtain Call | 攻击 / 谢幕 | `2` | 稀有 | 随机敌人 | `src/cards/CurtainCall.cs` | 只能在子弹为 0 时使用；随机攻击 4 次，每段根据目标当前生命损失独立提高伤害，使用后消耗。 |

## 备注

| 项目 | 说明 |
| --- | --- |
| 射击牌统一入口 | 当前 `普通射击`、`瞄准射击`、`远距狙击`、`致命华彩` 都继承 `AbstractShootCard`。 |
| 标记联动 | 当前已接入阶段 4 标记系统的牌为 `瞄准射击`、`布置舞台`、`远距狙击`、`致命华彩`。 |
| 莲花陷阱联动 | 当前已接入阶段 5 莲花陷阱系统的牌为 `设下陷阱`、`盛大布景`。 |
| 谢幕联动 | 当前 `谢幕` 使用独立的谢幕条件与四段随机攻击 Action，不消耗子弹，也不触发普通射击华彩判定。 |
| 占位图规则 | 当前卡牌默认使用 `JhinMod/Images/card_placeholder.png`。 |
