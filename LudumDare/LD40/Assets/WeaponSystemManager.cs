using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystemManager : MonoBehaviour
{
    public PlayerInventory Inventory;
    public WeaponRegistry Registry;
    public Transform BulletPoint;

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
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!_attacking && Input.GetButtonDown("Fire1"))
        {
            _attacking = true;

            _attackSystems[_weaponIndex].StartAttack();

            if (Registry.Weapons[_weaponIndex].Type == AttackType.Projectile)
            {
                var bullet = Instantiate(Registry.Weapons[_weaponIndex].Projectile);
                var controller = bullet.GetComponent<Projectile>();
                controller.transform.position = BulletPoint.position;
                controller.Damage = Registry.Weapons[_weaponIndex].Damage;

                controller.Shoot(1, BulletPoint.forward);
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
        }
        
            
	}
}
