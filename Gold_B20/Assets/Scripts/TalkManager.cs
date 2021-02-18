using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int,Sprite> talkImgData;

    public Sprite[] imgArr;

    void Awake() {
        talkData = new Dictionary<int, string[]>();
        talkImgData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    public void GenerateData() {
        talkData.Add(1000, new string[] { "안녕?:0" , "이 곳에 처음 왔구나?:1"});
        talkData.Add(2000, new string[] { "여어~:1" ,"이 호수는 정말 아름답지?:0", "사실 이 호수에는 무언가의 비밀이 숨겨져 있다고해.:1"});

        talkData.Add(100, new string[] { "평범한 나무상자다" });
        talkData.Add(200, new string[] { "누군가가 사용했던 흔적이 있는 책상이다." });

        talkImgData.Add(1000 + 0,imgArr[0]);
        talkImgData.Add(1000 + 1,imgArr[1]);
        talkImgData.Add(1000 + 2,imgArr[2]);
        talkImgData.Add(1000 + 3,imgArr[3]);
        talkImgData.Add(2000 + 0,imgArr[4]);
        talkImgData.Add(2000 + 1,imgArr[5]);
        talkImgData.Add(2000 + 2,imgArr[6]);
        talkImgData.Add(2000 + 3,imgArr[7]);
    }

    public string GetTalk(int id, int talkIndex) {
        // Debug.Log(id+":" + talkIndex);
        if(talkIndex == talkData[id].Length)
            return null;
        else
             return talkData[id][talkIndex];
    }

    public Sprite GetImage(int id,int imgIndex) {
        return talkImgData[id+imgIndex];
    }

}
