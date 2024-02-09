using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AnimatedVisualisator<DataPacket> : Visualisator<DataPacket>
{
    protected Animator Animator;
    protected virtual void Awake() => Animator = GetComponent<Animator>();

    public override void Open() 
    { 
        Animator.SetTrigger("Open"); 
        DisplayData(); 
    }
    public override void Hide() => Animator.SetTrigger("Hide");
}
