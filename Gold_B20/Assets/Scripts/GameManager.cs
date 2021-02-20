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
    public Text talkname;
    public Text questText;

    public GameObject scanObject;
    public bool isAction;

    public Image talkImg;

    public Sprite prevPortrait;

    public GameObject menuSet;
    // public GameObject questSet;
    public GameObject player;

    void Start() 
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    public string hello() {
        string[] arr = new string[] { "좋은날씨네요." , "안녕하세요." , "오늘도 즐거운 하루~" ,"잘지내고 있죠?" };
        return arr[Random.Range(0,arr.Length)];
    }

    public void Action(GameObject scanObj) {
        scanObject = scanObj;

        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc, scanObj.name);
        // talkText.text = "[" + scanObj.name + "] " + hello();

        // talkPanel.SetActive(isAction);
        animPanel.SetBool("isShow",isAction);
    }

    void Talk(int id, bool isNpc, string name) {
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
            questText.text = questManager.CheckQuest(id);

            return;
        }

        if(isNpc) {
            talkname.text = name;
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
            talkname.text = "";
            talk.SetMsg(talkData);
            // talkText.text = talkData;
            talkImg.color = new Color(1,1,1,0);
        }

        isAction = true;
        talkIndex++;
    }


    void Update() {
        if(Input.GetButtonDown("Cancel")) {
            SubMenuActive();
        }
    }

    public void SubMenuActive() {
        if(menuSet.activeSelf) 
            menuSet.SetActive(false);
        else
            menuSet.SetActive(true);
    }

    public void GameLoad() {
        if(!PlayerPrefs.HasKey("PlayerX"))
            return;
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x,y,0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameSave() {
        PlayerPrefs.SetFloat("PlayerX",player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY",player.transform.position.y);
        PlayerPrefs.SetInt("QuestId",questManager.questId);;
        PlayerPrefs.SetInt("QuestActionIndex",questManager.questActionIndex);
        PlayerPrefs.Save();
        // player x,y;
        // quest id
        // quest action index
    }

    public void GameExit() {
        Application.Quit();
    }

}
