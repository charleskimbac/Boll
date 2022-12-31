using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    MeshRenderer meshRenderer;
    [SerializeField] [Range(0f, 1f)] float lerpTime;

    [SerializeField] Color[] myColors;

    int colorIndex = 0;
    float t = 0f;
    int len;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        len = myColors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, myColors[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > .9f){
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= myColors.Length) ? 0 : colorIndex;
        }
    }
}
