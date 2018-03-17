using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVMapBlender : MonoBehaviour
{

    public Material BlockMaterial;
    private float x = 0;
    private float y = 1;
    private const float PixelSize = 2;
    private Mesh _mesh;

    void Start()
    {
        GetComponent<Renderer>().material = BlockMaterial;
        _mesh = GetComponent<MeshFilter>().mesh;
        SetUVs();
    }

    public void SetHighlightTexture()
    {
        x = 1;
        SetUVs();
    }

    public void SetNormalTexture()
    {
        x = 0;
        SetUVs();
    }

    private void SetUVs()
    {
        Vector2[] blockUVs = (Vector2[])_mesh.uv.Clone();

        // Top
        blockUVs[23] = new Vector2(0.0f, 0.75f);
        blockUVs[20] = new Vector2(0.25f, 0.75f);
        blockUVs[22] = new Vector2(0.0f, 1.0f);
        blockUVs[21] = new Vector2(0.25f, 1.0f);

        // Bottom
        blockUVs[15] = new Vector2(0.0f, 0.5f);
        blockUVs[12] = new Vector2(0.25f, 0.5f);
        blockUVs[14] = new Vector2(0.0f, 0.75f);
        blockUVs[13] = new Vector2(0.25f, 0.75f);
    
        // North
        blockUVs[4] = new Vector2(0.25f, 0.75f);
        blockUVs[7] = new Vector2(0.5f, 0.75f);
        blockUVs[5] = new Vector2(0.25f, 1.0f);
        blockUVs[6] = new Vector2(0.5f, 1.0f);

        // South
        blockUVs[0] = new Vector2(0.5f, 0.75f);
        blockUVs[1] = new Vector2(0.75f, 0.75f);
        blockUVs[3] = new Vector2(0.5f, 1.0f);
        blockUVs[2] = new Vector2(0.75f, 1.0f);

        // East
        blockUVs[19] = new Vector2(0.25f, 0.5f);
        blockUVs[16] = new Vector2(0.5f, 0.5f);
        blockUVs[18] = new Vector2(0.25f, 0.75f);
        blockUVs[17] = new Vector2(0.5f, 0.75f);

        // West
        blockUVs[9] = new Vector2(0.5f, 0.5f);
        blockUVs[10] = new Vector2(0.75f, 0.5f);
        blockUVs[8] = new Vector2(0.5f, 0.75f);
        blockUVs[11] = new Vector2(0.75f, 0.75f);

        _mesh.uv = blockUVs;
    }

    public void SetFaceHighlight(BlockFace face)
    {
        Vector2[] blockUVs = (Vector2[])_mesh.uv.Clone();

        switch (face)
        {
            case BlockFace.Top:
                blockUVs[23] = new Vector2(0.0f, 0.25f);
                blockUVs[20] = new Vector2(0.25f, 0.25f);
                blockUVs[22] = new Vector2(0.0f, 0.5f);
                blockUVs[21] = new Vector2(0.25f, 0.5f);
                break;

            case BlockFace.Bottom:
                blockUVs[15] = new Vector2(0.0f, 0.0f);
                blockUVs[12] = new Vector2(0.25f, 0.0f);
                blockUVs[14] = new Vector2(0.0f, 0.25f);
                blockUVs[13] = new Vector2(0.25f, 0.25f);
                break;

            case BlockFace.North:
                blockUVs[4] = new Vector2(0.25f, 0.25f);
                blockUVs[7] = new Vector2(0.5f, 0.25f);
                blockUVs[5] = new Vector2(0.25f, 0.5f);
                blockUVs[6] = new Vector2(0.5f, 0.5f);
                break;

            case BlockFace.South:
                blockUVs[0] = new Vector2(0.5f, 0.25f);
                blockUVs[1] = new Vector2(0.75f, 0.25f);
                blockUVs[3] = new Vector2(0.5f, 0.5f);
                blockUVs[2] = new Vector2(0.75f, 0.5f);
                break;

            case BlockFace.East:
                blockUVs[19] = new Vector2(0.25f, 0.0f);
                blockUVs[16] = new Vector2(0.5f, 0.0f);
                blockUVs[18] = new Vector2(0.25f, 0.25f);
                blockUVs[17] = new Vector2(0.5f, 0.25f);
                break;

            case BlockFace.West:
                blockUVs[9] = new Vector2(0.5f, 0.0f);
                blockUVs[10] = new Vector2(0.75f, 0.0f);
                blockUVs[8] = new Vector2(0.5f, 0.25f);
                blockUVs[11] = new Vector2(0.75f, 0.25f);
                break;
        }

        _mesh.uv = blockUVs;
    }
}
