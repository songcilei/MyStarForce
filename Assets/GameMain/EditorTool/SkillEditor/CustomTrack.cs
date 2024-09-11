using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Timeline;
[TrackColor(2, 5, 1)]
[TrackBindingType(typeof(MonoBehaviour))]
[TrackClipType(typeof(Asset))]
public class CustomTrack : PlayableTrack {
    //这个是轨道
}