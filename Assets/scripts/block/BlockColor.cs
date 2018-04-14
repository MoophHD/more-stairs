using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColor : MonoBehaviour {
    public Material mat;
    const float TIME_BEFORE_CHANGE = 5f;
    private float changeTimer = 0f;
    const float LERP_TIME = 1f;
    private float t = 1;

    private List<int> options = new List<int> { 0, 1, 2 };

    // FFC3E2
    private Color pink = new Color(255/255f, 195/255f, 226/255f);
    // 4800FF
    private Color blue = new Color(72/255f, 0f, 255/255f);
    //FFC0A3
    private Color orange = new Color(255/255f, 192/255f, 163/255f);
    //A34D0B
    private Color brown = new Color(163/255f, 77/255f, 11/255f);
    //C1FFFC
    private Color cyan = new Color(193/255f, 255/255f, 252/255f);
    //183A04
    private Color green = new Color(24/255f, 58/255f, 4/255f);
    private ColorSet pinkBlue;
    private ColorSet orangeBrown;
    private ColorSet cyanGreen;
    private ColorSet[] colors = new ColorSet[3];
    private Color _clFrom;
    private Color _emmFrom;
    private Color _clTo;
    private Color _emmTo;
    void Start() {
        pinkBlue = new ColorSet(pink, blue);
        orangeBrown = new ColorSet(orange, brown);
        cyanGreen = new ColorSet(cyan, green);
        colors[0] = pinkBlue;
        colors[1] = orangeBrown;
        colors[2] = cyanGreen;

        shuffle(options);
    }

    public void reset() {
        changeTimer = 0;
        int option = getOption();
        ColorSet clSet = colors[option];
        
        mat.SetColor("_Color", clSet.getColor(1));
        mat.SetColor("_EmissionColor", clSet.getColor(2));
    }

    private void setColor(ColorSet clSet) {
        _clFrom = mat.GetColor("_Color");
        _emmFrom = mat.GetColor("_EmissionColor");

        _clTo = clSet.getColor(1);
        _emmTo = clSet.getColor(2);

        t = 0;
    }

    void Update() {
        changeTimer += Time.deltaTime;

        if (changeTimer > TIME_BEFORE_CHANGE) {
            changeTimer = 0;
            setColor(colors[getOption()]);
        }
        
        if (t < 1) {
            t += Time.deltaTime / LERP_TIME;

            Color newCl = Color.Lerp( _clFrom, _clTo, t );
            Color newEmm = Color.Lerp( _emmFrom, _emmTo, t );
            mat.SetColor("_Color", newCl);
            mat.SetColor("_EmissionColor", newEmm);
        }
    }

    int getOption() {
        int option = options[0];
        options.Remove(option);

        if (options.Count == 0) {
            options = new List<int> { 0, 1, 2 };

            shuffle(options);
        }

        return option;
    }

    //https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
    void shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

}