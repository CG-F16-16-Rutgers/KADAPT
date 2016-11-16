using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class MyBehaviorTree2 : MonoBehaviour
{
	public Transform wander1;
	public Transform wander2;
	public Transform wander3;
    public Transform wander4;
    public Transform wander5;
    public Transform wander6;
    public Transform wander7;
    public Transform wander8;

    public Transform stride1;
    public Transform stride2;
    public Transform stride3;
    public Transform stride4;
    public Transform stride5;
    public Transform stride6;
    public Transform stride7;
    public Transform stride8;

    public Transform setUpPoint1;
    public Transform pushThroughPoint1;
    public Transform setUpPoint2;
    public Transform pushThroughPoint2;
    public GameObject participant;
	public GameObject police;
    public GameObject mayor;
    public GameObject[] pickup;
    public Transform townMeetingLoc;

	private BehaviorAgent behaviorAgent;

    private bool TownMeeting = false;
    private static bool BarrierSelected;
    private GameObject barrier;
    // Use this for initialization
    void Start ()
	{
		behaviorAgent = new BehaviorAgent (this.BuildTreeRoot ());
		BehaviorManager.Instance.Register (behaviorAgent);
		behaviorAgent.StartBehavior ();


        barrier = GameObject.FindGameObjectWithTag("Barrier");
    }

	// Update is called once per frame
	void Update ()
	{
        /*if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
                Vector3 position = barrier.transform.position;
                position.x++;
                barrier.transform.position = position;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {

                Vector3 position = barrier.transform.position;
                position.x--;
                barrier.transform.position = position;

        }
        else*/ if (Input.GetKeyDown(KeyCode.DownArrow))
        {

                Vector3 position = barrier.transform.position;
                position.x++;
                barrier.transform.position = position;

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

                Vector3 position = barrier.transform.position;
                position.x--;
                barrier.transform.position = position;

        }
    }

    /*	protected Node ST_ApproachAndWait(Transform target)
        {
            Val<Vector3> position = Val.V (() => target.position);

            //print("Sending BOW Command");
            return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BOW", true), new LeafWait(500));

            //print("Sending KARATEGREET Command");
            return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("KARATEGREET", true), new LeafWait(500));

            //this was the original behaviour
            return new Sequence( participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(500));
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

    protected Node GoToMeeting()
    {
        Sequence policeGoToMeeting = new Sequence(police.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(townMeetingLoc.position, 5.0f),
                                                    new LeafWait(500),
                                                    police.GetComponent<BehaviorMecanim>().Node_OrientTowards(townMeetingLoc.position),
                                                    new LeafWait(500));

        Sequence citizenGoToMeeting = new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(townMeetingLoc.position, 5.0f),
                                                    new LeafWait(500),
                                                    participant.GetComponent<BehaviorMecanim>().Node_OrientTowards(townMeetingLoc.position),
                                                    new LeafWait(500));

        Sequence mayorGoToMeeting = new Sequence(mayor.GetComponent<BehaviorMecanim>().Node_GoTo(townMeetingLoc.position),
                                                    new LeafWait(500),
                                                    mayor.GetComponent<BehaviorMecanim>().Node_OrientTowards(police.transform.position),
                                                    new LeafWait(500));

        return new SequenceParallel(policeGoToMeeting, citizenGoToMeeting, mayorGoToMeeting);
    }


    protected Node HaveTownMeeting()
    {
        Sequence policeAtMeeting= new Sequence(   police.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true),
                                                    new LeafWait(500),
                                                    police.GetComponent<BehaviorMecanim>().Node_HandAnimation("BOW", true),
                                                    new LeafWait(500),
                                                   // police.GetComponent<BehaviorMecanim>().Node_HandAnimation("POINT", true),
                                                    new LeafWait(500),
                                                    police.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true),
                                                    new LeafWait(500),
                                                    police.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", false)
                                                    );

        Sequence citizenAtMeeting = new Sequence(participant.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true),
                                            new LeafWait(500),
                                            participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("KARATEGREET", true),
                                            new LeafWait(5000),
                                            participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("ARMFLEX", true),
                                            new LeafWait(500),
                                            participant.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", false)
                                            );


        Sequence mayorsMeeting = new Sequence(mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true),
                                                new LeafWait(500),
                                                mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("CROWDPUMP", true),
                                                new LeafWait(500),
                                                mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true),
                                                new LeafWait(500),
                                                mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", false)
                                               //mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("POINT", true)
                                               );

        return new SequenceParallel(policeAtMeeting, citizenAtMeeting, mayorsMeeting) ; 
    }

    /*protected Node CallTownMeeting()
    {
        //TownMeeting = true;
        return mayor.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(townMeetingLoc.position, 1.0f);
    }*/


    protected Node ST_PushCrates()
    {
        return new Sequence (
            participant.GetComponent<BehaviorMecanim>().Node_GoTo(setUpPoint2.position),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_GoTo(pushThroughPoint2.position),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("PUSHING", true),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("ARMFLEX", true),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_GoTo(setUpPoint2.position),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_GoTo(setUpPoint1.position),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_GoTo(pushThroughPoint1.position),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("PUSHING", true),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("ARMFLEX", true),
            new LeafWait(500),
            participant.GetComponent<BehaviorMecanim>().Node_GoTo(setUpPoint1.position),
            new LeafWait(500)
            //participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE", true)
            //new LeafWait(500)
            );
    }

    protected Node PO_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        //print(position);

        //this was the original behaviour
        return new Sequence(police.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(500));
    }

    protected Node MO_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);

        //print(position);

        //this was the original behaviour
        return new Sequence(mayor.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(500));
    }

    protected Node ST_KarateGreet()
    {

        //print("Sending KARATEGREET Command");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("KARATEGREET", true), new LeafWait(500));
    }
    protected Node ST_Bow()
    {
        //Val<Vector3> position = Val.V(() => target.position);

        //print("Sending BOW Command");
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BOW", true), new LeafWait(500));
    }

    protected Node ST_ApproachAndGrab(Transform target)
    {
        //Val<Vector3> position = Val.V(() => target.position);

        //this was the original behaviour
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(GrabThis().position), 
                            participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("PICKUPRIGHT", true), 
                            new LeafWait(500));
    }

    private Transform GrabThis()
    {
        //Val<Vector3> position = Val.V(() => target.position);

        //print("Sending BOW Command");
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in pickup)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.transform;


        
    }

    protected Node BDNow()
    {
        return new SequenceParallel(participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE", true),
        police.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE", true),
        mayor.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE", true));
    }

    protected Node BDEnd()
    {
        return new SequenceParallel(participant.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE", false),
        police.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE", false),
        mayor.GetComponent<BehaviorMecanim>().Node_BodyAnimation("BREAKDANCE", false));
    }

    protected Node Escape()
    {
        return new SelectorParallel(mayor.GetComponent<BehaviorMecanim>().Node_GoTo(this.stride3.position),this.Escape());
    }

    protected Node BuildTreeRoot()
    {
        Val<float> pp = Val.V(() => police.transform.position.z);
        Func<bool> act = () => (police.transform.position.z - participant.transform.position.z< 10);

        Val<float> mpp = Val.V(() => mayor.transform.position.z);
        Func<bool> mact = () => (mayor.transform.position.z - participant.transform.position.z < 10);

        Node roaming = //new DecoratorLoop(
            new Sequence(
                this.ST_KarateGreet(),
                this.ST_PushCrates(),
                new LeafWait(500)                
                );
        Node trigger = new Sequence(new LeafAssert(act),
                                            this.PO_ApproachAndWait(this.wander1),
                                            this.PO_ApproachAndWait(this.wander2), 
                                            this.PO_ApproachAndWait(this.wander3),
                                            this.PO_ApproachAndWait(this.wander4),
                                            this.PO_ApproachAndWait(this.wander5),
                                            this.PO_ApproachAndWait(this.wander6),
                                            this.PO_ApproachAndWait(this.wander7),
                                            this.PO_ApproachAndWait(this.wander8));

        Node oversee = new SequenceShuffle(new LeafAssert(mact), mayor.GetComponent<BehaviorMecanim>().Node_GoTo(townMeetingLoc.position),
                                            new Sequence(
                                            this.MO_ApproachAndWait(this.stride1),
                                            new Sequence(this.MO_ApproachAndWait(this.stride3),
                                                        new Selector(
                                                            new Sequence(
                                                                this.MO_ApproachAndWait(this.stride4),
                                                                new LeafWait(500),
                                                                new Selector(this.MO_ApproachAndWait(this.stride3),
                                                                    new SelectorShuffle(
                                                                        mayor.GetComponent<BehaviorMecanim>().Node_BodyAnimation("TALKING ON PHONE", true))),
                                                                        this.MO_ApproachAndWait(this.stride3)),
                                                            new Sequence (
                                                                mayor.GetComponent<BehaviorMecanim>().Node_BodyAnimation("TALKING ON PHONE", true),
                                                                new LeafWait(500),
                                                                this.MO_ApproachAndWait(this.stride3)))),

                                            this.MO_ApproachAndWait(this.stride2),
                                            this.MO_ApproachAndWait(this.stride5),
                                            this.MO_ApproachAndWait(this.stride6),
                                            this.MO_ApproachAndWait(this.stride7),
                                            this.MO_ApproachAndWait(this.stride8)));

        //Node mayorsPropoganda = new Sequence(oversee, this.CallTownMeeting()); 

        Node root = new DecoratorLoop(
                        new DecoratorForceStatus(RunStatus.Success, new SequenceShuffle(
                            new SelectorParallel(trigger, roaming, oversee),
                            new LeafWait(500),
                            new Sequence (this.GoToMeeting(), new LeafWait(500), this.HaveTownMeeting()) 
                            )));

        

        return root;


        //return roaming;
    }



}

