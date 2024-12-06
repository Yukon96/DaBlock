using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtils
{
    public static Vector3 SnapToGrid(Vector3 position, float gridSize)
    {
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;
        return new Vector3(x, y, z);
    }

    public static Vector3 SnapToGridOnAxis(Vector3 position, float gridSize, Vector3 origin, string axis, int direction)
    {
        float x = position.x, y = position.y, z = position.z;

        switch (axis.ToLower())
        {
            case "x":
                x = Mathf.Round(((position.x + direction)- origin.x) / gridSize) * gridSize + origin.x;
                break;
            case "y":
                y = Mathf.Round(((position.y + direction)- origin.y) / gridSize) * gridSize + origin.y;
                break;
            case "z":
                z = Mathf.Round(((position.z + direction)- origin.z) / gridSize) * gridSize + origin.z;
                break;
        }

        return new Vector3(x, y, z);
    }

}

