using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;

    public QuestManager questManager;

    public int talkIndex; 
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    public Image talkImg;

    void Start() 
    {
        Debug.Log(questManager.CheckQuest());
    }

    public string hello() {
        string[] arr = new string[] { "좋은날씨네요." , "안녕하세요." , "오늘도 즐거운 하루~" ,"잘지내고 있죠?" };
        return arr[Random.Range(0,arr.Length)];
    }

    public void Action(GameObject scanObj) {
        scanObject = scanObj;

        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        // talkText.text = "[" + scanObj.name + "] " + hello();

        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc) {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);

        string talkData = talkManager.GetTalk(id+questTalkIndex , talkIndex);
        //Debug.Log(talkData);
        //Debug.Log("index:" + talkIndex);

        // end talk
        if(talkData == null) {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        if(isNpc) {
            var arr = talkData.Split(':');
            talkText.text = arr[0];
            talkImg.sprite = talkManager.GetImage(id,int.Parse(arr[1]));
            talkImg.color = new Color(1,1,1,1);
        }
        else {
            talkText.text = talkData;
            talkImg.color = new Color(1,1,1,0);
        }

        isAction = true;
        talkIndex++;
    }


}
