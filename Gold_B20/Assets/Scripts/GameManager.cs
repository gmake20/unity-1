using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    public string hello() {
        string[] arr = new string[] { "좋은날씨네요." , "안녕하세요." , "오늘도 즐거운 하루~" ,"잘지내고 있죠?" };
        return arr[Random.Range(0,arr.Length)];
    }

    public void Action(GameObject scanObj) {
        if(isAction) {
            isAction = false; 
        }
        else {
            isAction = true;
            scanObject = scanObj;

            
            talkText.text = "[" + scanObj.name + "] " + hello();
        }
        talkPanel.SetActive(isAction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
