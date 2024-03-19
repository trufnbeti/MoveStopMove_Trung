using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Assign this script to the indicator prefabs.
/// </summary>
public class Indicator : MonoBehaviour
{
    [SerializeField] private IndicatorType indicatorType;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private CanvasGroup canvasGroup;

    /// <summary>
    /// Gets if the game object is active in hierarchy.
    /// </summary>
    public bool Active
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
    }

    /// <summary>
    /// Gets the indicator type
    /// </summary>
    public IndicatorType Type
    {
        get
        {
            return indicatorType;
        }
    }

    /// <summary>
    /// Sets the image color for the indicator.
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        indicatorImage.color = color;
        if (indicatorType == IndicatorType.BOX) {
            nameTxt.color = color;
        }
    }

    public void SetName(string name) {
        if (indicatorType == IndicatorType.BOX) {
            nameTxt.text = name;
        }
    }

    public void SetScore(int score) {
        if (indicatorType == IndicatorType.BOX) {
            scoreTxt.text = score.ToString();
        }
    }

    public void SetAlpha(float alpha) {
        canvasGroup.alpha = alpha;
    }

    /// <summary>
    /// Sets the distance text for the indicator.
    /// </summary>
    /// <param name="value"></param>

    /// <summary>
    /// Sets the distance text rotation of the indicator.
    /// </summary>
    /// <param name="rotation"></param>

    /// <summary>
    /// Sets the indicator as active or inactive.
    /// </summary>
    /// <param name="value"></param>
    public void Activate(bool value)
    {
        transform.gameObject.SetActive(value);
    }
}

public enum IndicatorType
{
    BOX,
    ARROW
}
