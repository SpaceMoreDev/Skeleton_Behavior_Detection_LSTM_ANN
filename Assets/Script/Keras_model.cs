using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using TMPro;
using WebSocketSharp;

public class Keras_model : MonoBehaviour
{
    [SerializeField]
    private GenerateAnim genAnim;//get bones

    //[SerializeField]
    //private TMP_InputField inputvalue;

    [SerializeField]
    private TMP_Text outputPrediction;


    WebSocket ws;
    //void Start()
    //{
    //    //runtimeModel = ModelLoader.Load(kerasModel);
    //    //worker = WorkerFactory.CreateWorker(WorkerFactory.Type.Auto, runtimeModel);
    //    //outputLayerName = runtimeModel.outputs[runtimeModel.outputs.Count-1];

    //}

    string recmsg = "";

    private void Start()
    {
        ws = new WebSocket("ws://localhost:5555");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("message received from "+((WebSocket)sender).Url + ", Data: "+e.Data);
            recmsg = e.Data;
        };
        ws.Connect();

        InvokeRepeating("test", 0.001f, 0.25f);
    }

    private void Update()
    {
        if (ws == null) return;
    }

    private void test()
    {
        string text = genAnim.getBonesPositions();
        //Debug.Log("a7a - "+text);

        string[] test_arr;
        List<float> float_arr = new List<float>();
        float[] final_arr;

        //test_arr = text.Replace(")(", ",").Replace(")", "").Replace("(", "").Replace(" ", "").Split(',');

        //Debug.Log("++++++++++++++++++++++++++++"+ inputvalue.text.Replace(")(", ",").Replace(")", "").Replace("(", ""));

        //for (int i = 0; i < test_arr.Length; i++)
        //{
        //    float temp = float.Parse(test_arr[i]);
        //    //print(">>" + temp);
        //    float_arr.Add(temp);
        //}

        //final_arr = float_arr.ToArray();

        print(text);
        ws.Send(text);

        switch (recmsg)
        {
            case ("1"):
                outputPrediction.text = "Walking";
                break;

            case ("2"):
                outputPrediction.text = "Punching";
                break;

            case ("3"):
                outputPrediction.text = "Jumping";
                break;

            case ("4"):
                outputPrediction.text = "Crouching";
                break;

            case ("5"):
                outputPrediction.text = "Running";
                break;

            default:
                outputPrediction.text = "Idle";
                break;

        }


        //print(prediction);
    }
    //public void Predict(float[] boneinput)
    //{

    //    PredictionRequester pr = new PredictionRequester();

    //    client.Predict(boneinput, output =>
    //    {

    //        var outputMax = output.Max();
    //        var maxIndex = Array.IndexOf(output, outputMax);
    //        prediction = "Prediction: " + maxIndex;

    //    }, error =>
    //    {
    //        print("I see nothing");
    //    });
    //}
}
