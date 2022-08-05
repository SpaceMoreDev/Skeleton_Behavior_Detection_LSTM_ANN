using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rig_represent : MonoBehaviour
{
    public string rig = "(17.10848617553711,94.95634460449219,5.342008113861084)(10.931397438049316,106.81322479248047,0.7976403832435608)(14.986125946044922,117.48683166503906,0.5697858929634094)(19.49347686767578,129.73500061035156,0.46764472126960754)(24.41238784790039,143.5694122314453,0.5308116674423218)(24.380931854248047,149.60580444335938,4.082103729248047)(23.8837833404541,166.7809295654297,17.63715171813965)(28.450838088989258,140.3310546875,4.2444071769714355)(36.9424934387207,135.96302795410156,13.074228286743164)(44.21297836303711,134.10287475585938,37.50851821899414)(19.490028381347656,143.54513549804688,-3.513507604598999)(9.473937034606934,143.01571655273438,-11.793068885803223)(-14.2371187210083,134.31228637695312,-7.803450584411621)(14.826793670654297,91.09082794189453,6.446521282196045)(14.549480438232422,48.99721908569336,14.12883472442627)(17.321195602416992,10.630157470703125,-3.1601457595825195)(21.81655502319336,0.752777099609375,8.843802452087402)(24.032066345214844,0.7608670592308044,14.640743255615234)(0.8001566529273987,93.13951110839844,-1.3079776763916016)(-12.441247940063477,53.277732849121094,6.78419303894043)(-15.961978912353516,12.109046936035156,-1.6935176849365234)(-20.44133186340332,1.2791690826416016,9.5782470703125)(-23.069875717163086,0.7257642149925232,15.143938064575195)";
    string[] test_arr;
    List<float> float_arr = new List<float>();
    float[] final_arr;

    List<Vector3> listOfChildren = new List<Vector3>();
    float totX, totY, totZ = 0;

    void Start()
    {

        test_arr = rig.Replace(")(", ",").Replace(")", "").Replace("(", "").Replace(" ", "").Split(',');

        for (int i = 0; i < test_arr.Length; i++)
        {
            float temp = float.Parse(test_arr[i]);
            float_arr.Add(temp);
        }

        final_arr = float_arr.ToArray();

        for (int i = 0; i < final_arr.Length; i+=3)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(float_arr[i], float_arr[i+1], float_arr[i+2]);
            sphere.transform.localScale = new Vector3(10,10,10);


            Vector3 pnn = new Vector3(
                        sphere.transform.position.x,
                        sphere.transform.position.y,
                        sphere.transform.position.z
                        );
            listOfChildren.Add(pnn);
        }

        for (int i = 0; i < listOfChildren.Count; i++)
        {
            totX += listOfChildren[i].x / listOfChildren.Count;
            totY += listOfChildren[i].y / listOfChildren.Count;
            totZ += listOfChildren[i].z / listOfChildren.Count;
        }


        Vector3 centroid = new Vector3(totX, totY, totZ);

        GameObject cen = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cen.transform.position = new Vector3(centroid.x, centroid.y, centroid.z);
        cen.transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        cen.transform.localScale = new Vector3(30, 30, 30);
    }
}
