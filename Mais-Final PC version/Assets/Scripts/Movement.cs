using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
	private int score;

	private int time = 120;
	private float endTime;
	private Vector3 initialPos;
	public Text timerText;
	public Text countText;
	public Text winText;
	public GameObject player;
	public GameObject[] arrYellow;
	public GameObject[] arrBlue;
	public GameObject[] arrPurple;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		endTime = Time.time + time;
		initialPos = player.transform.position;
		score = 0;
		SetCountText ();
		winText.text = "";

		arrYellow = GameObject.FindGameObjectsWithTag ("Yellow");
	    arrBlue = GameObject.FindGameObjectsWithTag ("Blue");
	    arrPurple = GameObject.FindGameObjectsWithTag ("Purple");
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
		SetTimerText ();
		if (score >= 53) {
			winText.text = "You Win!\n'x' to play again!";
			if (Input.GetKeyDown ("x"))
				reset ();
		}
    }

	void SetTimerText ()
	{
		timerText.text = "Time: " + (int)(endTime - Time.time);
		if (endTime - Time.time <= 0) 
		{
			timerText.text = "Time Over\n Press x to restart.";
			Time.timeScale = 0.00F;
			if (Input.GetKeyDown ("x"))
				reset ();
		}
	}

	void reset()
	{
		//Resetting of pickups should be done here.
		Time.timeScale = 1.0F;
		player.transform.position = initialPos;
		rb.velocity = Vector3.zero;

		foreach (GameObject go in arrBlue)
			go.SetActive(true);
		Debug.Log (arrBlue.Length);

		foreach (GameObject go in arrYellow)
			go.SetActive(true);
		Debug.Log (arrYellow.Length);

		foreach (GameObject go in arrPurple)
			go.SetActive(true);
		Debug.Log (arrPurple.Length);

		endTime = Time.time + time;
		winText.text = "";
		score = 0;
		SetCountText ();
	}


    void OnTriggerEnter(Collider other)
    {     
		if (other.gameObject.tag == "Yellow") {
			score += 2;
			endTime += 10;
		} else if (other.gameObject.tag == "Purple") {
			score += 1;
			endTime += 5;
		} else if (other.gameObject.tag == "Blue") {
			score += 3;
			endTime += 15;
		}
		other.gameObject.SetActive(false);
		SetTimerText ();
		SetCountText ();
    }

	void SetCountText ()
	{
		countText.text = "Count: " + score.ToString ();
	}
}
