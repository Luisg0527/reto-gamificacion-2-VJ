using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rig;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        rig.linearVelocity = new Vector2(xInput * moveSpeed, rig.linearVelocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}