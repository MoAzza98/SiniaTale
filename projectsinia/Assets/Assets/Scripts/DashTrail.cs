using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashTrail : MonoBehaviour
{
	public SpriteRenderer mLeadingSprite;

	public int mTrailSegments;
	public float mTrailTime;
	public GameObject mTrailObject;

	private float mSpawnInterval;
	private float mSpawnTimer;
	private bool mbEnabled;

	private List<GameObject> mTrailObjectsInUse;
	private Queue<GameObject> mTrailObjectsNotInUse;

	// Use this for initialization
	void Start()
	{
		mSpawnInterval = mTrailTime / mTrailSegments;
		mTrailObjectsInUse = new List<GameObject>();
		mTrailObjectsNotInUse = new Queue<GameObject>();

		for (int i = 0; i < mTrailSegments; i++)
		{
			GameObject trail = Instantiate(mTrailObject);
			trail.transform.SetParent(transform);
			mTrailObjectsNotInUse.Enqueue(trail);
		}

		mbEnabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (mbEnabled)
		{
			mSpawnTimer += Time.deltaTime;

			if (mSpawnTimer >= mSpawnInterval)
			{
				GameObject trail = mTrailObjectsNotInUse.Dequeue();
				if (trail != null)
				{
					DashTrailObject trailObject = trail.GetComponent<DashTrailObject>();

					trailObject.Initiate(mTrailTime, mLeadingSprite.sprite, transform.position, this);
					mTrailObjectsInUse.Add(trail);

					mSpawnTimer = 0;
				}
			}
		}
	}

	public void RemoveTrailObject(GameObject obj)
	{
		mTrailObjectsInUse.Remove(obj);
		mTrailObjectsNotInUse.Enqueue(obj);
	}

	public void SetEnabled(bool enabled)
	{
		mbEnabled = enabled;

		if (enabled)
		{
			mSpawnTimer = mSpawnInterval;
		}
	}

}