using UnityEngine;
using System.Collections;
using System;

public class ColorSet {
    private Color _cl1;
    private Color _cl2;

    public ColorSet(Color cl1, Color cl2) {
        _cl1 = cl1;
        _cl2 = cl2;
    }

    public Color getColor(int number) {
        if (number == 1) {
            return _cl1;
        } else {
            return _cl2;
        }
    }
}