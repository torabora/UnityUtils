using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class GameObjectUtils {
    public static GameObject Instantiate(GameObject prefab, Transform parent) {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        go.transform.parent = parent;
        go.transform.localScale = new Vector3(1, 1, 1);
        go.transform.localPosition = new Vector3(0, 0, 0);
        return go;
    }

    public static GameObject Instantiate(String path, Transform parent) {
        return Instantiate(Resources.Load(path) as GameObject, parent);
    }
}

public static class TransformUtils {
    public static void SetScaleX(this Transform transform, float scale) {
        var localScale = transform.localScale;
        localScale.x = scale;
        transform.localScale = localScale;
    }

    public static void SetScaleY(this Transform transform, float scale) {
        var localScale = transform.localScale;
        localScale.y = scale;
        transform.localScale = localScale;
    }
}

public static class TableUtils {
    public static void ClearTable(this UITable table) {
        int childs = table.transform.childCount;
        for (int i = childs - 1; i >= 0; i--) {
            var child = table.transform.GetChild(i);
            if (child == null) continue;
            GameObject.DestroyImmediate(child.gameObject);
        }
    }
}

public class SpriteUtils {
    public static void SetColorRecursive(GameObject parent, Color color) {
        SpriteRenderer[] sprites = parent.transform.GetComponentsInChildren<SpriteRenderer>(includeInactive:true);
        foreach (SpriteRenderer sprite in sprites) {
            color.a = sprite.color.a;
            sprite.color = color;
        }
    }

    public static void SetAlphaRecursive(GameObject parent, float alpha) {
        SpriteRenderer[] sprites = parent.transform.GetComponentsInChildren<SpriteRenderer>(includeInactive:true);
        foreach (SpriteRenderer sprite in sprites) {
            Color color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }
    }
}

public static class LabelUtils {

    public static void TweenDigitTo(this UILabel label, int toValue, float duration = 0.3f) {
        TweenLabel tweenLabel = label.gameObject.AddMissingComponent<TweenLabel>();

        int outText;
        bool tryParce = int.TryParse(label.text, out outText);

        tweenLabel.from = tryParce ? outText : 0;
        tweenLabel.to = toValue;
        tweenLabel.duration = duration;
        tweenLabel.tweenFactor = 0;
        tweenLabel.enabled = true;
    }

}
