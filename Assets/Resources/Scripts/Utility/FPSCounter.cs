using UnityEngine;
using UnityEngine.UI;
 
public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text fpsText;
    [SerializeField] private float hudRefreshRate = 1f;
 
    private float timer;
 
    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = fps.ToString();
            timer = Time.unscaledTime + hudRefreshRate;
        }
    }
}