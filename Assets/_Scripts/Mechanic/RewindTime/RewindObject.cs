using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour
{
	bool isRewinding = false;

	public float recordTime = 5f;

	List<StateInTime> statesInTime;

	Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    public Animator animator;

	// Use this for initialization
	void Start () {
		statesInTime = new List<StateInTime>();
		rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null){
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (animator == null){
            animator = GetComponent<Animator>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
			StartRewind();
		if (Input.GetKeyUp(KeyCode.P))
			StopRewind();
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
            if (animator != null){
                animator.enabled = false;
            }
			StateInTime stateInTime = statesInTime[0];
            transform.position = stateInTime.position;
			transform.rotation = stateInTime.rotation;
            spriteRenderer = stateInTime.spriteRenderer;
			statesInTime.RemoveAt(0);
		} else
		{
			StopRewind();
		}
		
	}

	void Record ()
	{
		if (statesInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			statesInTime.RemoveAt(statesInTime.Count - 1);
		}

		statesInTime.Insert(0, new StateInTime(transform.position, transform.rotation, spriteRenderer));
	}

	public void StartRewind ()
	{
		isRewinding = true;
		rb.isKinematic = true;
	}

	public void StopRewind ()
	{
        if (animator != null){
            animator.enabled = true;
        }
		isRewinding = false;
		rb.isKinematic = false;
	}
}
