using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewindObject : MonoBehaviour
{
	bool isRewinding = false;

	public float recordTime = 5f;

	List<StateInTime> statesInTime;
	[SerializeField]
	public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
	public GhostTrail ghostTrail;

    public Animator animator;

	// Use this for initialization
	void Start () {
		statesInTime = new List<StateInTime>();
		if (rb == null){
			rb = GetComponent<Rigidbody2D>();
		}
        if (spriteRenderer == null){
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (animator == null){
            animator = GetComponent<Animator>();
        }
		ghostTrail = this.AddComponent<GhostTrail>();
		ghostTrail.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Backspace))
			StartRewind();
		if (Input.GetKeyUp(KeyCode.Backspace))
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
			transform.localScale = stateInTime.scale;
            spriteRenderer.sprite = stateInTime.sprite;
			rb.velocity = stateInTime.velocity;
			rb.angularVelocity = stateInTime.angularVelocity;
            ghostTrail.enabled = true;
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

		statesInTime.Insert(0, new StateInTime(transform.position, transform.rotation, transform.localScale, spriteRenderer.sprite, rb.velocity, rb.angularVelocity));
	}

	public void StartRewind ()
	{
		isRewinding = true;
		rb.velocity = new Vector3(0f, 0f, 0f);
		rb.isKinematic = true;
	}

	public void StopRewind ()
	{
        if (animator != null){
            animator.enabled = true;
        }
		isRewinding = false;
		rb.isKinematic = false;
		ghostTrail.enabled = false;
	}
}
