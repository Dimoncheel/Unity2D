using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
    
{
    public static float PipeShift = 2;   

    [SerializeField]
    private float JumpMagnitude = 10f;

    private Rigidbody2D Rigidbody2D;
    private Vector2 jumpForce;
    private float holdTime;
    private GameStat gameStat;   
    private GameMenu gameMenu;
    Vector2 pos;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        gameStat =                        
            GameObject.Find("GameStat")   
            .GetComponent<GameStat>();

        gameMenu =
            GameObject.Find("GameMenu")
            .GetComponent<GameMenu>();
        jumpForce = Vector2.up * JumpMagnitude;
        holdTime = 0;
        Vector2 pos = this.transform.position;
    }

    void Update()
    {
        float jump;

        if (GameMenu.ControlType == 0)             
        {
            jump = Input.GetAxis("Jump");
            jump *= Time.deltaTime * 100;
            Rigidbody2D.AddForce(jumpForce * jump);
        }
        else if (GameMenu.ControlType == 1)          
        {
            jump = Input.GetKeyDown(KeyCode.Space) ? 20 : 0;
            jump *= Time.deltaTime * 100;
            Rigidbody2D.AddForce(jumpForce * jump);
        }
        else                                       
        {
            jump = 1.5f;
            if (Input.GetKey(KeyCode.Space))
            {
                if (holdTime == 0) holdTime = 1;
                if (holdTime > 0) holdTime -= Time.deltaTime;
            }
            else holdTime = 0;
            jump *= Time.deltaTime * 100;  
            if (holdTime > 0) Rigidbody2D.AddForce(jumpForce * jump);
        }
        if (jump > 0)   
        {
        
            gameStat.GameEnergy -= jump / 5000;
        }
    }

    private void LateUpdate()
    {
        
        this.transform.rotation = Quaternion.Euler(0, 0, 3 * Rigidbody2D.velocity.y);

        if (Rigidbody2D.velocity.x != 0) 
        {
            Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Energy"))   
        {
            gameStat.GameScore += 1;

            if (gameStat.GameEnergy < 0.5f) gameStat.GameEnergy += 0.5f;
            else gameStat.GameEnergy = 1;

            GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Heart"))
        {
           

            GameObject.Destroy(other.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D other)  
    {
        if (other.gameObject.CompareTag("Tube"))   
        {
            gameStat.GameScore += 1;
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Pipe"))
        {
            gameStat.GetDamage();
            GameObject.Destroy(collision.gameObject);
            this.transform.position = new Vector2((float)-4.07,(float)0.31);
        }
        if (collision.gameObject.CompareTag("OutOf"))
        {
            gameStat.GameEnergy -= 0.2f;
        }
    }
}
