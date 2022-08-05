using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAnim : MonoBehaviour
{
    private List<float> listOfChildren = new List<float>();

    public Vector3 offset = new Vector3(-13f,0.0047f,13f);

    float totalX = 0f;
    float totalY = 0f;
    float totalZ = 0f;
    
    int tot = 0;

    float centerX;
    float centerY;
    float centerZ;

    public void Start()
    {
        getBonesPositions();

        CentroidinChildren(this.gameObject);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(centerX, centerY, centerZ);
        sphere.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        sphere.transform.localScale = new Vector3(10, 10, 10);

        //print("//count= "+tot);

    }
    public string getBonesPositions()
    {
        listOfChildren.Clear();

        listOfChildren.Add(gameObject.transform.position.x + offset.x - transform.parent.position.x);
        listOfChildren.Add(gameObject.transform.position.y + offset.y - transform.parent.position.y);
        listOfChildren.Add(gameObject.transform.position.z + offset.z - transform.parent.position.z);

        GetChildRecursive(gameObject);
        
        string combinedString = string.Join(",", listOfChildren.ToArray());

        //print(combinedString);
        return combinedString;
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
                    listOfChildren.Add(child.gameObject.transform.position.x + offset.x - transform.parent.position.x);
                    listOfChildren.Add(child.gameObject.transform.position.y + offset.y - transform.parent.position.y);
                    listOfChildren.Add(child.gameObject.transform.position.z + offset.z - transform.parent.position.z);


                    totalX += child.gameObject.transform.position.x;
                    totalY += child.gameObject.transform.position.y;
                    totalZ += child.gameObject.transform.position.z;

                    centerX = totalX / tot;
                    centerY = totalY / tot;
                    centerZ = totalZ / tot;

                }
                GetChildRecursive(child.gameObject);
            }
        }
    }

    private void CentroidinChildren(GameObject obj)
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
                    tot += 1;
                    //print(child.name+"\n");
                }
                GetChildRecursive(child.gameObject);
            }
        }
    }

}
