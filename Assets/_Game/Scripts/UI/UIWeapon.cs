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
    private WeaponItem weaponItem;
    private int currentIdx;

    public override void Setup() {
        base.Setup();
        currentIdx = DataManager.Ins.IdWeapon;
        ChangeWeapon(currentIdx);
        playerCoinTxt.text = DataManager.Ins.Coin.ToString();
    }

    public override void CloseDirectly() {
        base.CloseDirectly();
        if (currentWeapon != null) {
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void OnBtnNextClick() {
        ChangeWeapon(weaponData.NextWeaponIdx(currentIdx));
    }

    public void OnBtnPrevClick() {
        ChangeWeapon(weaponData.PrevWeaponIdx(currentIdx));
    }

    public void OnBtnBuyClick() {
        if (DataManager.Ins.Coin >= weaponItem.cost) {
            DataManager.Ins.Coin -= weaponItem.cost;
            DataManager.Ins.SetStateData(currentIdx, 1, ShopType.Weapon);
            LoadBtn();
        }
        //TODO làm nốt equip weapon + fix rotate uzi
    }

    public void ChangeWeapon(int index) {
        currentIdx = index;

        if (currentWeapon != null ) {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = Instantiate(SkinManager.Ins.weaponData.GetWeapon(index), weaponPoint);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one;
        
        //check data dong
        LoadBtn();
        
    }

    private void LoadBtn() {
        int stateWeapon = DataManager.Ins.GetStateData(currentIdx, ShopType.Weapon);
        buttonState.SetState((StateButton)stateWeapon);
        weaponItem = weaponData.GetWeaponItem(currentIdx);
        nameTxt.text = weaponItem.name;
        coinTxt.text = weaponItem.cost.ToString();
    }
}
