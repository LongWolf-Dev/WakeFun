using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace VictorDev.EditorTool
{
    /// <summary>
    /// 隨機測試資料工具
    /// </summary>
    public abstract class RandomDataUtils
    {
        /// <summary>
        /// 取得隨機姓名
        /// </summary>
        public static string GetNameByRandom(EnumLang lang = EnumLang.zh) => CheckLang(lang, NameSet);

        /// <summary>
        /// 取得隨機EMail
        /// </summary>
        public static string GetEMailByRandom(EnumLang lang = EnumLang.zh) => CheckLang(lang, EMailSet);

        /// <summary>
        /// 取得隨機Lorem文章
        /// </summary>
        public static string GetLoremContextByRandom(EnumLang lang = EnumLang.zh) => CheckLang(lang, LoremSet);

        /// <summary>
        /// 取得隨機課程介紹
        /// <para>陣列0：課程名稱</para>
        /// <para>陣列1：課程介紹</para>
        /// </summary>
        public static string[] GetCourseByRandom(EnumLang lang = EnumLang.zh) => CheckLang(lang, CourseSet);

        /// <summary>
        /// 取得隨機地址路段
        /// <para>陣列0：縣市</para>
        /// <para>陣列1：地址</para>
        /// </summary>
        public static string[] GetAddressByRandom(EnumLang lang = EnumLang.zh) => CheckLang(lang, AddressSet);


        /// <summary>
        /// 取得從目前算起，往後n個月以內任一天DateTime
        /// </summary>
        public static DateTime GetRandomDateTimeAfterMonths(int weeks = 2)
        {
            // 從今天開始往後兩個月內隨機選擇一天
            DateTime startDate = DateTime.Today;
            DateTime endDate = startDate.AddMonths(weeks);
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan randomSpan = new TimeSpan((long)(Random.value * timeSpan.Ticks));
            DateTime randomDate = startDate + randomSpan;

            // 生成隨機的時和分
            int hour = Random.Range(8, 23); // 上午8點到晚上10點
            int minute = Random.Range(0, 6) * 10; // 分的單位為10

            // 組合日期和時間並返回
            return new DateTime(randomDate.Year, randomDate.Month, randomDate.Day, hour, minute, 0);
        }
        /// <summary>
        /// 取得從目前算起，往後n個月以內任一天DateTime字串
        /// <para>回傳格式為西元年/月/日 (星期) - 時:分</para>
        /// </summary>
        public static string GetRandomDateTimeAfterMonthsToString(int weeks = 2)
        => GetRandomDateTimeAfterMonths(weeks).ToString("yyyy/MM/dd (ddd) - HH:mm");

        #region[>>> 取值資料判斷]
        /// <summary>
        /// 依照EnumLang來隨機取得目標Dictionary裡的資料
        /// </summary>
        private static string CheckLang(EnumLang lang, Dictionary<EnumLang, List<string>> dataDict)
        {
            List<string> list = dataDict[lang];
            return list[Random.Range(0, list.Count - 1)];
        }
        private static string[] CheckLang(EnumLang lang, Dictionary<EnumLang, Dictionary<string, string>> dataDict)
        {
            Dictionary<string, string> dict = dataDict[lang];
            string key = dict.Keys.ToArray()[Random.Range(0, dict.Count - 1)];
            return new string[] { key, dict[key] };
        }
        #endregion

        // ---------------- List<string> ----------------

        #region [>>> 隨機姓名]
        private static List<string> name_ZH = new List<string>{
               "陳大明", "林小美", "黃志強", "張美玲", "王文華",
                "李靜宜", "吳建國", "劉美麗", "蔡國豪", "楊雅文",
                "吳政宏", "謝佳蓉", "許明峰", "郭怡君", "邱子豪",
                "蔡政霖", "黃婷婷", "林偉宏", "陳美玲", "王大同",
                "張雅文", "許文華", "李美惠", "吳志偉", "王怡君",
                "陳政宏", "劉靜宜", "林志強", "黃美玲", "張建宏",
                "王佳蓉", "李大明", "蔡小美", "楊志偉", "陳雅文",
                "張政霖", "吳美惠", "劉建國", "許美麗", "王政宏",
                "黃文華", "郭志強", "邱美玲", "李建宏", "蔡政宏",
                "林佳蓉", "陳小美", "張大同", "王文華", "吳怡君"
        };
        private static Dictionary<EnumLang, List<string>> NameSet = new Dictionary<EnumLang, List<string>>() {
            {EnumLang.zh, name_ZH},
            {EnumLang.en, null},
        };
        #endregion

        #region [>>> 隨機EMail]
        private static List<string> eMail_ZH = new List<string>{
             "john.doe@acme.com", "jane.smith@amazon.com", "mary.johnson@apple.com",
            "michael.jackson@tesla.com", "emma.watson@microsoft.com", "robert.smith@nike.com",
            "emily.brown@cocacola.com", "david.jones@disney.com", "susan.miller@ibm.com",
            "william.davis@samsung.com",
            "james.wilson@intel.com", "olivia.johnson@sony.com", "ethan.williams@nintendo.com",
            "oliver.brown@nike.com", "ava.jones@toyota.com", "noah.miller@mcdonalds.com",
            "isabella.davis@pepsi.com", "lucas.martin@dell.com", "mia.taylor@netflix.com",
            "mason.anderson@starbucks.com",
            "harper.thomas@facebook.com", "alexander.hernandez@spotify.com", "amelia.rodriguez@uber.com",
            "benjamin.martinez@youtube.com", "evelyn.lopez@hp.com", "william.gonzalez@linkedin.com",
            "abigail.perez@visa.com", "james.roberts@twitter.com", "sophia.hernandez@nvidia.com",
            "logan.garcia@ebay.com",
            "emma.wilson@paypal.com", "alexander.martinez@snapchat.com", "mia.lopez@lenovo.com",
            "mason.hernandez@airbnb.com", "ava.rodriguez@adidas.com", "noah.martinez@spotify.com",
            "isabella.gonzalez@pinterest.com", "liam.perez@volkswagen.com", "olivia.roberts@bmw.com",
            "michael.anderson@oracle.com",
            "sophia.thomas@cisco.com", "james.rodriguez@pepsico.com", "emma.martinez@nvidia.com",
            "william.garcia@siemens.com", "olivia.martinez@nokia.com", "lucas.garcia@huawei.com",
            "amelia.lopez@lg.com", "logan.rodriguez@kfc.com", "ava.gonzalez@adobe.com",
            "benjamin.perez@spotify.com"
        };
        private static Dictionary<EnumLang, List<string>> EMailSet = new Dictionary<EnumLang, List<string>>() {
            {EnumLang.zh, eMail_ZH},
            {EnumLang.en, null},
        };
        #endregion

        #region [>>> 隨機Lorem文章]
        private static List<string> loremContext_ZH = new List<string>{
           "我是一個熱愛音樂的人。從小學鋼琴，到後來學吉他，現在更是樂隊的一員。",
            "從小就對科學充滿好奇，常常在家裡的實驗室裡進行各種實驗。",
            "我是一個熱愛閱讀的人，從小就喜歡翻閱各種書籍，尤其喜歡歷史和文學類型。",
            "我的興趣是攝影，喜歡捕捉生活中的美好瞬間，用鏡頭記錄下來。",
            "我是一個熱愛運動的人，從小就喜歡參加各種體育活動，尤其喜歡籃球。",
            "對於美食有著無比的熱愛，喜歡嘗試各種不同的菜式，並且喜歡在家裡烹飪。",
            "喜歡旅行，喜歡去不同的地方感受不同的文化和風景。",
            "我是一個熱愛動物的人，家裡養了許多寵物，每天都和牠們玩耍。",
            "喜歡學習新的事物，從小就喜歡不斷地學習，提升自己的能力。",
            "對於藝術有著極大的熱情，喜歡欣賞各種藝術作品，並且喜歡自己創作。",
            "我是一個喜歡挑戰的人，對於困難從不退縮，努力克服各種困難。",
            "熱愛大自然，喜歡在大自然中散步，感受大自然的美妙。",
            "我是一個熱愛社會公益的人，經常參加各種志願活動，幫助需要幫助的人。",
            "對於科技產品有著極大的興趣，喜歡研究各種新的科技產品，並且喜歡自己動手做一些小發明。",
            "我是一個熱愛學習的人，對於知識有著無窮的渴望，不斷地學習新的知識。",
            "喜歡和朋友們一起聚會，聊天、玩遊戲，度過愉快的時光。",
            "對於家庭有著極大的熱愛，喜歡和家人在一起，共度美好時光。",
            "我是一個熱愛寫作的人，喜歡用文字記錄下生活中的點點滴滴，並且喜歡分享自己的故事。",
            "喜歡參加各種社交活動，樂於與不同的人交流，拓展自己的人脈。",
            "對於教育有著極大的熱情，喜歡教導孩子們學習知識，並且喜歡看到他們茁壯成長。",
            "我是一個熱愛生活的人，喜歡享受生活帶來的一切美好，並且珍惜眼前的幸福。",
            "對於音樂有著極大的熱愛，喜歡彈奏各種樂器，並且喜歡欣賞各種音樂作品。",
            "喜歡參加各種文化活動，如展覽、音樂會等，並且喜歡欣賞各種藝術作品。",
            "我是一個熱愛旅行的人，喜歡到處走走看看，感受不同地方的風土人情。",
            "對於健身有著極大的熱情，喜歡去健身房鍛煉身體，保持良好的身體狀態。",
            "喜歡參加各種社會活動，如志願服務、公益活動等，並且積極參與其中。",
            "我是一個熱愛戶外活動的人，喜歡爬山、遠足、露營等，享受大自然的美好。",
            "對於美食有著極大的熱愛，喜歡品嘗各種美食，並且喜歡嘗試不同的菜式。",
            "喜歡參加各種文化藝術活動，如音樂會、舞蹈表演等，並且喜歡欣賞各種藝術作品。",
            "我是一個熱愛運動的人，喜歡參加各種體育活動，如籃球、足球、羽毛球等。",
            "對於科技產品有著極大的興趣，喜歡研究各種新的科技產品，並且喜歡嘗試新的科技產品。",
            "喜歡參加各種社交活動，如派對、聚會等，並且喜歡和不同的人交流。",
            "我是一個熱愛學習的人，喜歡不斷地學習新的知識，提升自己的能力。",
            "對於生活有著極大的熱愛，喜歡享受生活帶來的一切美好，並且珍惜眼前的幸福。",
            "喜歡參加各種娛樂活動，如電影、音樂會等，並且喜歡享受其中帶來的快樂。",
            "我是一個熱愛閱讀的人，喜歡翻閱各種書籍，並且喜歡用書籍來豐富自己的知識。",
            "對於藝術有著極大的興趣，喜歡欣賞各種藝術作品，並且喜歡自己動手做一些小作品。",
            "喜歡參加各種社會活動，如公益活動、志願服務等，並且積極參與其中。",
            "我是一個熱愛運動的人，喜歡參加各種體育活動，如籃球、足球、排球等。",
            "對於科技產品有著極大的興趣，喜歡研究各種新的科技產品，並且喜歡嘗試其中的新功能。",
            "喜歡參加各種社交活動，如聚會、派對等，並且喜歡和不同的人交流。",
            "我是一個熱愛學習的人，喜歡不斷地學習新的知識，並且努力提升自己的能力。",
            "對於生活有著極大的熱愛，喜歡享受生活帶來的一切美好，並且積極追求自己的夢想。",
            "喜歡參加各種文化活動，如音樂會、展覽等，並且喜歡欣賞各種藝術作品。",
            "我是一個熱愛閱讀的人，喜歡翻閱各種書籍，並且喜歡用書籍來豐富自己的知識。",
            "對於科技產品有著極大的興趣，喜歡研究各種新的科技產品，並且喜歡嘗試其中的新功能。",
            "喜歡參加各種社交活動，如聚會、派對等，並且喜歡和不同的人交流。",
            "我是一個熱愛學習的人，喜歡不斷地學習新的知識，並且努力提升自己的能力。",
            "對於生活有著極大的熱愛，喜歡享受生活帶來的一切美好，並且積極追求自己的夢想。",
            "喜歡參加各種文化活動，如音樂會、展覽等，並且喜歡欣賞各種藝術作品。",
            "我是一個熱愛閱讀的人，喜歡翻閱各種書籍，並且喜歡用書籍來豐富自己的知識。",
            "對於科技產品有著極大的興趣，喜歡研究各種新的科技產品，並且喜歡嘗試其中的新功能。",
            "喜歡參加各種社交活動，如聚會、派對等，並且喜歡和不同的人交流。",
            "我是一個熱愛學習的人，喜歡不斷地學習新的知識，並且努力提升自己的能力。",
            "對於生活有著極大的熱愛，喜歡享受生活帶來的一切美好，並且積極追求自己的夢想。",
            "喜歡參加各種文化活動，如音樂會、展覽等，並且喜歡欣賞各種藝術作品。",
            "我是一個熱愛閱讀的人，喜歡翻閱各種書籍，並且喜歡用書籍來豐富自己的知識。",
            "對於科技產品有著極大的興趣，喜歡研究各種新的科技產品，並且喜歡嘗試其中的新功能。",
            "喜歡參加各種社交活動，如聚會、派對等，並且喜歡和不同的人交流。"
        };

        private static Dictionary<EnumLang, List<string>> LoremSet = new Dictionary<EnumLang, List<string>>() {
            {EnumLang.zh, loremContext_ZH},
            {EnumLang.en, null},
        };
        #endregion

        // ---------------- Dictionary<string, string> ----------------

        #region [>>> 隨機課程介紹]
        private static Dictionary<string, string> course_ZH = new Dictionary<string, string>
        {
           {"中國古典詩詞欣賞", "介紹中國古典詩詞的基本知識，教授詩詞鑒賞技巧和解讀方法。"},
            {"西方文學導讀", "簡要介紹西方文學的發展歷程，選讀西方文學名著，探討其內涵和意義。"},
            {"台灣文學史", "從台灣文學的發展脈絡出發，介紹台灣文學的主要作品和作家。"},
            {"東亞文學比較", "比較中國、日本、韓國等東亞國家的文學作品，探討其異同和影響。"},
            {"現代詩導讀", "介紹現代詩的特點和發展，選讀現代詩作品，分析其風格和意義。"},
            {"小說寫作導論", "介紹小說的基本結構和寫作技巧，指導學生進行小說創作。"},
            {"戲劇欣賞與評論", "介紹戲劇的基本元素和演出技巧，教授戲劇評論的方法和技巧。"},
            {"文學理論導讀", "介紹文學理論的主要觀點和流派，探討其對文學研究的影響。"},
            {"現代散文選讀", "選讀現代散文作品，分析其風格和主題，探討現代散文的特點。"},
            {"台灣現代詩導讀", "介紹台灣現代詩的發展歷程，選讀台灣現代詩作品，探討其特點和意義。"},
            {"古典戲曲欣賞", "介紹中國古典戲曲的基本形式和演出技巧，指導學生進行戲曲欣賞。"},
            {"文學與社會", "探討文學作品與社會現實的關係，分析文學對社會的影響和作用。"},
            {"歐洲文學史", "介紹歐洲各個時期的文學發展情況，選讀歐洲文學的代表作品。"},
            {"現代中國文學", "介紹現代中國文學的主要作品和作家，探討其對當代社會的反映。"},
            {"台灣散文導讀", "介紹台灣散文的特點和發展，選讀台灣散文作品，探討其主題和風格。"},
            {"東方美學導讀", "介紹中國、日本、韓國等東方國家的美學觀念和文學作品，比較其異同。"},
            {"現代戲劇導讀", "介紹現代戲劇的特點和發展，選讀現代戲劇作品，分析其表現形式和主題。"},
            {"文學與心理學", "探討文學作品中的心理描寫和人物塑造，分析文學對心理學的啟示。"},
            {"台灣戲曲導讀", "介紹台灣傳統戲曲的基本形式和特點，指導學生進行戲曲欣賞。"},
            {"現代散文評論", "分析現代散文作品的主題和風格，探討其對當代社會的啟示和影響。"},
            {"歐洲小說導讀", "介紹歐洲各個時期的小說作品，探討其風格和主題，分析其對文學的貢獻。"},
            {"中國古代文學", "介紹中國古代文學的發展歷程和主要作品，探討其對當代文學的影響。"},
            {"台灣詩詞導讀", "介紹台灣詩詞的特點和發展，選讀台灣詩詞作品，探討其風格和意義。"},
            {"現代戲劇評論", "分析現代戲劇作品的演出形式和主題，探討其對當代社會的啟示和影響。"},
            {"文學與宗教", "探討文學作品中的宗教意義和象徵，分析文學對宗教的反映和詮釋。"},
            {"中國古代小說", "介紹中國古代小說的特點和發展，選讀中國古代小說作品，分析其內容和意義。"},
            {"台灣小說導讀", "介紹台灣小說的發展歷程和主要作品，探討其對當代文學的影響和意義。"},
            {"現代詩詞導讀", "介紹現代詩詞的發展和特點，選讀現代詩詞作品，探討其風格和意義。"},
            {"戲曲與電影", "探討戲曲與電影之間的關係，分析電影對戲曲的改編和詮釋。"},
            {"文學與性別", "探討文學作品中的性別意識和性別形象，分析文學對性別問題的反映和詮釋。"},
            {"中國古典小說", "介紹中國古典小說的主要作品和代表作家，探討其對中國文學的影響。"},
            {"台灣散文評論", "分析台灣散文作品的主題和風格，探討其對台灣社會的啟示和影響。"},
            {"現代戲曲導讀", "介紹現代戲曲的發展和特點，選讀現代戲曲作品，探討其演出形式和內容。"},
            {"文學與音樂", "探討文學作品中的音樂意象和音樂元素，分析音樂對文學的影響和詮釋。"},
            {"中國古典詩詞", "介紹中國古典詩詞的特點和發展，選讀中國古典詩詞作品，探討其風格和意義。"},
            {"台灣現代小說", "介紹台灣現代小說的特點和發展，選讀台灣現代小說作品，分析其內容和意義。"},
            {"現代散文導讀", "介紹現代散文的特點和發展，選讀現代散文作品，分析其風格和意義。"},
            {"戲曲與舞蹈", "探討戲曲與舞蹈之間的關係，分析舞蹈對戲曲的詮釋和演繹。"},
            {"文學與繪畫", "探討文學作品中的繪畫意象和繪畫元素，分析繪畫對文學的影響和詮釋。"}

        };

        private static Dictionary<EnumLang, Dictionary<string, string>> CourseSet = new Dictionary<EnumLang, Dictionary<string, string>>() {
            {EnumLang.zh, course_ZH},
            {EnumLang.en, null},
        };
        #endregion

        #region [>>> 隨機縣市地址]
        private static Dictionary<string, string> address_ZH = new Dictionary<string, string>
        {
            {"台北市中山區", "南京東路一段1號"},
            {"台北市信義區", "忠孝東路五段2號"},
            {"台北市大安區", "仁愛路四段3號"},
            {"台北市中正區", "重慶南路一段4號"},
            {"台北市士林區", "中山北路七段5號"},
            {"新北市板橋區", "文化路一段6號"},
            {"新北市新店區", "北新路三段7號"},
            {"新北市永和區", "永和路一段8號"},
            {"新北市中和區", "中和路一段9號"},
            {"新北市深坑區", "深坑路一段10號"},
            {"桃園市桃園區", "中正路11號"},
            {"桃園市中壢區", "中央西路12號"},
            {"桃園市八德區", "介壽路13號"},
            {"桃園市大溪區", "仁愛路14號"},
            {"桃園市蘆竹區", "南山路15號"},
            {"新竹市東區", "光復路二段16號"},
            {"新竹市北區", "中正路17號"},
            {"新竹市香山區", "牛埔東路18號"},
            {"苗栗縣苗栗市", "中正路19號"},
            {"苗栗縣頭份市", "中華路20號"},
            {"苗栗縣竹南鎮", "中山路21號"},
            {"苗栗縣後龍鎮", "中山路22號"},
            {"台中市西屯區", "文心路一段23號"},
            {"台中市北屯區", "文心路四段24號"},
            {"台中市南屯區", "文心路二段25號"},
            {"台中市中區", "中山路26號"},
            {"台中市北區", "三民路三段27號"},
            {"台南市中西區", "民族路28號"},
            {"台南市東區", "中華東路一段29號"},
            {"台南市南區", "中華南路30號"},
            {"台南市北區", "成功北路31號"},
            {"高雄市前鎮區", "中山二路32號"},
            {"高雄市苓雅區", "成功一路33號"},
            {"高雄市鳳山區", "文化路34號"},
            {"高雄市三民區", "建國三路35號"},
            {"屏東縣屏東市", "自由路36號"},
            {"屏東縣潮州鎮", "中山路37號"},
            {"屏東縣東港鎮", "中山路38號"},
            {"屏東縣恆春鎮", "民生路39號"},
            {"宜蘭縣宜蘭市", "中山路40號"},
            {"宜蘭縣羅東鎮", "中正路41號"},
            {"宜蘭縣礁溪鄉", "中山路42號"},
            {"宜蘭縣冬山鄉", "中山路43號"},
            {"花蓮縣花蓮市", "中山路44號"},
            {"花蓮縣鳳林鎮", "中正路45號"},
            {"花蓮縣壽豐鄉", "中山路46號"},
            {"花蓮縣玉里鎮", "中正路47號"},
            {"台東縣台東市", "中正路48號"},
            {"台東縣成功鎮", "中山路49號"},
            {"台東縣關山鎮", "中正路50號"},
            {"台東縣池上鄉", "中山路51號"}
        };

        private static Dictionary<EnumLang, Dictionary<string, string>> AddressSet = new Dictionary<EnumLang, Dictionary<string, string>>() {
            {EnumLang.zh, address_ZH},
            {EnumLang.en, null},
        };
        #endregion
    }

    /// <summary>
    /// 語系Enum
    /// </summary>
    public enum EnumLang
    {
        zh, en
    }
}
