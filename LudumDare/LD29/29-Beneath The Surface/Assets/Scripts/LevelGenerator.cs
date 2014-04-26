using System;
using System.Collections;
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
    public int WorldWidth = 100;
    public int WorldHeight = 100;
    public float BlockOffSet = 100f;
    public int BlockWidth = 32;
    public int BlockHeight = 32;
    public int MaxGemstones = 30;
    public int MinGemstones = 10;
    
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


    private float _xOffset = 0f;
    private new BoxCollider2D collider;
    private List<GameObject> _currentBlocks = new List<GameObject>();
    private List<GameObject> _lastBlocks = new List<GameObject>();
    private bool FirstLoad = true;
    private PlayerMovment _movmentScript;
    
	// Use this for initialization
	void Start ()
	{
	    _movmentScript = Player.GetComponent<PlayerMovment>();
        collider = GetComponent<BoxCollider2D>();
        StartCoroutine("GenerateWorld");
	    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(FirstLoad)
            Player.SetActive(false);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {

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

        _lastBlocks = _currentBlocks;
        _currentBlocks = new List<GameObject>();
        StartCoroutine("GenerateWorld");
    }


    IEnumerator GenerateWorld()
    {

        collider.size = new Vector2((BlockWidth * WorldWidth) / BlockOffSet, BlockHeight / BlockOffSet);
        transform.position = new Vector3(-(BlockWidth / 2) / BlockOffSet, (BlockHeight * WorldHeight / BlockOffSet) / 2, 0f);
        _xOffset = ((BlockWidth*WorldWidth) / BlockOffSet) / 2;
        _xOffset = -_xOffset;

        var gemStoneCount = UnityEngine.Random.Range(MinGemstones, MaxGemstones);
        



        var world = new int[WorldWidth, WorldHeight];

        for (var y = 0; y < WorldHeight; y++)
        {
            for (var x = 0; x < WorldWidth; x++)
                    world[x, y] = 0;

            world[0, y] = 1;
            world[WorldWidth -1, y] = 1;            
        }

        for (var i = gemStoneCount; i != 0;)
        {
            var x = UnityEngine.Random.Range(0, WorldWidth - 1);
            var y = UnityEngine.Random.Range(0, WorldHeight - 1);

            if (world[x, y] != 0) continue;

            i--;
            world[x, y] = 3;
        }

        world = GeneratePool(2, MaxLavaPools, MinLavaPools, MaxLavaPoolWidth, MinLavaPoolWidth, MaxLavaPoolHeight,
            MinLavaPoolHeight, world);

        world = GeneratePool(4, MaxSlimePools, MinSlimePools, MaxSlimePoolWidth, MinSlimePoolWidth, MaxSlimePoolHeight,
    MinSlimePoolHeight, world);

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

        if (!FirstLoad) yield break;
        FirstLoad = false;
        Player.SetActive(true);
    }

    private int[, ] GeneratePool(int blockID, int maxPool, int minPool, int maxPoolWidth, int minPoolWidth, int maxPoolHeight,
        int minPoolHeight, int[, ]world)
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
                    if (world[iX, iY] == 0)
                        world[iX, iY] = blockID;
                }
            }
        }

        return world;
    }
}
