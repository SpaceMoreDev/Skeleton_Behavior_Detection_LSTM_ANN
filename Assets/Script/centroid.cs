using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centroid : MonoBehaviour
{

    List<Vector3> listOfChildren = new List<Vector3>();

    float totX, totY, totZ=0;
    GameObject cen;
    private void Start()
    {
        cen = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cen.transform.position = new Vector3(0, 0, 0);
        cen.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        cen.transform.localScale = new Vector3(30, 30, 30);

        InvokeRepeating("test", 0.001f, 0.25f);

    }


    private void test()
    {
        listOfChildren.Clear();
        totX = 0;
        totY = 0; 
        totZ = 0;

        GetChildRecursive(this.gameObject);

        for (int i = 0; i < listOfChildren.Count; i++)
        {
            totX += listOfChildren[i].x/ listOfChildren.Count;
            totY += listOfChildren[i].y / listOfChildren.Count;
            totZ += listOfChildren[i].z / listOfChildren.Count;
        }


        Vector3 centroid = new Vector3(totX, totY, totZ);
        cen.transform.position = centroid;



    }
    private void GetChildRecursive(GameObject obj)
    {
        if (null == obj)
            return;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            else
            {
                if (!child.name.Contains("hand") && !child.name.Contains("Hand"))
                {
                    Vector3 pnn = new Vector3(
                        child.gameObject.transform.position.x,
                        child.gameObject.transform.position.y,
                        child.gameObject.transform.position.z
                        );
                    listOfChildren.Add(pnn);
                }
                GetChildRecursive(child.gameObject);
            }
        }
    }

}
