using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubeMeshData
{
    public static readonly Vector3[] verts_startsZero = new Vector3[8]
    {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f)
    };
    public static readonly Vector3[] verts_centerZero = new Vector3[8]
    {
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f)
    };

    // Order of using voxelVerts.
    public static readonly int[,] vectorTris = new int[6, 4]
    {
        {0, 3, 1, 2 }, //back
        {5, 6, 4, 7 }, //front
        {3, 7, 2, 6 }, //top
        {1, 5, 0, 4 }, //bottom
        {4, 7, 0, 3 }, //left
        {1, 2, 5, 6 }  //right

        // Tip >> vectorTris order = [face index, (0, 1, 2, 2, 1, 3)]
    };

    // Value of normal vector.
    public static readonly Vector3Int[] faceChecks = new Vector3Int[6]
    {
        //back front top bottom left right
        new Vector3Int(0, 0, -1), //back
        new Vector3Int(0, 0, 1), //front
        new Vector3Int(0, 1, 0), //top
        new Vector3Int(0, -1, 0), //bottom
        new Vector3Int(-1, 0, 0), //left
        new Vector3Int(1, 0, 0)  //right
    };

    // Default UV order.
    public static readonly Vector2[] uvs = new Vector2[4]
    {
        new Vector2(0.0f, 0.0f), //0
        new Vector2(0.0f, 1.0f), //1
        new Vector2(1.0f, 0.0f), //2
        new Vector2(1.0f, 1.0f), //3
    };
}
