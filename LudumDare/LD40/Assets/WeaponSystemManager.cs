using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemManager : MonoBehaviour
{
    public PlayerInventory Inventory;
    public WeaponRegistry Registry;
    public Transform BulletPoint;
    public PlayerStats Stats;

    private bool _attacking = false;
    private List<AttackSystem> _attackSystems = new List<AttackSystem>();
    private int _weaponIndex = 0;
    private bool _nextWeapon = false;

	void Start ()
    {
        foreach (var sys in Registry.Weapons)
        {
            var obj = Instantiate(sys.WeaponSystem);
            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            _attackSystems.Add(obj.GetComponent<AttackSystem>());
        }

        _attackSystems[_weaponIndex].gameObject.SetActive(true);
        _attackSystems[_weaponIndex].Up();

        UpdateGameStats();
    }

    private void ShootProjectile()
    {
        if (Registry.Weapons[_weaponIndex].Type == AttackType.Projectile)
        {
            var bullet = Instantiate(Registry.Weapons[_weaponIndex].Projectile);
            var controller = bullet.GetComponent<Projectile>();
            controller.transform.position = BulletPoint.position;
            controller.Damage = Registry.Weapons[_weaponIndex].Damage;

            controller.Shoot(Registry.Weapons[_weaponIndex].ProjectileSpeed, BulletPoint.forward);

            Inventory.Ammo[Registry.Weapons[_weaponIndex].Ammo]--;

            Stats.CurrentAmmoAmmount = Registry.Weapons[_weaponIndex].Ammo != null && Inventory.Ammo.ContainsKey(Registry.Weapons[_weaponIndex].Ammo) ? Inventory.Ammo[Registry.Weapons[_weaponIndex].Ammo] : 0;

        }
    }

    void UpdateGameStats()
    {
        Stats.CurrentGunName = Registry.Weapons[_weaponIndex].Name;
        Stats.CurrentGunIcon = Registry.Weapons[_weaponIndex].Item;
        Stats.CurrentAmmoIcon = Registry.Weapons[_weaponIndex].Ammo != null ? Registry.Weapons[_weaponIndex].Ammo.Icon : null;
        Stats.CurrentAmmoAmmount = Registry.Weapons[_weaponIndex].Ammo != null && Inventory.Ammo.ContainsKey(Registry.Weapons[_weaponIndex].Ammo) ? Inventory.Ammo[Registry.Weapons[_weaponIndex].Ammo] : 0;
    }

    // Update is called once per frame
    void Update ()
	{
        //Update ammo display.

        if (_attacking && _attackSystems[_weaponIndex].IsReady())
        {
            if (Registry.Weapons[_weaponIndex].Ammo != null && Inventory.Ammo.ContainsKey(Registry.Weapons[_weaponIndex].Ammo) &&
                Inventory.Ammo[Registry.Weapons[_weaponIndex].Ammo] > 0)
            {
                ShootProjectile();
            }
            else if (Registry.Weapons[_weaponIndex].Type == AttackType.Melee)
            {
                //meele attack
            }
            else
            {
                //click
            }
        }

        if (!_attacking && Input.GetButtonDown("Fire1") && _attackSystems[_weaponIndex].IsReady())
        {
            if (Registry.Weapons[_weaponIndex].Ammo != null && Inventory.Ammo.ContainsKey(Registry.Weapons[_weaponIndex].Ammo) &&
                Inventory.Ammo[Registry.Weapons[_weaponIndex].Ammo] > 0)
            {
                _attacking = true;

                _attackSystems[_weaponIndex].StartAttack();

                ShootProjectile();
            }
            else if (Registry.Weapons[_weaponIndex].Type == AttackType.Melee)
            {
                //meele attack
                _attacking = true;
                _attackSystems[_weaponIndex].StartAttack();
            }
            else
            {
                //click
            }
        }

        if (_attacking && Input.GetButtonUp("Fire1"))
        {
            _attacking = false;
            _attackSystems[_weaponIndex].StopAttack();
        }

        if (Input.GetButton("Next Weapon"))
        {
            _nextWeapon = true;
            _attackSystems[_weaponIndex].Down();
        }

        if (_nextWeapon && _attackSystems[_weaponIndex].HasCompletedDown)
        {
            _nextWeapon = false;

            _attackSystems[_weaponIndex].gameObject.SetActive(false);

            _weaponIndex++;

            if (_weaponIndex >= _attackSystems.Count)
                _weaponIndex = 0;

            _attackSystems[_weaponIndex].gameObject.SetActive(true);
            _attackSystems[_weaponIndex].Up();

            UpdateGameStats();
        }
        
            
	}
}
