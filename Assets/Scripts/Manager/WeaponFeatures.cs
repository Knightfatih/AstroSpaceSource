using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Pistol,
    Shotgun,
    Rifle
}

[System.Serializable]
public class WeaponFeatures
{
    public int damage;
    public float range;
    public int maxAmmo;
    public int magazineCount;
    public int maxMagazines;
    public float reloadTime;
}
