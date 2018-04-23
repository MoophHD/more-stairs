using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{   
    public Animator animator;
    private Text popUpText;
    public float animationLength;
    void Start() {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        animationLength = clipInfo[0].clip.length;

        Destroy(gameObject, animationLength - 0.05f);

        popUpText = animator.GetComponent<Text>();
        popUpText.text = "+ " + Score.SCORE_PER_POINT_UP.ToString();
    }
}