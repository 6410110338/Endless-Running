using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = -10.0f;
    private float tileLength = 40.0f;
    private float safeZone = 43.0f;
    private int amnTilesOnScreen = 7;

    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i= 0; i< amnTilesOnScreen; i++)
        {
            if (i < 4)
                SpawnTile(0);
            else
                SpawnTile();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
        
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject Tile;
        if(prefabIndex == -1)
            Tile = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            Tile = Instantiate(tilePrefabs[prefabIndex]) as GameObject;

        Tile.transform.SetParent(transform);
        Tile.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(Tile);

    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randowIndex = lastPrefabIndex;
        while (randowIndex == lastPrefabIndex)
        {
            randowIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randowIndex;
        return randowIndex;
    }
}
