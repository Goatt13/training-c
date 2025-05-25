using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(inputX, inputY);
    }
}
