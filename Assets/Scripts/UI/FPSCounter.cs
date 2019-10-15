using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private Text text;
        
    private void Start()
    {
        text = GetComponent<Text>();
        Update();
    }

    private void Update()
    {
        text.text = (1f / Time.deltaTime).ToString();
    }
}
