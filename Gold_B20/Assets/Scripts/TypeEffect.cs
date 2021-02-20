using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public float CharPerSeconds;
    public GameObject endCursor;

    string targetMsg;
    Text msgText;
    int index;

    AudioSource audioSource;
    public bool isAnim; 


    public void Awake() {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg) {
        if(isAnim) { // interrupt
            CancelInvoke();
            msgText.text = targetMsg;
            EffectEnd();
        }
        else {
            targetMsg = msg;
            EffectStart();
        }

    }

    void EffectStart() {
        msgText.text = "";
        index = 0;
        endCursor.SetActive(false);
        isAnim = true;

        Invoke("Effecting",1/CharPerSeconds);
    }

    void Effecting() {
        if(msgText.text == targetMsg) {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();
        index++;
        Invoke("Effecting",1/CharPerSeconds);
    }

    void EffectEnd() {
        isAnim = false;
        endCursor.SetActive(true);
    }


}
