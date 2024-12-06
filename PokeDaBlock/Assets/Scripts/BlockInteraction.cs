using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public BlockType blockType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the parent object's tag
        string parentTag = collision.transform.parent != null ? collision.transform.parent.tag : collision.gameObject.tag;
        Transform otherParent = collision.transform.parent != null ? collision.transform.parent : collision.transform;
        blockType = BlockType.GetOrCreateType(gameObject.tag);
        BlockType otherBlockType = BlockType.GetOrCreateType(parentTag);
        Debug.Log($"The {blockType.Name} is SUPER effective against {otherBlockType.Name}!");

        // Check if the current type is effective against the other type
        if (blockType.SuperEffectiveAgainst.Contains(otherBlockType))
        {
            Destroy(otherParent.gameObject); // Destroy the parent of the other object

            Debug.Log($"SUCCESS {blockType.Name} is SUPER effective against {otherBlockType.Name}!");
            // Add any additional logic for SUPER effective behavior
        }
    }
}
