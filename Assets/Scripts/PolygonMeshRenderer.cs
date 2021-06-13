using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(LineRenderer))]
public class PolygonMeshRenderer : MonoBehaviour
{
    public enum Interior { None, Filled }
    public enum Outline { None, Open, Closed }

    public Interior interior = Interior.Filled;
    public Outline outline = Outline.Closed;

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public LineRenderer lineRenderer;
    public bool isWorldSpaceUV;
    public bool isOutlineClosed = true;

    private Vector2[] worldPoints;
    private Vector2[] localPoints;
    private Bounds pointBounds; //In world space

    // Start is called before the first frame update
    void Start()
    {
        //Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh(Vector2[] worldPoints)
    {
        this.worldPoints = worldPoints;
        localPoints = worldPoints.Select(point => (Vector2)transform.InverseTransformPoint(point)).ToArray();
        pointBounds = VectorUtils.ComputeBoundsFromPoints(worldPoints);

        CreateMesh();
        CreateLine();
    }

    void CreateMesh()
    {
        if (meshFilter == null) meshFilter = GetComponent<MeshFilter>();
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if ((meshFilter == null || meshRenderer == null) && interior == Interior.None) return;
        if (meshFilter == null)
        {
            Debug.LogError(this + " has null meshFilter");
            return;
        }
        if (meshRenderer == null)
        {
            Debug.LogError(this + " has null meshRenderer");
            return;
        }

        if (interior == Interior.None)
        {
            meshRenderer.enabled = false;
            return;
        }
        else
        {
            meshRenderer.enabled = true;
        }

        int pointCount = localPoints.Length;
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[pointCount];
        Vector2[] uv = new Vector2[pointCount];
        for (int j = 0; j < pointCount; j++)
        {
            Vector2 actual = localPoints[j];
            vertices[j] = new Vector3(actual.x, actual.y, 0);
            if (isWorldSpaceUV)
            {
                uv[j] = actual;

            }
            else
            {
                uv[j] = new Vector2(actual.x / pointBounds.size.x, actual.y / pointBounds.size.y);
            }
        }
        Triangulator tr = new Triangulator(localPoints);
        int[] triangles = tr.Triangulate();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;
    }

    void CreateLine()
    {
        //if (polygonCollider2D == null) polygonCollider2D = gameObject.GetComponent<PolygonCollider2D>();
        if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null && outline == Outline.None) return;
        if (lineRenderer == null)
        {
            Debug.LogError(this + " has null lineRenderer");
            return;
        }
        if (outline == Outline.None)
        {
            lineRenderer.enabled = false;
            return;
        }
        else
        {
            lineRenderer.enabled = true;
        }

        //Render thing
        int pointCount = localPoints.Length;
        //pointCount = polygonCollider2D.GetTotalPointCount();
        if (pointCount < 1) return;

        int loopClose = 0;
        if (outline == Outline.Closed)
            loopClose++;
        //Vector2[] points = polygonCollider2D.points;

        lineRenderer.positionCount = pointCount + loopClose;

        for (int j = 0; j < pointCount; j++)
        {
            lineRenderer.SetPosition(j, (Vector3)localPoints[j]);
        }

        if (outline == Outline.Closed)
        {
            lineRenderer.SetPosition(pointCount, (Vector3)localPoints[0]);
        }
    }
}
