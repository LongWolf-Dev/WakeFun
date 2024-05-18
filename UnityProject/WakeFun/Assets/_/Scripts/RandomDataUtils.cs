﻿using System.Collections.Generic;
using System.Linq;

namespace VictorDev.WakeFun
{
    /// <summary>
    /// 隨機測試資料工具
    /// </summary>
    public abstract class RandomDataUtils
    {
        private static string[] GetRandomFromDictionary(Dictionary<string, string> dictionary)
        {
            // 使用 UnityEngine.Random 來生成隨機索引
            int index = UnityEngine.Random.Range(0, dictionary.Count);
            KeyValuePair<string, string> randomAchievement = dictionary.ElementAt(index);
            return new string[2] { randomAchievement.Key, randomAchievement.Value };
        }

        #region [>>> 隨機成就]
        /// <summary>
        /// 取得隨機成就名稱與其敘述
        /// </summary>
        public static string[] GetAchievementByRandom() => GetRandomFromDictionary(achievementsList);

        private static Dictionary<string, string> achievementsList = new Dictionary<string, string>
        {
            {"靜心初學者", "開始第一次冥想練習。"},
            {"每日練習者", "連續七天每天冥想。"},
            {"專注專家", "完成一次30分鐘以上的冥想。"},
            {"深度放鬆", "在冥想中達到完全放鬆的狀態。"},
            {"心靈平靜", "經歷一次完全無雜念的冥想。"},
            {"身心合一", "在冥想中感受到身體與心靈的連接。"},
            {"清晰見解", "在冥想中獲得一個重要的見解或啟示。"},
            {"持續專注", "能夠在冥想中保持專注超過20分鐘。"},
            {"情緒平衡", "通過冥想成功調節情緒波動。"},
            {"呼吸掌握", "能夠在冥想中完全控制呼吸節奏。"},
            {"光明心境", "在冥想中體驗到內心的光明。"},
            {"慈悲心", "在冥想中感受到對所有眾生的慈悲。"},
            {"愛與善意", "在冥想中體驗到無條件的愛和善意。"},
            {"正念飲食", "在進餐時保持完全的正念。"},
            {"正念行走", "在行走時保持完全的正念。"},
            {"冥想導師", "帶領他人進行一次冥想練習。"},
            {"超越自我", "在冥想中體驗到自我的消融。"},
            {"心靈洞察", "獲得深層次的心靈洞察。"},
            {"內在寧靜", "在冥想中感受到內在的完全寧靜。"},
            {"圓滿靜心", "完成100次冥想練習。"},
            {"清晨靜心", "每天清晨進行冥想連續30天。"},
            {"晚間靜心", "每天晚上進行冥想連續30天。"},
            {"無我之境", "在冥想中達到無我的境界。"},
            {"能量流動", "感受到體內能量的流動。"},
            {"內心光芒", "在冥想中體驗到內心的光芒。"},
            {"愛與光", "在冥想中感受到愛與光的融合。"},
            {"心靈花園", "在冥想中構建並探索自己的心靈花園。"},
            {"聲音冥想", "通過聆聽自然聲音進行一次成功的冥想。"},
            {"音樂冥想", "通過聆聽音樂進行一次成功的冥想。"},
            {"心跳同步", "在冥想中與自己的心跳同步。"},
            {"瑜伽冥想", "結合瑜伽動作進行一次冥想。"},
            {"曼陀羅冥想", "使用曼陀羅圖形進行一次冥想。"},
            {"自然冥想", "在大自然中進行一次成功的冥想。"},
            {"香薰冥想", "結合香薰進行一次冥想。"},
            {"蠟燭冥想", "注視蠟燭火焰進行一次冥想。"},
            {"月光冥想", "在月光下進行一次冥想。"},
            {"日出冥想", "在日出時分進行一次冥想。"},
            {"正念冥想", "進行一次完全的正念冥想。"},
            {"直覺增強", "通過冥想增強直覺能力。"},
            {"無痛冥想", "在冥想中體驗到無痛的狀態。"},
            {"過去回顧", "在冥想中回顧並釋放過去的創傷。"},
            {"未來願景", "在冥想中構建對未來的願景。"},
            {"冥想日記", "記錄冥想日記連續30天。"},
            {"感恩冥想", "在冥想中感受到深刻的感恩之情。"},
            {"光與暗", "在冥想中探索內心的光明與黑暗。"},
            {"靜默觀察", "在冥想中靜默觀察自己的思想。"},
            {"平和內心", "通過冥想達到內心的平和狀態。"},
            {"集體冥想", "參加一次集體冥想活動。"},
            {"冥想長者", "完成1000次冥想練習。"},
            {"心靈旅行", "在冥想中進行一次心靈的旅行。"}
        };
        #endregion
    }
}