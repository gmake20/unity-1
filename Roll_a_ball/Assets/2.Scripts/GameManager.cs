using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text txtLevel;

    public Text txtStageCount;

    public int totalItemCount;
    public int stage;
    
    void Start()
    {
        Debug.Log(stage);
        txtLevel.text = "Level : " + stage;

        
        UpdateItemCount(0);
    }

    public void UpdateItemCount(int count) {
        txtStageCount.text = count + " / " + totalItemCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            SceneManager.LoadScene(stage);
        }
    }
}
