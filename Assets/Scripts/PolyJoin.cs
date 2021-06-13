using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClipperLib;
using System.Linq;

using Path = System.Collections.Generic.List<ClipperLib.IntPoint>;
using Paths = System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>;


[RequireComponent(typeof(PolygonCollider2D))]
public class PolyJoin : MonoBehaviour
{
	public int precision = 10000;

	private PolygonCollider2D polyCollider;

    // Start is called before the first frame update
    void Start()
    {
		polyCollider = GetComponent<PolygonCollider2D>();

		Paths subj = new Paths();

		foreach (BoxCollider2D bc in GetComponentsInChildren<BoxCollider2D>())
        {
			//Debug.Log("Adding box with offset=" + bc.offset + ", size=" + bc.size);
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
			//string s = "";
			//foreach (IntPoint ip in path)
   //         {
			//	s += "(" + ip.X + "," + ip.Y + "),";
   //         }
			//Debug.Log("IntPoints:"+s);
			subj.Add(path);

			bc.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

		Paths solution = new Paths();

		Clipper c = new Clipper();
		c.AddPaths(subj, PolyType.ptSubject, true);
		//c.AddPaths(clip, PolyType.ptClip, true);
		c.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftPositive);


		//Debug.Log("Computed union");
		foreach (Path path in solution)
		{
			//string s = "";
			//foreach (IntPoint ip in path)
			//{
			//	s += "(" + ip.X + "," + ip.Y + "),";
			//}
			//Debug.Log("IntPoints:" + s);
			Vector2[] worldPoints = path.Select(point => new Vector2((float)point.X / precision, (float)point.Y / precision)).ToArray();
			Vector2[] localPoints = worldPoints.Select(point => (Vector2)transform.InverseTransformPoint(point)).ToArray();

			//s = "";
			//foreach (Vector2 ip in localPoints)
			//{
			//	s += "(" + ip.x + "," + ip.y + "),";
			//}
			//Debug.Log("LocalPoints:" + s);

            polyCollider.points = localPoints;


			break;
		}

		GetComponent<PolygonMeshRenderer>().Refresh();

		polyCollider.enabled = false;

	}

	// Update is called once per frame
	void Update()
    {
		
	}

}
