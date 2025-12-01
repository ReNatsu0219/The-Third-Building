using UnityEngine.Animations; // 需要引入这个命名空间
using UnityEngine.Playables;
using UnityEngine;

public class Strength : MonoBehaviour
{
    public Animator targetAnimator;
    public AnimationClip animationClip;

    private PlayableGraph playableGraph;

    void Start()
    {
        if (targetAnimator == null || animationClip == null)
        {
            Debug.LogError("Animator 或 AnimationClip 未赋值！");
            return;
        }

        // 使用工具方法创建并播放动画[citation:2]
        AnimationPlayableUtilities.PlayClip(targetAnimator, animationClip, out playableGraph);
    }

    void OnDestroy()
    {
        // 确保在脚本销毁时清理PlayableGraph，释放资源[citation:1]
        if (playableGraph.IsValid())
        {
            playableGraph.Destroy();
        }
    }
}
