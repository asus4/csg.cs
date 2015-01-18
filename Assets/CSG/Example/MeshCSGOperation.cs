using UnityEngine;
using System.Collections;
using ConstructiveSolidGeometry;

public class MeshCSGOperation : MonoBehaviour
{

    /*
     *  Apply a CSG operation to the meshes for specified GameObjects a and b.
     *  If a and b are not specified (null), grab the meshes from the first to children of the transform.
     *  newObjectPrefab is cloned and given the resulting mesh after the CSG operation.
     */

    public enum Operation { Subtract, Union, Intersection };
    public Operation operation;
	public MeshFilter aMesh;
	public MeshFilter bMesh;
    public GameObject newObjectPrefab;
    
    public bool isRealtime;
    
	MeshFilter targetMesh;
	
    void Start()
    {
    
		if (bMesh == null || bMesh == null)
        {
			MeshFilter[] children = GetComponentsInChildren<MeshFilter>();
            if(children.Length >= 2) {
				aMesh = children[0].GetComponent<MeshFilter>();
				bMesh = children[1].GetComponent<MeshFilter>();
            }
        }
        
		GameObject newGo = Instantiate(newObjectPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		targetMesh = newGo.GetComponent<MeshFilter>();
		ComputeMesh(aMesh, bMesh, operation, targetMesh);

        /*
         * Debug.Log(A.polygons.Count + ", " + B.polygons.Count + ", " + result.polygons.Count);
        foreach (Polygon p in result.polygons) {
            Debug.Log("Result: " + p.vertices[0].pos+", "+p.vertices[1].pos+", "+p.vertices[2].pos);
            if (p.vertices.Length > 3) Debug.Log("!!! " + p.vertices.Length);
        }
        */
        
		aMesh.gameObject.SetActive(false);
		bMesh.gameObject.SetActive(false);
    }
    
    void Update()
    {
		if(isRealtime)
		{
			ComputeMesh(aMesh, bMesh, operation, targetMesh);
		}
    }
    
    CSG ComputeMesh(MeshFilter aMeshFilter, MeshFilter bMeshFilter, Operation op, MeshFilter target)
    {
		CSG a = CSG.fromMeshFilter(aMeshFilter);
		CSG b = CSG.fromMeshFilter(bMeshFilter);
		
		CSG result = null;
		if (op == Operation.Subtract)
		{
			result = a.subtract(b);
		}
		if (op == Operation.Union)
		{
			result = a.union(b);
		}
		if (op == Operation.Intersection)
		{
			result = a.intersect(b);
		}
		
		if(result != null)
		{
			target.mesh = result.toMesh();
		}
		return result;
    }
    
    
}
