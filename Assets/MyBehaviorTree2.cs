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

    public Transform mayorStride1;
    public Transform mayorStride2;
    public Transform mayorStride3;
    public Transform mayorStride4;
    public Transform mayorStride5;
    public Transform mayorStride6;
    public Transform mayorStride7;
    public Transform mayorStride8;

    public Transform barrierFlag;

    public Transform setUpPoint1;
    public Transform pushThroughPoint1;
    public Transform setUpPoint2;
    public Transform pushThroughPoint2;
    public GameObject participant;
	public GameObject police;
    public GameObject mayor;
    public GameObject TomCluster1Clone1;
    public GameObject TomCluster1Clone2;
    public GameObject TomCluster1Clone3;
    public GameObject TomCluster2Clone1;
    public GameObject TomCluster2Clone2;
    public GameObject TomCluster2Clone3;
    public GameObject TomCluster2Clone4;

    public GameObject[] pickup;
    public Transform townMeetingLoc;

	private BehaviorAgent behaviorAgentCoreAct1;
    private BehaviorAgent behaviorAgentCoreAct1Mayor;
    private BehaviorAgent behaviorAgentIntermission1Mayor;
    private BehaviorAgent behaviorAgentCoreAct2Mayor;

    private BehaviorAgent behaviorAgentFringeCluster1Start;
    private BehaviorAgent behaviorAgentFringeCluster2Start;
    private BehaviorAgent behaviorAgentFringeCluster1Mayor;
    private BehaviorAgent behaviorAgentFringeCluster2Mayor;
    private BehaviorAgent behaviorAgentCoreAct3;

    private GameObject barrier;

    int fCounter;

    int pushedNum;

    bool mVisitedTCC1Recently;
    bool mVisitedTCC2Recently;
    bool mVisitedEndRecently;
    bool mVisitedRoom;
    bool mBlocked;
    bool mTrapped;
    bool m2ndStep;
    bool pPushed;

    // Use this for initialization
    void Start ()
	{
		behaviorAgentCoreAct1 = new BehaviorAgent (this.CoreAct1());
		BehaviorManager.Instance.Register (behaviorAgentCoreAct1);
        behaviorAgentCoreAct1.StartBehavior();

        behaviorAgentFringeCluster1Start = new BehaviorAgent(this.TomCloneCluster1());
        BehaviorManager.Instance.Register(behaviorAgentFringeCluster1Start);
        behaviorAgentFringeCluster1Start.StartBehavior();

        behaviorAgentFringeCluster2Start = new BehaviorAgent(this.TomCloneCluster2());
        BehaviorManager.Instance.Register(behaviorAgentFringeCluster2Start);
        behaviorAgentFringeCluster2Start.StartBehavior();

        behaviorAgentFringeCluster1Mayor = new BehaviorAgent(this.MayorAtCluster1());
        BehaviorManager.Instance.Register(behaviorAgentFringeCluster1Mayor);

        behaviorAgentFringeCluster2Mayor = new BehaviorAgent(this.MayorAtCluster2());
        BehaviorManager.Instance.Register(behaviorAgentFringeCluster2Mayor);

        behaviorAgentCoreAct1Mayor = new BehaviorAgent(this.CoreAct1Mayor());
        BehaviorManager.Instance.Register(behaviorAgentCoreAct1Mayor);
        behaviorAgentCoreAct1Mayor.StartBehavior();

        behaviorAgentIntermission1Mayor = new BehaviorAgent(this.CoreIntermission1Mayor());
        BehaviorManager.Instance.Register(behaviorAgentIntermission1Mayor);

        behaviorAgentCoreAct2Mayor = new BehaviorAgent(this.CoreAct2Mayor());
        BehaviorManager.Instance.Register(behaviorAgentCoreAct2Mayor);

        behaviorAgentCoreAct3 = new BehaviorAgent(this.CoreAct3());
        BehaviorManager.Instance.Register(behaviorAgentCoreAct3);

        mVisitedTCC1Recently = false;
        mVisitedTCC2Recently = false;
        mBlocked = false;
        mTrapped = false;
        m2ndStep = false;
        mVisitedRoom = false;
        pPushed = false;



        barrier = GameObject.FindGameObjectWithTag("Barrier");
        fCounter = 0;

        pushedNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
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

        if (m2ndStep) return;

        if (Vector3.Distance(participant.transform.position, setUpPoint1.position) < 2.0f && !pPushed)
        {
            pPushed = true;
            pushedNum++;
        }

        if (Vector3.Distance(participant.transform.position, setUpPoint1.position) > 3.0f && pPushed)
        {
            pPushed = false;
        }


        if (Vector3.Distance(mayor.transform.position, mayorStride1.position) < 3.0f  && !mVisitedTCC1Recently)
        {
            behaviorAgentFringeCluster1Start.StopBehavior();
            behaviorAgentFringeCluster1Mayor.StartBehavior();
            mVisitedTCC1Recently = true;
            print("Mayor Here");
        }

        if (Vector3.Distance(mayor.transform.position, mayorStride1.position) > 5.0f && mVisitedTCC1Recently)
        {
            behaviorAgentFringeCluster1Mayor.StopBehavior();
            behaviorAgentFringeCluster1Start.StartBehavior();
            mVisitedTCC1Recently = false;
            print("Mayor gone");
        }

        if (Vector3.Distance(mayor.transform.position, mayorStride3.position) < 3.0f && !mVisitedTCC2Recently)
        {
            behaviorAgentFringeCluster2Start.StopBehavior();
            behaviorAgentFringeCluster2Mayor.StartBehavior();
            mVisitedTCC2Recently = true;
            print("Mayor Here");
        }

        if (Vector3.Distance(mayor.transform.position, mayorStride3.position) > 5.0f && mVisitedTCC2Recently)
        {
            behaviorAgentFringeCluster2Mayor.StopBehavior();
            behaviorAgentFringeCluster2Start.StartBehavior();
            mVisitedTCC2Recently = false;
            print("Mayor gone");
        }


        if (Vector3.Distance(mayor.transform.position, mayorStride8.position) < 3.0f && !mVisitedEndRecently)
        {
            behaviorAgentCoreAct1.StopBehavior();
            behaviorAgentFringeCluster2Mayor.StartBehavior();
            mVisitedEndRecently = true;
            print("Mayor Here");
        }

        if (Vector3.Distance(mayor.transform.position, mayorStride8.position) > 5.0f && mVisitedEndRecently)
        {
            behaviorAgentFringeCluster2Mayor.StopBehavior();
            behaviorAgentFringeCluster2Start.StartBehavior();
            mVisitedEndRecently = false;
            print("Mayor gone");
        }

            fCounter++;
        if (fCounter % 100 == 0)
        {
            fCounter = 0;
            //print(Vector3.Distance(barrier.transform.position, barrierFlag.position));

            /*if (mBlocked && !m2ndStep) print("Blocked");
            if (mTrapped) print("Trapped");
            print(m2ndStep);*/
        }

        if ((Vector3.Distance(barrier.transform.position, barrierFlag.position) < 8.0f) && (Vector3.Distance(mayor.transform.position, mayorStride3.position) < 3.0f))
        {
            mVisitedRoom = true;
            behaviorAgentCoreAct1Mayor.StopBehavior();
            behaviorAgentCoreAct2Mayor.StartBehavior(); 
        }

        if ((Vector3.Distance(barrier.transform.position, barrierFlag.position) < 8.0f) && (Vector3.Distance(mayor.transform.position, mayorStride4.position) < 4.0f) && !mTrapped)
        {
            mTrapped = true;
            mVisitedRoom = true;
            behaviorAgentCoreAct1Mayor.StopBehavior();
            behaviorAgentIntermission1Mayor.StartBehavior();
            print("Poop");

        }

        if (mTrapped && (Vector3.Distance(barrier.transform.position, barrierFlag.position) >= 8.0f))
        {

            print("Pain");
            mTrapped = false;
            behaviorAgentIntermission1Mayor.StopBehavior();
            behaviorAgentCoreAct2Mayor.StartBehavior();
        }


        if (mVisitedRoom && pushedNum%2==0 && !mTrapped)
        {
            behaviorAgentCoreAct2Mayor.StopBehavior();
            behaviorAgentCoreAct1.StopBehavior();
            behaviorAgentCoreAct3.StartBehavior();
        }
    }

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

    protected Node TomCloneCluster1()
    {
        SequenceParallel idle = new SequenceParallel(TomCluster1Clone1Acts(), TomCluster1Clone2Acts(), TomCluster1Clone3Acts());
        return idle;

    }

    protected Node MayorAtCluster1()
    {
        SequenceParallel mayorHere = new SequenceParallel(
                                                  new Sequence(TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_OrientTowards(wander1.position),
                                                                    TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true)),
                                                  new Sequence(TomCluster1Clone2.GetComponent<BehaviorMecanim>().Node_OrientTowards(wander1.position),
                                                                    TomCluster1Clone2.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true)),
                                                  new Sequence(TomCluster1Clone3.GetComponent<BehaviorMecanim>().Node_OrientTowards(wander1.position),
                                                                    TomCluster1Clone3.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true)));
        return new DecoratorLoop(mayorHere);
    }

    protected Node MayorAtCluster2()
    {
        SequenceParallel mayorHere = new SequenceParallel(
                                                  new Sequence(TomCluster2Clone1.GetComponent<BehaviorMecanim>().Node_OrientTowards(wander3.position),
                                                                    TomCluster2Clone1.GetComponent<BehaviorMecanim>().Node_BodyAnimation("KARATEGREET", true)),
                                                  new Sequence(TomCluster2Clone2.GetComponent<BehaviorMecanim>().Node_OrientTowards(wander1.position),
                                                                    TomCluster2Clone2.GetComponent<BehaviorMecanim>().Node_BodyAnimation("KARATEGREET", true)),
                                                  new Sequence(TomCluster2Clone3.GetComponent<BehaviorMecanim>().Node_OrientTowards(wander1.position),
                                                                    TomCluster2Clone3.GetComponent<BehaviorMecanim>().Node_BodyAnimation("KARATEGREET", true)));
        return new DecoratorLoop(mayorHere);
    }

    protected Node TomCluster1Clone1Acts()
    {

        return new Sequence(TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_OrientTowards(mayorStride1.position), new LeafWait(500),
                        new DecoratorLoop(
                            new SelectorShuffle( 
                                   new Sequence(TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_OrientTowards(TomCluster1Clone2.transform.position), 
                                                    new LeafWait(4000), TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("POINTING", true), new LeafWait(4000) ,TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("POINTING", false)),
                                   new Sequence(TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_OrientTowards(TomCluster1Clone3.transform.position),
                                                    new LeafWait(4000), TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("POINTING", true), new LeafWait(4000) ,TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("POINTING", false)),
                                   new Sequence(TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("YAWN", true), new LeafWait(500)),
                                   new Sequence(TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("CRY", true), new LeafWait(500)),
                                   new Sequence(TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("THINK", true), new LeafWait(500))
                                   )));
    }

    protected Node TomCluster1Clone2Acts()
    {
        return new Sequence(TomCluster1Clone2.GetComponent<BehaviorMecanim>().Node_OrientTowards(mayorStride1.position), new LeafWait(500),
                                new DecoratorLoop (new SelectorShuffle(TomCluster1Clone2.GetComponent<BehaviorMecanim>().Node_HandAnimation("CRY", true), new LeafWait(500),
                                                    TomCluster1Clone2.GetComponent<BehaviorMecanim>().Node_HandAnimation("THINK", true), new LeafWait(500))
            ));
    }

    protected Node TomCluster1Clone3Acts()
    {
        return new Sequence(TomCluster1Clone3.GetComponent<BehaviorMecanim>().Node_OrientTowards(mayorStride1.position), new LeafWait(500),
                                new DecoratorLoop (new SelectorShuffle(new Sequence (TomCluster1Clone3.GetComponent<BehaviorMecanim>().Node_HandAnimation("TEXTING", true), new LeafWait(500)),
                                    new Sequence(TomCluster1Clone3.GetComponent<BehaviorMecanim>().Node_HandAnimation("THINK", true), new LeafWait(500)),
                                    new Sequence(TomCluster1Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("CRY", true), new LeafWait(500)))));
    }

    protected Node TomCloneCluster2()
    {
        return new SequenceParallel(TomCluster2Clone1Acts(), TomCluster2Clone2Acts(), TomCluster2Clone3Acts() , TomCluster2Clone4Acts());
    }

    protected Node TomCluster2Clone1Acts()
    {
        return new Sequence(TomCluster2Clone1.GetComponent<BehaviorMecanim>().Node_OrientTowards(mayorStride3.position), new LeafWait(500),
            new DecoratorLoop(new SelectorShuffle(TomCluster2Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("CRY", true), new LeafWait(500),
                                                    TomCluster2Clone1.GetComponent<BehaviorMecanim>().Node_HandAnimation("THINK", true), new LeafWait(500))));
    }

    protected Node TomCluster2Clone2Acts()
    {
        return new Sequence(TomCluster2Clone2.GetComponent<BehaviorMecanim>().Node_OrientTowards(mayorStride3.position), new LeafWait(500),
            new DecoratorLoop(new SelectorShuffle(TomCluster2Clone2.GetComponent<BehaviorMecanim>().Node_HandAnimation("CRY", true), new LeafWait(500),
                                                    TomCluster2Clone2.GetComponent<BehaviorMecanim>().Node_HandAnimation("THINK", true), new LeafWait(500))));
    }

    protected Node TomCluster2Clone3Acts()
    {
        return new Sequence(TomCluster2Clone3.GetComponent<BehaviorMecanim>().Node_OrientTowards(mayorStride3.position), new LeafWait(500),
            new DecoratorLoop(new SelectorShuffle(TomCluster2Clone3.GetComponent<BehaviorMecanim>().Node_HandAnimation("CRY", true), new LeafWait(500),
                                                    TomCluster2Clone3.GetComponent<BehaviorMecanim>().Node_HandAnimation("THINK", true), new LeafWait(500))));
    }

    protected Node TomCluster2Clone4Acts()
    {
        return new Sequence(TomCluster2Clone4.GetComponent<BehaviorMecanim>().Node_OrientTowards(mayorStride3.position), new LeafWait(500),
            new DecoratorLoop(new SelectorShuffle(TomCluster2Clone4.GetComponent<BehaviorMecanim>().Node_HandAnimation("CRY", true), new LeafWait(500),
                                                    TomCluster2Clone4.GetComponent<BehaviorMecanim>().Node_HandAnimation("THINK", true), new LeafWait(500))));
    }

    protected Node HaveTownPrincicpalsMeeting()
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
            );
    }

    protected Node PO_ApproachAndWait(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        //print(position);

        //this was the original behaviour
        return new Sequence(police.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(500));
    }

    protected Node MO_ApproachAndWait(Transform target, int waitInMS)
    {
        //Val<Vector3> position = Val.V(() => target.position);

        //print(position);

        //this was the original behaviour
        return new Sequence(mayor.GetComponent<BehaviorMecanim>().Node_GoTo(target.position), new LeafWait(waitInMS));
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
        Val<Vector3> position = Val.V(() => target.position);

        //this was the original behaviour
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), 
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
        return new SelectorParallel(mayor.GetComponent<BehaviorMecanim>().Node_GoTo(this.mayorStride3.position),this.Escape());
    }

    protected Node CoreAct3()
    {
        return new Sequence(this.GoToMeeting(), new LeafWait(500), this.HaveTownPrincicpalsMeeting());
    }

    protected Node CoreAct1Mayor()
    {
        return new DecoratorForceStatus(RunStatus.Running, new DecoratorLoop( new Sequence(mayor.GetComponent<BehaviorMecanim>().Node_GoTo(townMeetingLoc.position),
                                            this.MO_ApproachAndWait(this.mayorStride1, 5000),
                                            mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true),
                                            new LeafWait(3000),
                                            this.MO_ApproachAndWait(this.mayorStride3, 5000),
                                            mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true),
                                            new LeafWait(3000),
                                            this.MO_ApproachAndWait(this.mayorStride4, 500),
                                            this.MO_ApproachAndWait(this.mayorStride2, 500),
                                            this.MO_ApproachAndWait(this.mayorStride5, 500),
                                            this.MO_ApproachAndWait(this.mayorStride6, 500),
                                            this.MO_ApproachAndWait(this.mayorStride7, 500),
                                            this.MO_ApproachAndWait(this.mayorStride8, 500))));

    }

    protected Node CoreIntermission1Mayor()
    {
        return new DecoratorLoop(new Sequence(new LeafWait(500), mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("TEXTING", true)));
    }

    protected Node CoreAct2Mayor()
    {
        return new DecoratorLoop(new Sequence( this.MO_ApproachAndWait(this.mayorStride5, 500),
                                               this.MO_ApproachAndWait(this.mayorStride6, 500),
                                               this.MO_ApproachAndWait(this.mayorStride7, 500),
                                               this.MO_ApproachAndWait(this.mayorStride8, 500),
                                               this.MO_ApproachAndWait(this.mayorStride1, 5000),
                                               mayor.GetComponent<BehaviorMecanim>().Node_HandAnimation("WAVE", true),
                                               new LeafWait(3000),
                                               this.MO_ApproachAndWait(this.mayorStride2, 500)));
    }

    protected Node CoreAct1()
    {
        Node roaming = //new DecoratorLoop(
            new Sequence(
                this.ST_KarateGreet(),
                this.ST_PushCrates(),
                new LeafWait(500)                
                );
        Node trigger = new Sequence(        this.PO_ApproachAndWait(this.wander1),
                                            this.PO_ApproachAndWait(this.wander2), 
                                            this.PO_ApproachAndWait(this.wander3),
                                            this.PO_ApproachAndWait(this.wander4),
                                            this.PO_ApproachAndWait(this.wander5),
                                            this.PO_ApproachAndWait(this.wander6),
                                            this.PO_ApproachAndWait(this.wander7),
                                            this.PO_ApproachAndWait(this.wander8));

        Node root = new DecoratorForceStatus( RunStatus.Success, 
                            new SequenceParallel(new DecoratorLoop(trigger), new DecoratorLoop(roaming))
                            );

        

        return root;


        //return roaming;
    }



}

