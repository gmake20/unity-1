using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int State = Animator.StringToHash("state");

    [Header("Settings")]
    public float JumpForce;

    [Header("Reference")]
    public Rigidbody2D PlayerRigidBody;
    public Animator PlayerAnimator;
    public BoxCollider2D PlayerCollider;

    public bool isInvincible = false;

    private bool isGrounded = true;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // PlayerRigidBody.linearVelocityX = 10;
            // PlayerRigidBody.linearVelocityY = 20;
            PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger(State, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Platform")
        {
            if(!isGrounded)
            {
                PlayerAnimator.SetInteger(State, 2);
            }
            isGrounded = true;
        }
    }

    public void KillPlayer()
    {
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidBody.AddForceY(JumpForce, ForceMode2D.Impulse);

    }
    
    void Hit()
    {
        GameManager.Instance.Lives -= 1;
        if (GameManager.Instance.Lives == 0)
        {
            KillPlayer();
        }
    }

    void Heal()
    {
        GameManager.Instance.Lives = Mathf.Min(3, GameManager.Instance.Lives + 1);
    }

    void StartInvinclble()
    {
        isInvincible = true;
        Invoke("StopInvinclble", 5f);
    }

    void StopInvinclble()
    {
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(!isInvincible)
            {
                Destroy(collision.gameObject);
                Hit();
            }
        }
        else if (collision.gameObject.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            Heal();
        }
        else if (collision.gameObject.CompareTag("Golden"))
        {
            Destroy(collision.gameObject);
            StartInvinclble();
        }
    }
}
