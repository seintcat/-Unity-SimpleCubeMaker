using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaker : MonoBehaviour
{
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    MeshFilter meshFilter;
    [SerializeField]
    Material material;

    // Horizontal and vertical cell counts are must be same.
    public int atlasSizeInBlocks = 16;
    // Using for UV, when making mesh with code.
    public float normalizedBlockTextureSize
    {
        get { return 1f / (float)atlasSizeInBlocks; }
    }

    int vertexIndex = 0;
    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    List<int> triangles = new List<int>();
    List<Color> colors = new List<Color>();
    List<Vector3> normals = new List<Vector3>();
    // face order >> front, back, up, down, left, right
    List<int> face = new List<int>();
    List<bool> make = new List<bool>();

    public int front, back, up, down, left, right;
    public bool makeFront = true, makeBack = true, makeUp = true, makeDown = true, makeLeft = true, makeRight = true;
    public CubePivot centerOption = CubePivot.Default;
    public Vector3 positionOffset;

    public enum CubePivot
    {
        Default = 0,
        Zero = 0,
        Center = 1
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeCubeQuick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeCubeQuick()
    {
        ClearMeshData();

        face.Add(front);
        face.Add(back);
        face.Add(up);
        face.Add(down);
        face.Add(left);
        face.Add(right);
        MakeMeshData(centerOption, face, positionOffset);

        ApplyMesh();
    }

    public void MakeMeshData(CubePivot pivot, List<int> uvIndex, Vector3 offset = new Vector3())
    {
        if(uvIndex.Count < 6)
        {
            Debug.Log("uvIndex error!");
            return;
        }

        make.Clear();
        make.Add(makeFront);
        make.Add(makeBack);
        make.Add(makeUp);
        make.Add(makeDown);
        make.Add(makeLeft);
        make.Add(makeRight);

        List<byte> face = new List<byte>();
        for (byte j = 0; j < 6; j++)
            if (make[j])
                face.Add(j);

        if ((int)pivot == 0)
            foreach(byte j in face)
            {
                vertices.Add(CubeMeshData.verts_startsZero[CubeMeshData.vectorTris[j, 0]] + offset);
                vertices.Add(CubeMeshData.verts_startsZero[CubeMeshData.vectorTris[j, 1]] + offset);
                vertices.Add(CubeMeshData.verts_startsZero[CubeMeshData.vectorTris[j, 2]] + offset);
                vertices.Add(CubeMeshData.verts_startsZero[CubeMeshData.vectorTris[j, 3]] + offset);
            }
        else if((int)pivot == 1)
            foreach (byte j in face)
            {
                vertices.Add(CubeMeshData.verts_centerZero[CubeMeshData.vectorTris[j, 0]] + offset);
                vertices.Add(CubeMeshData.verts_centerZero[CubeMeshData.vectorTris[j, 1]] + offset);
                vertices.Add(CubeMeshData.verts_centerZero[CubeMeshData.vectorTris[j, 2]] + offset);
                vertices.Add(CubeMeshData.verts_centerZero[CubeMeshData.vectorTris[j, 3]] + offset);
            }

        foreach (byte j in face)
        {
            colors.Add(new Color(255, 255, 255, 255));
            colors.Add(new Color(255, 255, 255, 255));
            colors.Add(new Color(255, 255, 255, 255));
            colors.Add(new Color(255, 255, 255, 255));

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 3);

            for (int i = 0; i < 4; i++) normals.Add(CubeMeshData.faceChecks[j]);

            AddTexture(uvIndex[j]);

            vertexIndex += 4;
        }
    }

    void AddTexture(int textureID)
    {
        float y = textureID / atlasSizeInBlocks * normalizedBlockTextureSize;
        float x = textureID % atlasSizeInBlocks * normalizedBlockTextureSize;

        uvs.Add(new Vector2(x, y));
        uvs.Add(new Vector2(x, y + normalizedBlockTextureSize));
        uvs.Add(new Vector2(x + normalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x + normalizedBlockTextureSize, y + normalizedBlockTextureSize));
    }

    public void ApplyMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();

        mesh.subMeshCount = 2;
        mesh.SetTriangles(triangles.ToArray(), 0);

        mesh.uv = uvs.ToArray();

        mesh.colors = colors.ToArray();

        mesh.normals = normals.ToArray();
        mesh.RecalculateNormals();

        meshRenderer.material = material;
        meshFilter.mesh = mesh;
    }

    void ClearMeshData()
    {
        vertexIndex = 0;
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();
        colors.Clear();
        normals.Clear();
        face.Clear();
    }
}