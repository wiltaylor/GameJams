using UnityEngine;
using System.Collections;

public class EntryRoomController : MonoBehaviour
{
    public static EntryRoomController Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public GameObject ArcherPrefab;
    public GameObject MagePrefab;
    public GameObject SpawnPoint;
    public float HeroCountDown = 100f;
    public float CurrentCountDown = 100f;

    private int _challangeLevel = 0;

    void CheckLevel()
    {
        var score = GameManager.Instance.Evil + GameManager.Instance.Desire;

        if (score > 10 && _challangeLevel < 1)
        {
            _challangeLevel = 1;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if(score > 20 && _challangeLevel < 2)
        {
            _challangeLevel = 2;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if (score > 50 && _challangeLevel < 3)
        {
            _challangeLevel = 3;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if (score > 80 && _challangeLevel < 4)
        {
            _challangeLevel = 4;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if (score > 100 && _challangeLevel < 5)
        {
            _challangeLevel = 5;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if (score > 150 && _challangeLevel < 6)
        {
            _challangeLevel = 6;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if (score > 200 && _challangeLevel < 7)
        {
            _challangeLevel = 7;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if (score > 300 && _challangeLevel < 8)
        {
            _challangeLevel = 8;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if (score > 500 && _challangeLevel < 9)
        {
            _challangeLevel = 9;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }

        if (score > 1000 && _challangeLevel < 10)
        {
            _challangeLevel = 10;
            CurrentCountDown = 0;
            HeroCountDown -= 5f;
        }
    }

    void SelectSpawns()
    {
        switch (_challangeLevel)
        {
            case 0:
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 6:
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 7:
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 8:
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 9:
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
            case 10:
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(MagePrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                Instantiate(ArcherPrefab, SpawnPoint.transform.position, Quaternion.identity);
                break;
        }
    }

    void FixedUpdate()
    {
        CurrentCountDown -= Time.fixedDeltaTime;

        if (CurrentCountDown <= 0f)
        {
            SelectSpawns();
            CurrentCountDown = HeroCountDown;
            
        }
    }
}
