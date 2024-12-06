using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [System.Serializable]
    public class BlockTyped
    {
        public string tag;
        public Sprite[] sprites; // Array of sprites for this tag
    }
    private float lastSpawnTime = 0f;//Spawn time tracker   
    public float spawnInterval = .25f; // Minimum time between spawns
    public BlockTyped[] blockTypes; // List of block types
    public Transform spawnPoint;   // Spawn location
    public GameObject objectToSpawn; // Drag your prefab here

    void Start()
    {
        SpawnRandomTypeBlock();
        BlockType.InitializeTypes();
    }

    public void SpawnNewBlock()
    {
        if (CanSpawn())
        {
            // Instantiate the object at the spawn point's position and rotation
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            lastSpawnTime = Time.time;

        }
    }
    public void SpawnRandomTypeBlock()
    {
        if (CanSpawn() || Time.time == 0)
        {
            // Choose a random block type
            int randomTypeIndex = Random.Range(0, blockTypes.Length);
            BlockTyped selectedType = blockTypes[randomTypeIndex];

            // Choose a random sprite for this type
            int randomSpriteIndex = Random.Range(0, selectedType.sprites.Length);
            Sprite selectedSprite = selectedType.sprites[randomSpriteIndex];

            // Instantiate the block
            GameObject block = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            // Set the tag and sprite
            block.tag = selectedType.tag;
            block.GetComponent<SpriteRenderer>().sprite = selectedSprite;
            lastSpawnTime = Time.time;
        }

    }
    private bool CanSpawn()
    {
        return Time.time - lastSpawnTime >= spawnInterval;
    }
}

