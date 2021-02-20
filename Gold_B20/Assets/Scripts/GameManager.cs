using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;

    public QuestManager questManager;

    public Animator animPanel;
    public Animator animPortrait;
    public int talkIndex; 
    public GameObject talkPanel;
    // public Text talkText;
    public TypeEffect talk;

    public GameObject scanObject;
    public bool isAction;

    public Image talkImg;

    public Sprite prevPortrait;

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

        // talkPanel.SetActive(isAction);
        animPanel.SetBool("isShow",isAction);
    }

    void Talk(int id, bool isNpc) {
        int questTalkIndex = 0;
        string talkData = "";
        if(talk.isAnim) {
            talk.SetMsg("");
            return;
        }
        else {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id+questTalkIndex , talkIndex);
        }
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
            talk.SetMsg(arr[0]);
            // talkText.text = arr[0];
            talkImg.sprite = talkManager.GetImage(id,int.Parse(arr[1]));
            talkImg.color = new Color(1,1,1,1);
            if(prevPortrait != talkImg.sprite) {
                animPortrait.SetTrigger("doEffect");
                prevPortrait = talkImg.sprite;
            }
        }
        else {
            talk.SetMsg(talkData);
            // talkText.text = talkData;
            talkImg.color = new Color(1,1,1,0);
        }

        isAction = true;
        talkIndex++;
    }


}
