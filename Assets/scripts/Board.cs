using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    [SerializeField]
    GameObject BlockPrefab;

    [SerializeField]
    [Range(3, 8)]
    int Width = 5;
    [SerializeField]
    [Range(3, 10)]
    int Height = 6;

    [SerializeField]
    [Range(0.5f, 5f)]
    float GridSize = 1f;

    [SerializeField]
    [Range(0.5f, 5f)]
    public float BlockSize = 1f;

    public Block[,] Blocks { get; private set; }

    // Use this for initialization
    void Start () {
        GenerateBoard();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateBoard()
    {
        Blocks = new Block[Width, Height];

        for(int x=0; x<Width; x++)
        {
            for(int y=0; y<Height; y++)
            {
                Blocks[x, y] = GenerateBlock(x, y);
            }
        }
    }

    private Block GenerateBlock(int x, int y)
    {
        var obj = Instantiate(BlockPrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one * BlockSize;

        var block = obj.GetComponent<Block>();
        block.Configure(x, y);
        return block;

    }

    public Vector2 GetBlockPosition(int x, int y)
    {
        var basePosition = new Vector2(
            x - Width / 2f + 0.5f,
            y - Height / 2f + 0.5f);
        return basePosition * GridSize;
    }

    public void RemoveBlocks(List<Block> connectedBlocks)
    {
        connectedBlocks.ForEach(b => Blocks[b.X, b.Y] = null);
        connectedBlocks.ForEach(b => Destroy(b.gameObject));

    }

    public void RefreshBlocks()
    {
        for (int x=0; x<Width; x++)
        {
            int h = 0;

            for (int y=0; y<Height; y++)
            {
                if(Blocks[x,y] == null)
                {
                    continue;
                }

                Blocks[x, h] = Blocks[x, y];
                Blocks[x, h].Configure(x, h);

                h++;
            }

            for (int y=h; y<Height; y++)
            {
                Blocks[x, y] = GenerateBlock(x, y);
                Blocks[x, y].PlaceOnTargetPosition();
            }
        }
    }
}
