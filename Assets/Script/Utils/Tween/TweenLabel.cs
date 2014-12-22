#region

using UnityEngine;

#endregion

/// <summary>
///     Tween the object's position.
/// </summary>
[AddComponentMenu("NGUI/Tween/Position")]
public class TweenLabel : UITweener {

    public int from = 0;
    public int to = 100;

    private UILabel mLabel;

    public UILabel cachedLabel {
        get {
            if (mLabel == null) {
                mLabel = gameObject.GetComponent<UILabel>();
            }
            return mLabel;
        }
    }

    //public Vector3 position { get { return cachedSlider.localPosition; } set { cachedSlider.localPosition = value; } }

    protected override void OnUpdate(float factor, bool isFinished) {
        cachedLabel.text = ((int) (from*(1f - factor) + to*factor)).ToString();
        //.sliderValue = from * (1f - factor) + to * factor;
    }

    /*void OnDisable() {
        if (tweenFactor == 1) {
            Destroy(this);
        }
    }*/

    /// <summary>
    ///     Start the tweening operation.
    /// </summary>
    public static TweenPosition Begin(GameObject go, float duration, Vector3 pos) {
        TweenPosition comp = Begin<TweenPosition>(go, duration);
        comp.from = comp.value;
        comp.to = pos;

        if (duration <= 0f) {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

}