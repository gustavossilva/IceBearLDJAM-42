using UnityEngine;
using Spine.Unity;

public class AttackBehaviour : MonoBehaviour {

	public SkeletonAnimation _skeletonAnimation;
	public AnimationReferenceAsset attack_front, attack_right, attack_back, idle;

	public Spine.TrackEntry trackEntry;
	public bool trackClear = true;

	void Start()
	{
		// trackEntry.End += OnEnd;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.RightArrow) && trackClear)
		{
			trackClear = false;
			trackEntry = _skeletonAnimation.state.SetAnimation(2, attack_right, false);
			// trackEntry.End += OnEnd;
			trackEntry.Complete += OnComplete;
			_skeletonAnimation.state.AddEmptyAnimation(0, 0, .6f);
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow) && trackClear)
		{
			trackClear = false;
			transform.localScale = new Vector2(-1f, transform.localScale.y);
			trackEntry = _skeletonAnimation.state.SetAnimation(2, attack_right, false);
			// trackEntry.End += OnEnd;
			trackEntry.Complete += OnComplete;
			_skeletonAnimation.state.AddEmptyAnimation(0, 0, .6f);
		}

		if(Input.GetKeyDown(KeyCode.UpArrow) && trackClear)
		{
			trackClear = false;
			trackEntry = _skeletonAnimation.state.SetAnimation(2, attack_back, false);
			// trackEntry.End += OnEnd;
			trackEntry.Complete += OnComplete;
			_skeletonAnimation.state.AddEmptyAnimation(0, 0, .6f);
		}

		if(Input.GetKeyDown(KeyCode.DownArrow) && trackClear)
		{
			trackClear = false;
			trackEntry = _skeletonAnimation.state.SetAnimation(2, attack_front, false);
			// trackEntry.End += OnEnd;
			trackEntry.Complete += OnComplete;
			_skeletonAnimation.state.AddEmptyAnimation(0, 0, .6f);
		}
	
		
	}

	// void OnEnd(Spine.TrackEntry e)
	// {
	// 	print("Animation end");
	// 	transform.localScale = new Vector2(1f, transform.localScale.y);
	// 	trackClear = true;
	// }

	void OnComplete(Spine.TrackEntry e)
	{
		print("Animation complete");
		transform.localScale = new Vector2(1f, transform.localScale.y);
		trackClear = true;
		
		// Apply damage
	}
}
