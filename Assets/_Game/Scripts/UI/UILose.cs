using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILose : UICanvas
{
    [SerializeField] private Text coinTxt;

    public override void Open() {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
    }
}
