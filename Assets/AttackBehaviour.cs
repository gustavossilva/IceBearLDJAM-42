using UnityEngine;
using Spine.Unity;

public class AttackBehaviour : MonoBehaviour {

	public SkeletonAnimation _skeletonAnimation;
	public AnimationReferenceAsset attack_front, attack_right, attack_back;

	public Spine.TrackEntry trackEntry;
	public bool trackClear = true;

	private IcePosition icePosition;

	void Start()
	{
		// trackEntry.End += OnEnd;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.RightArrow) && trackClear)
		{
			PlayerManager.Instance.attacking = true;
			trackClear = false;
			trackEntry = _skeletonAnimation.state.SetAnimation(2, attack_right, false);
			//play SFX 
			// trackEntry.End += OnEnd;
			icePosition = IcePosition.ICE_RIGHT;
			trackEntry.Complete += OnComplete;
			_skeletonAnimation.state.AddEmptyAnimation(2, 0.2f, _skeletonAnimation.state.GetCurrent(2).AnimationTime+1f);
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow) && trackClear)
		{
			PlayerManager.Instance.attacking = true;
			trackClear = false;
			_skeletonAnimation.transform.localScale = new Vector2(-1f, transform.localScale.y);
			trackEntry = _skeletonAnimation.state.SetAnimation(2, attack_right, false);
			// trackEntry.End += OnEnd;
			icePosition = IcePosition.ICE_LEFT;
			trackEntry = _skeletonAnimation.state.AddEmptyAnimation(2, 0.2f, _skeletonAnimation.state.GetCurrent(2).AnimationTime+1f);
			trackEntry.Complete += OnLeftComplete;
		}

		if(Input.GetKeyDown(KeyCode.UpArrow) && trackClear)
		{
			PlayerManager.Instance.attacking = true;
			trackClear = false;
			trackEntry = _skeletonAnimation.state.SetAnimation(2, attack_back, false);
			trackEntry.mixDuration = 0f;
			// trackEntry.End += OnEnd;
			icePosition = IcePosition.ICE_BACK;
			trackEntry.Complete += OnComplete;
			_skeletonAnimation.state.AddEmptyAnimation(2, 0.2f, _skeletonAnimation.state.GetCurrent(2).AnimationTime+1f);
		}

		if(Input.GetKeyDown(KeyCode.DownArrow) && trackClear)
		{
			PlayerManager.Instance.attacking = true;
			trackClear = false;
			trackEntry = _skeletonAnimation.state.SetAnimation(2, attack_front, false);
			trackEntry.mixDuration = 0f;
			// trackEntry.End += OnEnd;
			icePosition = IcePosition.ICE_FRONT;
			trackEntry.Complete += OnComplete;
			_skeletonAnimation.state.AddEmptyAnimation(2, 0.2f, _skeletonAnimation.state.GetCurrent(2).AnimationTime+1f);
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
		// print("Animation complete");

		trackClear = true;
		
		Tubarao tubarao = null;

		// Kill shark if there is one
		if(SpawnerInimigo.dic.TryGetValue(icePosition, out tubarao))
		{
			tubarao.isAlive = false;
			SpawnerInimigo.dic.Remove(icePosition);
		}
		PlayerManager.Instance.attacking = false;
	}

	void OnLeftComplete(Spine.TrackEntry e)
	{
		_skeletonAnimation.transform.localScale = new Vector2(1f, transform.localScale.y);

		trackClear = true;
		
		Tubarao tubarao = null;

		// Kill shark if there is one
		if(SpawnerInimigo.dic.TryGetValue(icePosition, out tubarao))
		{
			tubarao.isAlive = false;
			SpawnerInimigo.dic.Remove(icePosition);
		}
		PlayerManager.Instance.attacking = false;
	}
}
