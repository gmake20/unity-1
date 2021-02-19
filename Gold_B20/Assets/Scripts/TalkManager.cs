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

        talkData.Add(3000, new string[] { "평범한 나무상자다" });
        talkData.Add(4000, new string[] { "누군가가 사용했던 흔적이 있는 책상이다." });

        // Quest Talk
        talkData.Add(10+1000, new string[] { "어서와:0" ,
                            "이 마을에 놀라운 전설이 있다는데:1",
                            "오른쪽 호수 쪽에 루도가 알려줄거야.:0"  });

        talkData.Add(11+2000, new string[] { "여어.:0" ,
                            "이 호수의 전설을 들으러 온거야?:1",
                            "그럼 일좀 하나 해주면 좋을텐데....:0",
                            "내 집 근처에 떨어진 동전 좀 주워주었으면...:1"  });

        talkData.Add(20+1000, new string[] { "루도의 동전?.:1" ,
                            "돈을 흘리고 다니면 못쓰지!.:3" ,
                            "나중에 루도에게 한마디 해야겠어.:3" });

        talkData.Add(20+2000, new string[] { "찾으면 꼭 좀 가져다줘:1" });
        talkData.Add(20+5000, new string[] { "근처에서 동전을 찾았다." });
        talkData.Add(21+2000, new string[] { "엇! 찾아줘서 고마워.:2" });

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
        if(!talkData.ContainsKey(id)) {
            if(!talkData.ContainsKey(id-id%10)) {
                return GetTalk(id-id%100,talkIndex);    // Get First Talk
            }
            else {
                return GetTalk(id-id%10,talkIndex); // Get First Quest Talk
            }

        }

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
