using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockConnection : MonoBehaviour {

    List<Block> ConnectedBlocks = new List<Block>();
    private BlockColor? CurrentColor;
    private Board Board;
    private LineRenderer LineRenderer;

    public event Action<int> OnConnection;

    void Awake()
    {
        Board = FindObjectOfType<Board>();
        LineRenderer = GetComponent<LineRenderer>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!Input.GetMouseButton(0))
        {
            FinishConnection();
        }
	}



    public void Connect(Block block)
    {
        if (!Input.GetMouseButton(0)) return;
        if (ConnectedBlocks.Contains(block)) return;

        // limiting connecting block only to ones with the same color
        if (!CurrentColor.HasValue)
        {
            CurrentColor = block.Color;
        }
        if (CurrentColor != block.Color) return;

        if (ConnectedBlocks.Count() >= 1 && !ConnectedBlocks.Last().IsNeighbour(block)) return;

        block.IsConnected = true;
        ConnectedBlocks.Add(block);

        RefreshConnection();
    }

    private void FinishConnection()
    {
        ConnectedBlocks
            .ForEach(block => block.IsConnected = false);

        if (ConnectedBlocks.Count >= 3)
        {
            if (OnConnection != null)
            {
                OnConnection.Invoke(ConnectedBlocks.Count);
            }

            Board.RemoveBlocks(ConnectedBlocks);
            Board.RefreshBlocks();

        }

        ConnectedBlocks.Clear();

        CurrentColor = null;
        RefreshConnection();
    }

    private void RefreshConnection()
    {
        var points = ConnectedBlocks
            .Select(block => Board.GetBlockPosition(block.X, block.Y))
            .Select(position => (Vector3)position + Vector3.back * 2f)
            .ToArray();

        LineRenderer.positionCount = points.Length;
        LineRenderer.SetPositions(points);

    }
}
