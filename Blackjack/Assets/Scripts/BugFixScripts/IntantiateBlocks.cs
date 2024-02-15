using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntantiateBlocks : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform [] parent;
    public long[] blocks;
    public GameObject [] newBlocks;

    private void Start()
    {
        Initiate();
    }

    public void Initiate()
    {
        int i = 0;

        foreach(int block in blocks)
        {
            GameObject temp;
            temp = Instantiate(blockPrefab, parent[i].transform);
            newBlocks[i] = temp;

            newBlocks[i].GetComponent<BlockSnow>().SetBlockNumber(i);
            i++;
        }
    }
}
