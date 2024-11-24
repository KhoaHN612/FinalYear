using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewindAgent : MonoBehaviour
{
	bool isRewinding = false;

	public float recordTime = 5f;

	List<StateInTime> statesInTime;
	[SerializeField]
	public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
	public GhostTrail ghostTrail;
	public GameObject rewindEffect;

	// Use this for initialization
	void Start () {
		statesInTime = new List<StateInTime>();
		if (rb == null){
			rb = GetComponentInChildren<Rigidbody2D>();
		}
        if (spriteRenderer == null){
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

		ghostTrail = this.AddComponent<GhostTrail>();
		ghostTrail.enabled = false;
	}	

	void FixedUpdate ()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind ()
	{
		if (statesInTime.Count > 0)
		{		
			StateInTime stateInTime = statesInTime[0];
            transform.position = stateInTime.position;
			transform.rotation = stateInTime.rotation;
			transform.localScale = stateInTime.scale;
            spriteRenderer.sprite = stateInTime.sprite;
            rb.velocity = stateInTime.velocity;
			rb.angularVelocity = stateInTime.angularVelocity;
            ghostTrail.enabled = true;
			statesInTime.RemoveAt(0);
		} 
		else
		{
			return;
		}
		
	}

	void Record ()
	{
		if (statesInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			statesInTime.RemoveAt(statesInTime.Count - 1);
		}

		statesInTime.Insert(0, new StateInTime(transform.position, transform.rotation, transform.localScale, spriteRenderer.sprite, rb.velocity, rb.angularVelocity));
	}

	public void StartRewind ()
	{
		if (rewindEffect != null){
			rewindEffect.SetActive(true);
		}
		isRewinding = true;
		//rb.velocity = new Vector3(0f, 0f, 0f);
		rb.isKinematic = true;
	}

	public void StopRewind ()
	{
		if (rewindEffect != null){
			rewindEffect.SetActive(false);
		}
		isRewinding = false;
		rb.isKinematic = false;
		ghostTrail.enabled = false;
	}
}
