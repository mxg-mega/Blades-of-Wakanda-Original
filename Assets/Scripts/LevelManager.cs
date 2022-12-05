using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { set; get; }

    public bool SHOW_COLLIDER = true;//$$ this means it is going to be removed for the final build

    /* A transition - is that pause of spawning objects for the player to catch up/to rest 
     before continuing.
     */

    // Level Spawning
    private const float DISTANCE_BEFORE_SPAWN = 200.0f;
    private const int INITIAL_SEGMENTS = 20;
    private const int INITIAL_TRANSITION_SEGMENT = 2;
    private const int MAX_SEGMENT_ON_SCREEN = 25;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1, y2, y3;

    // List of Pieces
    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longBlocks = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> longSlides = new List<Piece>();
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>();// All the pieses that we have

    //List of Segments
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments = new List<Segment>();

    //GamePlay
    private bool isMoving = false;

    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        currentLevel = 0;
    }

    private void Start()
    {
        for (int i = 0; i < INITIAL_SEGMENTS; i++)
        {
            if(i < INITIAL_TRANSITION_SEGMENT)
            {
                SpawnTransition();
            }
            else
            {
                // Generate a segment
                GenerateSegment();
            }
            
        }
    }

    private void Update()
    {
        if(currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegment();
        }

        if(amountOfActiveSegments >= MAX_SEGMENT_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].Despawn();
            amountOfActiveSegments--;
        }
    }

    private void GenerateSegment()
    {
        SpawnSegment();

        // You can Only have up to 5 segments before the transition segment
        if (Random.Range(0f,1f) < (continiousSegments * 0.25f))
        {
            // Spawn Transition segment
            continiousSegments = 0;
            SpawnTransition();
        }
        else
        {
            continiousSegments++;
        }
    }

    private void SpawnSegment()
    {
        List<Segment> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s = GetSegment(id, false);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();
    }

    private void SpawnTransition()
    {
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        Segment s = GetSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();
    }

    public Segment GetSegment(int id, bool transition)
    {
        Segment s = null;
        s = segments.Find(x => x.SegId == id && x.transition == transition && !x.gameObject.activeSelf);
        

        if (s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<Segment>();

            s.SegId = id;
            s.transition = transition;

            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }

        return s;
    }

    public Piece GetPiece(PieceType pieceType, int visulaIndex)
    {
        Piece p = pieces.Find(x => x.type == pieceType && x.visualIndex == visulaIndex && !x.gameObject.activeSelf);

        if(p == null)
        {
            GameObject go = null;
            if(pieceType == PieceType.ramp)
            {
                go = ramps[visulaIndex].gameObject;
            }
            else if (pieceType == PieceType.longBlock)
            {
                go = longBlocks[visulaIndex].gameObject;
            }
            else if(pieceType == PieceType.jump)
            {
                go = jumps[visulaIndex].gameObject;
            }
            else if(pieceType == PieceType.slide)
            {
                go = slides[visulaIndex].gameObject;
            }
            else if (pieceType == PieceType.longSlides)
            {
                go = longSlides[visulaIndex].gameObject;
            }

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);
        }

        return p;
    }
}
