using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Player;
    public GameObject DirtBlock;
    public GameObject ShieldWall;
    public GameObject LavaBlock;
    public GameObject GemStones;
    public GameObject Slime;
    public GameObject Gold;
    public GameObject Heath;
    public GameObject Water;
    public GameObject Monster;
    public GameObject SlowBlock;
    public GameObject Dimond;
    public LevelGenerationDataScript[] LevelData;

    public int WorldWidth = 100;
    public int WorldHeight = 100;
    public float BlockOffSet = 100f;
    public int BlockWidth = 32;
    public int BlockHeight = 32;

    public int MaxGemstones = 30;
    public int MinGemstones = 10;
    public int MaxGold = 3;
    public int MinGold = 0;
    public int MaxHealthBoxes = 10;
    public int MinHealthBoxes = 1;
    public int MaxDimond = 0;
    public int MinDimond = 0;
    public int MaxMonsters = 1;
    public int MinMonsters = 1;
    public float MaxMonsterHight = 8f;
    
    public int MaxLavaPools = 3;
    public int MinLavaPools = 0;
    public int MinLavaPoolHeight = 1;
    public int MaxLavaPoolHeight = 3;
    public int MinLavaPoolWidth = 1;
    public int MaxLavaPoolWidth = 5;

    public int MaxSlimePools = 3;
    public int MinSlimePools = 0;
    public int MinSlimePoolHeight = 1;
    public int MaxSlimePoolHeight = 3;
    public int MinSlimePoolWidth = 1;
    public int MaxSlimePoolWidth = 5;

    public int MaxWaterPools = 3;
    public int MinWaterPools = 0;
    public int MinWaterPoolHeight = 1;
    public int MaxWaterPoolHeight = 3;
    public int MinWaterPoolWidth = 1;
    public int MaxWaterPoolWidth = 5;
    
    public int MaxSlowBlockPool = 0;
    public int MinSlowBlockPool = 0;
    public int MaxSlowBlockPoolHeight = 0;
    public int MinSlowBlockPoolHeight = 0;
    public int MaxSlowBlockPoolWidth = 0;
    public int MinSlowBlockPoolWidth = 0;

    private float _xOffset = 0f;
    private new BoxCollider2D collider;
    private List<GameObject> _currentBlocks = new List<GameObject>();
    private List<GameObject> _lastBlocks = new List<GameObject>();
    private List<GameObject> _monsters = new List<GameObject>(); 
    private bool _firstLoad = true;
    private PlayerMovment _movmentScript;
    
	// Use this for initialization
	void Start ()
	{
	    _movmentScript = Player.GetComponent<PlayerMovment>();
        collider = GetComponent<BoxCollider2D>();
        StartCoroutine("GenerateWorld");
        _lastBlocks.AddRange(GameObject.FindGameObjectsWithTag("Block"));
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(_firstLoad)
            Player.SetActive(false);
	}

    void GetLevelSettings()
    {

        if (LevelData.Length == 0)
            return;

        int index = 0;

        for (int i = 0; i < LevelData.Length; i++)
        {
            if (LevelData[i].Distance < _movmentScript.DistanceTraveled /5)
                index = i;
            else
                break;
        }

        Debug.Log(String.Format("Building Level from {0}", index));

        MaxGemstones = LevelData[index].MaxGemstones;
        MinGemstones = LevelData[index].MinGemstones;
        MaxGold = LevelData[index].MaxGold;
        MinGold = LevelData[index].MinGold;
        MaxHealthBoxes = LevelData[index].MaxHealthBoxes;
        MinHealthBoxes = LevelData[index].MinHealthBoxes;
        MaxMonsters = LevelData[index].MaxMonsters;
        MinMonsters = LevelData[index].MinMonsters;
        MaxDimond = LevelData[index].MaxDimond;
        MinDimond = LevelData[index].MinDimond;

        MaxLavaPools = LevelData[index].MaxLavaPools;
        MinLavaPools = LevelData[index].MinLavaPools;
        MinLavaPoolHeight = LevelData[index].MinLavaPoolHeight;
        MaxLavaPoolHeight = LevelData[index].MaxLavaPoolHeight;
        MinLavaPoolWidth = LevelData[index].MinLavaPoolWidth;
        MaxLavaPoolWidth = LevelData[index].MaxLavaPoolWidth;

        MaxSlimePools = LevelData[index].MaxSlimePools;
        MinSlimePools = LevelData[index].MinSlimePools;
        MinSlimePoolHeight = LevelData[index].MinSlimePoolHeight;
        MaxSlimePoolHeight = LevelData[index].MaxSlimePoolHeight;
        MinSlimePoolWidth = LevelData[index].MinSlimePoolWidth;
        MaxSlimePoolWidth = LevelData[index].MaxSlimePoolWidth;

        MaxWaterPools = LevelData[index].MaxWaterPools;
        MinWaterPools = LevelData[index].MinWaterPools;
        MinWaterPoolHeight = LevelData[index].MinWaterPoolHeight;
        MaxWaterPoolHeight = LevelData[index].MaxWaterPoolHeight;
        MinWaterPoolWidth = LevelData[index].MinWaterPoolWidth;
        MaxWaterPoolWidth = LevelData[index].MaxWaterPoolHeight;

        MaxSlowBlockPool = LevelData[index].MaxSlowBlockPool;
        MinSlowBlockPool = LevelData[index].MinSlowBlockPool;
        MaxSlowBlockPoolHeight = LevelData[index].MaxSlowBlockPoolHeight;
        MinSlowBlockPoolHeight = LevelData[index].MinSlowBlockPoolHeight;
        MaxSlowBlockPoolWidth = LevelData[index].MaxSlowBlockPoolWidth;
        MinSlowBlockPoolWidth = LevelData[index].MinSlowBlockPoolWidth;
    }


    void OnDrill(GameObject player)
    {
        //do nothing.
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag != "Player")
            return;

        foreach(var i in _lastBlocks)
            Destroy(i);

        var distance = WorldHeight*BlockHeight/BlockOffSet;

        _movmentScript.LastPosition = new Vector3(_movmentScript.LastPosition.x, _movmentScript.LastPosition.y + distance, 0f);
        Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + distance, 0f);

        foreach (var i in _currentBlocks)
        {
            try
            {
                i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y + distance, 0f);
            }
            catch (Exception)
            {
                //Skip exceptions. It will be destroyed objects.
            }
        }

        foreach (var i in _monsters)
        {
            try
            {
                i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y + distance, 0f);
            }
            catch (Exception)
            {
                //Skip exceptions. It will be destroyed objects.
            }
        }

        _lastBlocks = _currentBlocks;
        _currentBlocks = new List<GameObject>();
        StartCoroutine("GenerateWorld");
    }


    IEnumerator GenerateWorld()
    {

        GetLevelSettings();

        collider.size = new Vector2((BlockWidth * WorldWidth) / BlockOffSet, BlockHeight / BlockOffSet);
        transform.position = new Vector3(-(BlockWidth / 2) / BlockOffSet, (BlockHeight * WorldHeight / BlockOffSet) / 2, 0f);
        _xOffset = ((BlockWidth*WorldWidth) / BlockOffSet) / 2;
        _xOffset = -_xOffset;

        var world = new int[WorldWidth, WorldHeight];

        for (var y = 0; y < WorldHeight; y++)
        {
            for (var x = 0; x < WorldWidth; x++)
                    world[x, y] = 0;

            world[0, y] = 1;
            world[WorldWidth -1, y] = 1;            
        }

        world = AddOre(3, MaxGemstones, MinGemstones, world);
        yield return 0;
        world = AddOre(5, MaxGold, MinGold, world);
        yield return 0;
        world = AddOre(6, MaxHealthBoxes, MinHealthBoxes, world);
        yield return 0;
        world = AddOre(9, MaxDimond, MinDimond, world);

        world = GeneratePool(2, MaxLavaPools, MinLavaPools, MaxLavaPoolWidth, MinLavaPoolWidth, MaxLavaPoolHeight,
            MinLavaPoolHeight, world, new[] {0, 3, 5, 6, 9});
        yield return 0;

        world = GeneratePool(4, MaxSlimePools, MinSlimePools, MaxSlimePoolWidth, MinSlimePoolWidth,
            MaxSlimePoolHeight,
            MinSlimePoolHeight, world, new[] {0, 3, 5, 6,  9});
        yield return 0;

        world = GeneratePool(7, MaxWaterPools, MinWaterPools, MaxWaterPoolWidth, MinWaterPoolWidth, MaxWaterPoolHeight,
            MinWaterPoolHeight, world, new [] {0, 3, 5, 6, 9});
        yield return 0;

        world = GeneratePool(8, MaxSlowBlockPool, MinSlowBlockPool, MaxSlowBlockPoolWidth, MinSlowBlockPoolWidth, MaxSlowBlockPoolHeight,
            MinSlowBlockPoolHeight, world, new[] { 0, 3, 5, 6, 9 });
        yield return 0;

        for (var y = 0; y < WorldHeight; y++)
        {
            for (var x = 0; x < WorldWidth; x++)
            {
                GameObject newBlock = null;

                if (world[x, y] == 0)
                    newBlock = (GameObject) Instantiate(DirtBlock);
                if (world[x, y] == 1)
                    newBlock = (GameObject) Instantiate(ShieldWall);
                if (world[x, y] == 2)
                    newBlock = (GameObject) Instantiate(LavaBlock);
                if (world[x, y] == 3)
                    newBlock = (GameObject) Instantiate(GemStones);
                if (world[x, y] == 4)
                    newBlock = (GameObject) Instantiate(Slime);
                if (world[x, y] == 5)
                    newBlock = (GameObject) Instantiate(Gold);
                if (world[x, y] == 6)
                    newBlock = (GameObject) Instantiate(Heath);
                if (world[x, y] == 7)
                    newBlock = (GameObject) Instantiate(Water);
                if (world[x, y] == 8)
                    newBlock = (GameObject) Instantiate(SlowBlock);
                if (world[x, y] == 9)
                    newBlock = (GameObject) Instantiate(Dimond);

                if (newBlock == null)
                {
                    Debug.Log(string.Format("Block at {0}/{1} is set to invalid type.", x, y));
                    newBlock = (GameObject) Instantiate(DirtBlock);
                }

                newBlock.transform.position = new Vector3(_xOffset + (x*BlockWidth)/BlockOffSet,
                    (y*BlockHeight)/BlockOffSet, 0f);

                _currentBlocks.Add(newBlock);

            }

            yield return 0;
        }

        var monsterCount = UnityEngine.Random.Range(MinMonsters, MaxMonsters);
        _monsters.RemoveAll(m =>
        {
            if (m.transform.position.y > MaxMonsterHight)
            {
                Destroy(m);
                return true;
            }
            else
            {
                return false;
            }
        });

        if (_monsters.Count < monsterCount)
        {
            Debug.Log(string.Format("Creating new monster level monster count is {0} and current monster count is {1}", monsterCount, _monsters.Count));
            for (var i = 0; _monsters.Count < monsterCount; i++)
            {
                var x = UnityEngine.Random.Range(0, WorldWidth - 1);
                var y = UnityEngine.Random.Range(0, WorldHeight - 1);
                var newMonster = (GameObject)Instantiate(Monster);

                newMonster.transform.position = new Vector3(_xOffset + (x * BlockWidth) / BlockOffSet,
                    (y * BlockHeight) / BlockOffSet, 0f);


                _monsters.Add(newMonster);
            }
        }

        if (!_firstLoad) yield break;
        _firstLoad = false;
        Player.SetActive(true);
    }

    private int[,] AddOre(int blockID, int maxOre, int minOre, int[,] world)
    {
        var oreCount = UnityEngine.Random.Range(minOre, maxOre);

        for (var i = oreCount; i != 0; )
        {
            var x = UnityEngine.Random.Range(0, WorldWidth - 1);
            var y = UnityEngine.Random.Range(0, WorldHeight - 1);

            if (world[x, y] != 0) continue;

            i--;
            world[x, y] = blockID;
        }

        return world;
    }

    private int[, ] GeneratePool(int blockID, int maxPool, int minPool, int maxPoolWidth, int minPoolWidth, int maxPoolHeight,
        int minPoolHeight, int[, ]world,  int[] Replaceable)
    {
        var poolCount = UnityEngine.Random.Range(minPool, maxPool);

        for (var i = poolCount; i != 0; i--)
        {
            var x = UnityEngine.Random.Range(0, WorldWidth - 1);
            var y = UnityEngine.Random.Range(0, WorldHeight - 1);
            var poolWidth = UnityEngine.Random.Range(minPoolWidth, maxPoolWidth);
            var poolHeight = UnityEngine.Random.Range(minPoolHeight, maxPoolHeight);

            while (poolWidth + x > WorldWidth - 1)
            {
                poolWidth--;
            }

            while (poolHeight + y > WorldHeight - 1)
            {
                poolHeight--;
            }

            for (var iX = x; iX < x + poolWidth; iX++)
            {
                for (var iY = y; iY < y + poolHeight; iY++)
                {
                    if (Replaceable.Contains(world[iX, iY]))
                        world[iX, iY] = blockID;
                }
            }
        }

        return world;
    }
}
