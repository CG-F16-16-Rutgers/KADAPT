﻿using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree2 : MonoBehaviour
{
	public Transform wander1;
	public Transform wander2;
	public Transform wander3;
	public GameObject participant;
	public GameObject police;

	private BehaviorAgent behaviorAgent;
	// Use this for initialization
	void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

    /*	protected Node ST_ApproachAndWait(Transform target)
        {
            Val<Vector3> position = Val.V (() => target.position);

            //print("Sending BOW Command");
            return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BOW", true), new LeafWait(1000));

            //print("Sending KARATEGREET Command");
            return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("KARATEGREET", true), new LeafWait(1000));

            //this was the original behaviour
            return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
        }

        protected Node BuildTreeRoot()
        {
            Val<float> pp = Val.V (() => police.transform.position.z);
            Func<bool> act = () => (police.transform.position.z > 10);
            Node roaming = new DecoratorLoop (
                new Sequence(
                    this.ST_ApproachAndWait(this.wander1),
                    this.ST_ApproachAndWait(this.wander2),
                    this.ST_ApproachAndWait(this.wander3)));
            Node trigger = new DecoratorLoop (new LeafAssert (act));
            Node root = new DecoratorLoop (new DecoratorForceStatus (RunStatus.Success, new SequenceParallel(trigger, roaming)));
            return root;


            //return roaming;
        }*/

    protected Node ST_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);

        //this was the original behaviour
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }
    protected Node ST_KarateGreet()
    {

        //print("Sending KARATEGREET Command");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("KARATEGREET", true), new LeafWait(1000));
    }
    protected Node ST_Bow()
    {
        //Val<Vector3> position = Val.V(() => target.position);

        //print("Sending BOW Command");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BOW", true), new LeafWait(1000));
    }

    protected Node BuildTreeRoot()
    {
        Val<float> pp = Val.V(() => police.transform.position.z);
        Func<bool> act = () => (police.transform.position.z - participant.transform.position.z< 10);
        Node roaming = //new DecoratorLoop(
            new SequenceShuffle(
                this.ST_ApproachAndWait(this.wander1),
                this.ST_ApproachAndWait(this.wander2),
                this.ST_ApproachAndWait(this.wander3),
                this.ST_KarateGreet(),
                this.ST_Bow());
        Node trigger = new SequenceShuffle(new LeafAssert(act), this.ST_ApproachAndWait(this.wander1));
        Node root = new DecoratorLoop(new DecoratorForceStatus(RunStatus.Success, new SequenceParallel(trigger, roaming)));
        return root;


        //return roaming;
    }



}

