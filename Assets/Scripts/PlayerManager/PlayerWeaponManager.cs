using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerBullet;
    [SerializeField] private GameObject PlayerBullet1;
    [SerializeField] private GameObject PlayerBullet2;

    private Shooting shoot;

    public Sprite Pistol1;
    public Sprite Shotgun2;


    private void Awake()
    {
        shoot = GetComponent<Shooting>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(3);
        }
    }

    void SetWeapon(int weaponID)
    {
        switch (weaponID)
        {
            case 1:
                shoot.SetBulletPrefab(PlayerBullet);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Pistol1;
                break;
            case 2:
                shoot.SetBulletPrefab(PlayerBullet1);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Shotgun2;
                break;
            case 3:
                shoot.SetBulletPrefab(PlayerBullet2);
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Shotgun2;
                break;
        }
    }

}
