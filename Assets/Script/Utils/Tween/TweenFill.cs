#region

using UnityEngine;

#endregion

[AddComponentMenu("NGUI/Tween/Tween Fill")]
public class TweenFill : UITweener {

    [Range(0f, 1f)] public float from = 1f;
    [Range(0f, 1f)] public float to = 1f;
    [Range(0f, 1f)] public float initValue;

    private Transform mTrans;
    private UISprite mSprite;
    private UIPanel mPanel;

    public float fill {
        get {
            if (mSprite != null) {
                return mSprite.fillAmount;
            }
            return 0f;
        }
        set {
            if (mSprite != null) {
                mSprite.fillAmount = value;
            }
        }
    }

    private void Awake() {
        mPanel = GetComponent<UIPanel>();
        if (mPanel == null) {
            mSprite = GetComponentInChildren<UISprite>();
        }
    }

    protected override void OnUpdate(float factor, bool isFinished) {
        if (!isFinished) {
            fill = Mathf.Lerp(from, to, factor);
        }
        else {
            fill = initValue;
        }
    }

    public static TweenFill Begin(GameObject go, float duration, float fillAmount, float initValue) {
        TweenFill comp = Begin<TweenFill>(go, duration);
        comp.from = comp.fill;
        comp.initValue = initValue;
        comp.to = fillAmount;

        if (duration <= 0f) {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

}