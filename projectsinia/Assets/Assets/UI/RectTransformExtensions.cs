using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
{
    public static void SetWidth(this RectTransform t, float width ){
        t.sizeDelta = new Vector2(width, t.rect.height);
    }
}
