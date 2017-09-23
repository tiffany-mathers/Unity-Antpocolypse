using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ant_Behavior : MonoBehaviour {
    // instantiate variables
    public int playerSpeed = 10;
    public int jumpPower = 15;
    private float moveX;
    private float moveY;
    private bool facingRight = true;
    public int food = 0;
    private int pointsPerFood = 5;
    private int pointsPerSoda = 10;
    private int pointsPerAcorn = 5;
    public Text FoodText;
    public Text ExitText;
    public Text DeadText;
    public bool enemy = false;
    public bool exit;

    private void Start()
    {
        ExitText.text = "";
        DeadText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // moves ant
        antMove();

        // flips ant by directional movement
        if (moveX < 0 && facingRight == true)
        {
            flipAnt();
        } else if (moveX > 0 && facingRight == false)
        {
            flipAnt();
        }

        // enables the ant to jump
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
        SetFoodText();
    }

    void antMove()
    {
        // Move ant left or right   
        moveX = Input.GetAxis("Horizontal");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void flipAnt()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Jump()
    {
        transform.Translate(Vector3.up * jumpPower * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            food += pointsPerFood;
            other.gameObject.SetActive(false);
            SetFoodText();
        }

        else if (other.tag == "Soda")
        {
            food += pointsPerSoda;
            other.gameObject.SetActive(false);
            SetFoodText();
        }

        else if (other.tag == "Acorn")
        {
            food += pointsPerAcorn;
            other.gameObject.SetActive(false);
            SetFoodText();
        }

        else if (other.tag == "Enemy")
        {
            SetDeadText();
            Time.timeScale = 0f;
        }

        else if (other.tag == "Tunnel")
        {
            SetExitText();
        }
    }

    void SetFoodText()
    {
        FoodText.text = "Food: " + food.ToString();
    }

    void SetExitText()
    {
        if (food < 50)
        {
            ExitText.text = "You need to collect more food!";
        }
        else if (food >= 50)
        {
            ExitText.text = "Congrats, you won!";
            Time.timeScale = 0f;
        }
    }

    void SetDeadText()
    {
        ExitText.text = "Whoops, you died!";
    }
}
