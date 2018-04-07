using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signals : MonoBehaviour {

    public delegate void pauseDel(bool isPaused);
    public static event pauseDel onPause;
    public static void setPaused(bool isPaused) { onPause(isPaused); }
}
