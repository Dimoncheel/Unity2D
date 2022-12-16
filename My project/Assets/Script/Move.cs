using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float ForceMagnitude = 10f;

    private Rigidbody2D Rigidbody2D;
    private Vector2 ForceDirection;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        ForceDirection=new Vector2(inputX*ForceMagnitude, inputY*ForceMagnitude);
        Rigidbody2D.AddForce(ForceDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collider");
    }
}
