using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;
using System;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float numCoins;
    public int maxJumps = 2;
    public Text CoinCount;
    public Text winText;
    public Text LivesCount;

    private Rigidbody rb;
    private int coinCount;
    private int livesCount;
    private int jumpCount;
    [HideInInspector]
    public Checkpoint LastCheckpoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coinCount = 0;
        jumpCount = 0;
        livesCount = 5;
        SetCountText();
        SetLivesText();
    }

    internal void Respawn()
    {
        if (LastCheckpoint != null)
        {
            var p = LastCheckpoint.transform.position;
            p.y += 1; 
            transform.position = p;
            livesCount--;
            SetLivesText();
        }
        else
        {
            Debug.Log("geen checkpoint");
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coinCount = coinCount + 1;
            SetCountText();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }
    }
    void SetCountText()
    {
        CoinCount.text = "Count: " + coinCount.ToString();
        if (coinCount >= numCoins)
        {
            winText.gameObject.SetActive(true);
            GameOver();
        }
        else
        {
            winText.gameObject.SetActive(false);
        }
    }

    void SetLivesText()
    {
        LivesCount.text = $"Lives: {livesCount}";
        if (livesCount == 0)
        {
            winText.gameObject.SetActive(true);
            winText.text = "You lost!";
            GameOver();

        }
    }
    void GameOver()
    {
        enabled = false;
        rb.velocity = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            if (jumpCount < maxJumps)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++;
            }
        }
    }
} 