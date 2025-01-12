using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Scroll �ӵ��� �󸶳� ������ϴ°�?")]
    public float scrollSpeed;

    [Header("References")]
    public MeshRenderer meshRenderer;

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * (GameManager.Instance.CalcualteGameSpeed()/20) *Time.deltaTime , 0);
    }
}

