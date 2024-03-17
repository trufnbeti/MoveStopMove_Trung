using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : Singleton<SkinManager>
{
    public CharacterSkin<Hat> hatData;
    public CharacterSkin<Accessory> accessoryData;
    public CharacterSkin<Material> pantData;
    public CharacterSkin<Skin> skinData;
    public WeaponData weaponData;

}
