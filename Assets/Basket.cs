using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Basket : MonoBehaviour
{
    private int successNeededCount = 5;
    private int count = 0;

    public float rayLength = 10f; // Ray의 길이 설정
    private RectTransform m_rectTransform;
    BoxCollider2D m_boxCollider;
    ContactFilter2D contactFilter = new ContactFilter2D()
    {
        useLayerMask = true,

    };
    List<Collider2D> overlappingColliders = new List<Collider2D>();

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GetObject getObject;
    [SerializeField] private GameObject pineGame;
    [SerializeField] private GenerateBugAndCone generateBugAndCone;
    [SerializeField] private PineconeMove pineconeMove;


    private void OnEnable()
    {
        count = 0;
        successNeededCount = 5;
        rayLength = 10f;
    }

    private void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        contactFilter.SetLayerMask(Physics2D.AllLayers);
        m_boxCollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        
        
        int tmp= m_boxCollider.OverlapCollider(contactFilter, overlappingColliders);
        if (tmp>=1)
        {
            GameObject colliderObject = overlappingColliders[0].gameObject;
            if (colliderObject.tag == "cone")
            {
                //대충 점수 추가
                Debug.Log("1점 추가");
                count++;
                Destroy(colliderObject);
            }
            else if (colliderObject.tag == "bug")
            {

                generateBugAndCone.IsActive = false;
                pineconeMove.IsActive = false;
                //실패 로직 추가
                Destroy(colliderObject);
                StartCoroutine(StartFadeOut());

            }
            else
            {
                Destroy(colliderObject);
                Debug.Log("collision error");
            }
            //Destroy(overlappingColliders[0].gameObject);
            if (successNeededCount <= count)
            {
                generateBugAndCone.IsActive = false;
                pineconeMove.IsActive = false;

                //성공! 로직 추가
                StartCoroutine(StartFadeOut());
                getObject.AddSelectedItem(ItemInfo.Item.Pinecone);
            }
        }
    }

    IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        pineGame.SetActive(false);
        playerController.ChangeState(playerController._idleState);
        getObject.RotateClock();
    }
    
}
