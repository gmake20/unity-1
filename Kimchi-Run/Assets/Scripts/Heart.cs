using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart;
    public Sprite OffHeart;

    public SpriteRenderer spriteRenderer;

    public int LineNumber; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Lives >= LineNumber)
        {
            spriteRenderer.sprite = OnHeart;
        }
        else
        {
            spriteRenderer.sprite = OffHeart;
        }
    }
}
