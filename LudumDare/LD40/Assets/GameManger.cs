using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{

    private static GameManger _instance;
    private List<WeaponItem> _storedPlayerWeapons;
    private Dictionary<AmmoType, int> _storedPlayerAmmo;
    private int _storedScore;
    private GameObject _playerReference;
    public static GameManger Instance
    {
        get { return _instance; }
    }

	void Start ()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
	}

    public void SavePlayer(PlayerInventory player)
    {
        _storedPlayerWeapons = player.Weapons;
        _storedPlayerAmmo = player.Ammo;
        _storedScore = player.Gold;
    }

    public void RestorePlayer(PlayerInventory player)
    {
        if(_storedPlayerWeapons != null)
            player.Weapons = _storedPlayerWeapons;

        if(_storedPlayerAmmo != null)
            player.Ammo = _storedPlayerAmmo;

        player.Gold = _storedScore;

        _playerReference = player.gameObject;
    }

    public void ProgressLevel(string levelName, bool savePlayer)
    {
        if(savePlayer)
            SavePlayer(_playerReference.GetComponent<PlayerInventory>());

        SceneManager.LoadScene(levelName);
    }
}
