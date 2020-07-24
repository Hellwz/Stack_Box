using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public GameObject effectPrefab;
	bool effect; //只产生一次效果
	void OnCollisionEnter2D(Collision2D col)
	{
		if (!effect)
		{
			effect = true;
			GameObject hit = Instantiate(effectPrefab, transform.position, effectPrefab.transform.rotation);
			Destroy(hit, 2f);
		}

		if (col.gameObject.CompareTag("Ground"))
			FindObjectOfType<PlayerController>().gameOver();
	}
}
