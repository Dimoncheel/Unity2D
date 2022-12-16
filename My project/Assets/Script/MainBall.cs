using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBall : MonoBehaviour
{
    [SerializeField]                      // поля [SerializeField] та public
    private float ForceMagnitude = 10f;   // доступні для зміни через UnityEditor
                                          // причому Editor приорітетніше
    [SerializeField]
    private TMPro.TextMeshProUGUI CollisionsTMPro;

    private Rigidbody2D Rigidbody2D;
    private Vector2 ForceDirection;

    void Start()
    {
        Rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");  // "стрілки", ASDW, джойстик
        float inputY = Input.GetAxis("Vertical");    //

        ForceDirection = new Vector2(inputX * ForceMagnitude, inputY * ForceMagnitude);
        Rigidbody2D.AddForce(ForceDirection);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Walls")
        {
            CollisionsTMPro.text = (int.Parse(CollisionsTMPro.text) - 1).ToString();
            return;
        }

        CollisionsTMPro.text = (int.Parse(CollisionsTMPro.text) + 1).ToString();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        CollisionsTMPro.text = (int.Parse(CollisionsTMPro.text) + 2).ToString();
        Debug.Log("Trigger: " + other.gameObject.name);

        other.gameObject.transform.position =
            new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-3.5f, 3.5f));

    }
}
