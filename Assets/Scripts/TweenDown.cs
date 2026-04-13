using UnityEngine;

public class TweenDown : MonoBehaviour
{
    public float targetScale; // 1
    public float timeToReachTarget; // 2
    private float startScale;  // 3
    private float percentScaled; // 4
    public float timeToDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale.x;
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        if (percentScaled < 1f){
            percentScaled += Time.deltaTime / timeToReachTarget; // 2
            float scale = Mathf.Lerp(startScale, targetScale, percentScaled); // 3
            transform.localScale = new Vector3(scale, scale, scale); // 4
        }
    }
}
