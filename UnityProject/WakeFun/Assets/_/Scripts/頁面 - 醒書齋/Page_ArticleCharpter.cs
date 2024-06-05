using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  頁面：醒書齋 - 章節列表
/// </summary>
public class Page_ArticleCharpter : ScrollRectList<ArticleCharpterList_Item, SO_ArticleCharpter>
{
    protected override void AddActionInSetDataListForLoop(ArticleCharpterList_Item item, SO_ArticleCharpter soData)
    {
    }
}
