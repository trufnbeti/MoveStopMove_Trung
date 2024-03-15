using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UICanvas
{
    [SerializeField] private Transform weaponPoint;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private ButtonState buttonState;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text coinTxt;
    [SerializeField] private Text playerCoinTxt;
    
    private Weapon currentWeapon;
    private WeaponType weaponType;

    public override void Setup() {
        base.Setup();
        ChangeWeapon(DataManager.Ins.GetEnumData<WeaponType>(DataManager.Ins.IdWeapon));
        playerCoinTxt.text = DataManager.Ins.Coin.ToString();
    }

    public override void CloseDirectly() {
        base.CloseDirectly();
        if (currentWeapon != null) {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = null;
        }
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void OnBtnNextClick() {
        ChangeWeapon(weaponData.NextType(weaponType));
    }

    public void OnBtnPrevClick() {
        ChangeWeapon(weaponData.PrevType(weaponType));
    }

    public void OnBtnBuyClick() {
        // if (UserData.Ins.coin >= weaponIte)
    }

    public void ChangeWeapon(WeaponType weaponType) {
        this.weaponType = weaponType;

        if (currentWeapon != null ) {
            SimplePool.Despawn(currentWeapon);
        }
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, Vector3.zero, Quaternion.identity, weaponPoint);

        //check data dong
        int stateWeapon = DataManager.Ins.GetStateData(weaponType);
        if (stateWeapon != -1) {
            StateButton state = (StateButton)stateWeapon;
            buttonState.SetState(state);

            WeaponItem item = weaponData.GetWeaponItem(weaponType);
            nameTxt.text = item.name;
            coinTxt.text = item.cost.ToString();
        }
    }
}
