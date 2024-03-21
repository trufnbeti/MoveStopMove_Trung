    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILose : UICanvas
{
    [SerializeField] private Text coinTxt;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text rankingTxt;

    public override void Setup() {
        base.Setup();
        nameTxt.text = LevelManager.Ins.player.Attacker.Name;
        rankingTxt.text = "#" + LevelManager.Ins.player.Ranking;
        SoundManager.Ins.Play(SoundType.Lose, ref SoundManager.Ins.audioSource);
    }

    public override void Open() {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
    }

    public void OnBtnHomeClick() {
        this.PostEvent(EventID.AddCoin);
        this.PostEvent(EventID.Home);
    }

    public void SetCoin(int coin) {
        coinTxt.text = coin.ToString();
    }
}
