using UnityEngine;
using System.Collections;

public class AnimatorUtils {
	public static bool IsPlaying (Animator animator, string animationName) {
		if (animator == null) {
			return false;
		}

		return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
	}

	public static void PlayOnce (Animator animator, string animationName) {
		if (animator != null && !IsPlaying(animator, animationName)) {
			animator.Play(animationName);
		}
	}
}
