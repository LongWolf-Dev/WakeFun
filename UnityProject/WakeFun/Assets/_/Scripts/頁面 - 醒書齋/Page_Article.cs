using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 頁面 - 醒書齋
/// </summary>
public class Page_Article : ScrollRectList<ArticleList_Item, SO_Article>
{
    protected override void AddActionInSetDataListForLoop(ArticleList_Item item, SO_Article soData)
    {
    }
}
