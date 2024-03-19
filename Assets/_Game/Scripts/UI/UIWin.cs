using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : UICanvas
{
    [SerializeField] private Text coinTxt;

    public override void Open() {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
    }

    public void OnBtnNextClick() {
        this.PostEvent(EventID.AddCoin);
        this.PostEvent(EventID.NextLevel);
        this.PostEvent(EventID.Home);
    }

    public void SetCoin(int coin) {
        coinTxt.text = coin.ToString();
    }
}
