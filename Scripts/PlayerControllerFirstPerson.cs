using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float MovementX;
    private float MovementY;
    public float speed = 10.0f; // Añadir una variable de velocidad pública
    public TextMeshProUGUI countText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        Debug.Log("Move: " + inputVector);

        MovementX = inputVector.x;
        MovementY = inputVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(MovementX, 0.0f, MovementY);

        // Move the player
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
    }
}