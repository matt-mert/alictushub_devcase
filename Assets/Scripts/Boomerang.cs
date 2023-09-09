using UnityEngine;

public class Boomerang : Projectile
{
    [SerializeField]
    private AnimationCurve curve1;
    [SerializeField]
    private AnimationCurve curve2;

    private AnimationCurve selected;

    protected override void Awake()
    {
        base.Awake();
        int random = Random.Range(0, 2);
        selected = (random == 0) ? curve1 : curve2;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
