using UnityEngine;
using VirtueSky.Inspector;

public class HolderItem : ItemResource
{
    [HeaderLine("Animation")] [SerializeField]
    AnimationClip idleAnimation;

    [SerializeField] AnimationClip openAnimation;
    [SerializeField] AnimationClip closeAnimation;

    [HeaderLine("Properties")] [SerializeField]
    Transform holderItemPlace;

    [SerializeField] private GameObject model;

    [SerializeField] ArrangeData arrangeData;

    private void OnEnable()
    {
        PlayAnimOpen();
    }

    void PlayAnimOpen()
    {
        handleAnimancerComponentCustom.PlayAnim(openAnimation, _durationFade: 0, _endAnim: PlayAnimIdle);
    }

    public void PlayAnimClose()
    {
        handleAnimancerComponentCustom.PlayAnim(closeAnimation);
    }

    void PlayAnimIdle()
    {
        handleAnimancerComponentCustom.PlayAnim(idleAnimation);
    }

    public ArrangeData ArrangeData => arrangeData;
    public Transform HolderItemPlace => holderItemPlace;
}