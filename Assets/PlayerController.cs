using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {

	public GameObject StartPanel;
	public GameObject OverPanel;
	bool playing;
	void Start()  //S一定要大写
	{
		playing=false;
		StartPanel.SetActive(true);
		OverPanel.SetActive(false);
	}
	public float Speed;
	public float Range;
	public GameObject boxPrefab;
	public Vector2 moveDir = Vector2.right;
	public float timer;
	public float interval=0.5f;
	public Text scoreText;
	public Text overScoreText;
	public int score = 0;
	public AudioClip drop;
	public AudioClip over;
	// Update is called once per frame
	void Update () {

		if (!playing) return;
		if(transform.position.x > Range)
		moveDir = Vector2.left;
		if(transform.position.x < -Range)
		moveDir = Vector2.right;

		Vector2 moveAmount = moveDir * Speed * Time.deltaTime;
		transform.Translate(moveAmount);

		timer += Time.deltaTime;
		//Box
		if(Input.GetKeyDown(KeyCode.Mouse0) && timer>interval)
		{
			SpawnBox();
			timer = 0;
			StartCoroutine(Up());
		}
	}

	void SpawnBox()
	{
		Instantiate(boxPrefab, transform.position + Vector3.down * 0.9f, boxPrefab.transform.rotation);
		StartCoroutine(FindObjectOfType<CameraFollow>().exView());
		score++;
		scoreText.text = "SCORE : " + score;
		Speed += 0.5f;
		AudioSource.PlayClipAtPoint(drop,transform.position);
	}
	
	IEnumerator Up()
	{
		float tar = 1.5f, tmp = 0;
		while (tmp < tar)
		{
			tmp += Time.deltaTime * (1 / interval);
			transform.position += Vector3.up * Time.deltaTime;

			yield return null;
		}
	}

	public void gameStart()
	{
		playing=true;
		StartPanel.SetActive(false);
		scoreText.text = "SCORE : " + score;
	}
	public bool isOver = false;
	public void gameOver()
	{
		if (isOver == true) return;
		else isOver = true;
		
		playing=false;
		scoreText.text = "";
		overScoreText.text = "SCORE : " + score;
		OverPanel.SetActive(true);

		FindObjectOfType<CameraFollow>().stopBGM();
		AudioSource.PlayClipAtPoint(over,transform.position);
	}
	public void restart()
	{
		SceneManager.LoadScene(0);
	}
}
