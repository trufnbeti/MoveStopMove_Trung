using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TextCombat : GameUnit
{
    [SerializeField] private Text distanceTxt;
    [SerializeField] private Text scoreTxt;

    public void OnInit(int score, float distance, bool isScale) {
        scoreTxt.text = "+" + score;
        if (isScale) {
            distanceTxt.gameObject.SetActive(true);
            distanceTxt.text = distance + "m";
        }
        StartCoroutine(WaitForDespawn(Constant.TIME_TEXT_COMBAT));
    }

    private IEnumerator WaitForDespawn(float time) {
        yield return CacheComponent.GetWFS(time);
        OnDespawn();
    }

    private void OnDespawn() {
        distanceTxt.gameObject.SetActive(false);
        SimplePool.Despawn(this);
    }
}
