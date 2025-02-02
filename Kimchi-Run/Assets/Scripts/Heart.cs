using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite onHeart;
    public Sprite offHeart;

    public SpriteRenderer spriteRenderer;

    public int lineNumber; 

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Lives >= lineNumber)
        {
            spriteRenderer.sprite = onHeart;
        }
        else
        {
            spriteRenderer.sprite = offHeart;
        }
    }
}
