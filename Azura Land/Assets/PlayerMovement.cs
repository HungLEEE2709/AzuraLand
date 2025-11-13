using Quantum;
using UnityEngine;

public class GohanView : MonoBehaviour
{
    public QuantumEntityView view;
    private Animator animator;

    private GameObject idle;
    private GameObject run;
    private GameObject kame;
    private GameObject hit;

    void Awake()
    {
        animator = GetComponent<Animator>();

        idle = transform.Find("Idle").gameObject;
        run = transform.Find("Run").gameObject;
        kame = transform.Find("Kame").gameObject;
        hit = transform.Find("Hit").gameObject;
    }

    void Update()
    {
        if (view == null || view.EntityRef == EntityRef.None)
            return;

        var f = QuantumRunner.DefaultGame.Frames.Predicted;


    }
}
