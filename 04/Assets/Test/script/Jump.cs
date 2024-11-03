using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;
    bool isJumping;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isJumping = true;

            // 効果音を再生
            SoundManager.Instance.PlayJumpSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}