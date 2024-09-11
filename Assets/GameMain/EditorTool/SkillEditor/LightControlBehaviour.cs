using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class LightControlBehaviour : PlayableBehaviour {
    //public Light light = null; 不再需要它了
    public Color color = Color.white;
    public float intensity = 1f;
    public PlayableDirector playableDirector;
    public TimelineClip clip;
    double startTime = 0f;
    double totalTime = 0f;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        Light light = playerData as Light; // 这个地方有变化
        if (light != null) {
            light.color = color;
            light.intensity = intensity;
        }
        //使用下面到判断来在pasue自带的暂停之前来停下TimeLine
        if (playable.GetDuration() - playable.GetTime() < 0.083333f) {
            //playableDirector.time -= 0.08f;
            playableDirector.time = startTime;
#if InDebugMode
            Debug.Log("手动暂停");
#endif
        }
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        base.OnBehaviourPlay(playable, info);
        startTime = playableDirector.time;
        Debug.Log("开始");
    }
}