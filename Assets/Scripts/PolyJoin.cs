using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClipperLib;
using System.Linq;

using Path = System.Collections.Generic.List<ClipperLib.IntPoint>;
using Paths = System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>;


public class PolyJoin : MonoBehaviour
{
	public int precision = 10000;

    public GameObject pathPrefab;

    // Start is called before the first frame update
    void Start()
    {
		Paths subj = new Paths();

		foreach (BoxCollider2D bc in GetComponentsInChildren<BoxCollider2D>())
        {
            var halfsize = bc.size / 2;
			Vector2[] corners = 
			{
				-halfsize,
				new Vector2(halfsize.x, -halfsize.y),
				halfsize,
				new Vector2(-halfsize.x, halfsize.y),
			};

			Vector3[] worldPoints = corners.Select(v => bc.transform.TransformPoint(bc.offset + v)).ToArray();
			Path path = worldPoints.Select(v => new IntPoint(v.x * precision, v.y * precision)).ToList();
            subj.Add(path);

            bc.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            //string s = "";
            //foreach (IntPoint ip in path)
            //{
            //    s += "(" + ip.X + "," + ip.Y + "),";
            //}
            //Debug.Log("Adding box from " + bc.gameObject.name + ": " + s);
        }

        Paths solution = new Paths();

		Clipper c = new Clipper();
		c.AddPaths(subj, PolyType.ptSubject, true);
		//c.AddPaths(clip, PolyType.ptClip, true);
		c.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftPositive);


        Debug.Log("Computed union into " + solution.Count + " paths");
        foreach (Path path in solution)
		{
            GameObject pathObject = Instantiate(pathPrefab);
            pathObject.transform.parent = transform;

            //string s = "";
            //foreach (IntPoint ip in path)
            //{
            //    s += "(" + ip.X + "," + ip.Y + "),";
            //}
            //Debug.Log("IntPoints:" + s);

            Vector2[] worldPoints = path.Select(point => new Vector2((float)point.X / precision, (float)point.Y / precision)).ToArray();
            //s = "";
            //foreach (Vector2 ip in worldPoints)
            //{
            //    s += "(" + ip.x + "," + ip.y + "),";
            //}

            pathObject.GetComponent<PolygonMeshRenderer>().Refresh(worldPoints);
		}

	}

}
