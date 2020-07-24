using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float followSpeed;
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		float tarPos = target.position.y;
		float nowPos = transform.position.y;

		Vector2 moveAmount = ((tarPos - nowPos) * Vector2.up).normalized * followSpeed * Time.deltaTime;
		if (Mathf.Abs(tarPos - nowPos) > 0.1f)
		{
			transform.Translate(moveAmount);
		}
	}

    public float exSize = 0.3f;
	public IEnumerator exView()
	{
		float tmp = 0;
		while (tmp < exSize)
		{
			tmp += Time.deltaTime / 2;
			Camera.main.orthographicSize += Time.deltaTime / 2;

			yield return null;
		}
	}

	public void stopBGM()
	{
		AudioSource AS = gameObject.GetComponent<AudioSource>();
		AS.Stop();
	}
}
