using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	public Vector3 jump;
    public float jumpForce = 2.0f;
	public bool isGrounded;

        private float movementX;
        private float movementY;

	private Rigidbody rb;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;

		jump = new Vector3(0.0f, 2.0f, 0.0f);

		SetCountText ();
        {

            winTextObject.SetActive(false);
        }
    }

	void OnCollisionEnter(){
        isGrounded = true;
    }

	void FixedUpdate()
	{
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

		rb.AddForce (movement * speed);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
		}
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
		if (other.gameObject.CompareTag ("RespawnDetector"))
		{	
			transform.position = new Vector3(0f, 0.5f, 0f);
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}

    void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

    void SetCountText()
	{
		countText.text = "Score: " + count.ToString();

		if (count >= 12) 
		{
            winTextObject.SetActive(true);
		}
	}
}