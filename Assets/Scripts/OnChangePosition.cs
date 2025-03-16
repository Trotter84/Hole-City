using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    [Header("Components")]
    public PolygonCollider2D ground2DCollider;
    public PolygonCollider2D hole2DCollider;
    public MeshCollider GenerateMeshCollider;
    Mesh GeneratedMesh;


    void FixedUpdate()
    {
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            MakeHole2D();
            Make3DMeshCollider();
        }
    }

    void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DCollider.GetPath(0);

        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] += (Vector2)hole2DCollider.transform.position;
        }

        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, PointPositions);
    }

    void Make3DMeshCollider()
    {
        if (GeneratedMesh != null) Destroy(GeneratedMesh);

        GeneratedMesh = ground2DCollider.CreateMesh(true, true);
        GenerateMeshCollider.sharedMesh = GeneratedMesh;
    }
}
