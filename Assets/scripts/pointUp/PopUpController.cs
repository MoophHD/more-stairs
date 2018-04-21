using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    public static GameObject canvas;
    private static PopUp popUp;

    public static void init() {
        popUp = Resources.Load<PopUp>("PopUpParent");
        canvas = GameObject.Find("popUpTextCanvas");
        print(popUp);
    }

    public static PopUp createPopUp(Vector3 pos) {
        PopUp instance = Instantiate(popUp);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);
        instance.transform.SetParent(canvas.transform, false);

        instance.transform.position = screenPos;

        return instance;
    }

    // public static void createPopUp(Vector3 pos)
    // {
    //     PopUp instance = Instantiate(popUp);
    //     Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);
    //     instance.transform.SetParent(canvas.transform, false);

    //     instance.transform.position = screenPos;
    // }
}