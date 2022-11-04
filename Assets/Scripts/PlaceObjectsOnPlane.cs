using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObjectsOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    public GameObject[] placedPrefabs;


    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }
    public Camera arCamera;
    public GameObject spawnMarker;
    public GameObject textObject;
    public GameObject visualIndicator;

    public AudioSource SFXSource;
    public AudioClip[] SFXClips;


    private GameObject hitObject;

    private bool planeVisualizerIsON;

    private bool creatureSpawned;
    private bool secondCreatureSpawned;

    private bool groundPlaced;
    private bool startState;

    private Text screenText;



    void Start()
    {
        creatureSpawned = false;
        groundPlaced = false;
        startState = true;
        secondCreatureSpawned = false;

        screenText = textObject.GetComponent<Text>();

    }

    //bool TryGetTouchPosition(out Vector2 touchPosition)
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        touchPosition = Input.GetTouch(0).position;
    //        return true;
    //    }

    //    touchPosition = default;
    //    return false;
    //}


    //void FollowByViewRotation(Transform hitTransform)
    //{
    //    Vector2 diff = new Vector2(hitTransform.position.x, hitTransform.position.z) - new Vector2(arCamera.transform.localPosition.x, arCamera.transform.localPosition.z);
    //    float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90f;
    //    Quaternion rotationOffset = Quaternion.Euler(0, angle, 0);
    //}

    void PlaceObject()
    {
        int layerMask = 1 << 2;
        layerMask = ~layerMask;
        Ray ray = new Ray(arCamera.transform.position, arCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject.tag == "Ground")
            {
                SFXSource.PlayOneShot(SFXClips[Random.Range(1, SFXClips.Length)]);

                if (secondCreatureSpawned)
                    textObject.SetActive(false);

                if (creatureSpawned)
                    secondCreatureSpawned = true;

                creatureSpawned = true;
                spawnMarker.SetActive(false);


                if (placedPrefabs.Length != 0)
                {
                    spawnedObject = Instantiate(placedPrefabs[Random.Range(0, placedPrefabs.Length)], hit.point + Vector3.up * 0.5f, Quaternion.Euler(spawnMarker.transform.localRotation.x, arCamera.transform.localRotation.y - Random.Range(120f, 220f), spawnMarker.transform.localRotation.z));
                }
                else
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hit.point + Vector3.up * 0.5f, Quaternion.Euler(spawnMarker.transform.localRotation.x, arCamera.transform.localRotation.y - Random.Range(120f, 220f), spawnMarker.transform.localRotation.z));
                }
            }
        }
    }

    void SetGround()
    {
        screenText.text = "PAINA RUUTUA ASETTAAKSESI PINTA";

        if (Input.GetMouseButtonDown(0))
        {
            groundPlaced = true;
            startState = false;

            SFXSource.PlayOneShot(SFXClips[0]);
        }

    }

    void Update()
    {
        //if (!TryGetTouchPosition(out Vector2 touchPosition))
        //    return;

        if (!groundPlaced && startState && visualIndicator.activeSelf == false)
        {
            screenText.text = "LIIKUTA KAMERAA NIIN, ETTÄ LÖYDÄT TASAISEN PINNAN";
        }

        if (Input.GetMouseButtonDown(0) && groundPlaced)
        {
            if (Input.mousePosition.y > Screen.width * 0.9f)
            {
                return;
            }

            PlaceObject();
        }


        if (groundPlaced == false && visualIndicator.activeSelf)
        {
            SetGround();
        }

        if (!creatureSpawned && groundPlaced)
        {
            Ray ray = new Ray(arCamera.transform.position, arCamera.transform.forward);
            RaycastHit hit;

            spawnMarker.SetActive(true);
            screenText.text = "PAINA RUUTUA NIIN OTUS ILMAANTUU";

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Ground")
                {
                    spawnMarker.transform.localPosition = hit.point;
                    spawnMarker.transform.localRotation = Quaternion.Euler(spawnMarker.transform.localRotation.x, arCamera.transform.localRotation.y, spawnMarker.transform.localRotation.z);
                }
            }
        }

        if (creatureSpawned && groundPlaced)
        {
            screenText.text = "PAINA RUUTUA UUDESTAAN NIIN SAAT LISÄÄ OTUKSIA!";

        }



        //if (Input.touchCount == 1)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.tapCount == 1 && touch.phase == TouchPhase.Began && !creatureSpawned)
        //    {
        //        Ray ray = arCamera.ScreenPointToRay(touch.position);
        //        RaycastHit hit;

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.transform.gameObject.tag == "Ground")
        //            {
        //                spawnedObject = Instantiate(m_PlacedPrefab, hit.transform.position + Vector3.up, hit.transform.rotation);
        //                
        //                return;
        //            }
        //        }
        //    }

        //    if (touch.tapCount == 2 && touch.phase == TouchPhase.Began && creatureSpawned == true)
        //    {
        //        Ray ray = arCamera.ScreenPointToRay(touch.position);
        //        RaycastHit hit;

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.transform.gameObject.tag == "Creature")
        //            {
        //                spawnedObject = Instantiate(m_PlacedPrefab, hit.transform.position, hit.transform.rotation);
        //                Destroy(hitObject, 0.1f);

        //                return;
        //            }
        //        }
        //    }
        //}

    }

}
