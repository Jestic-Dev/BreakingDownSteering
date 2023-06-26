using UnityEngine;

namespace Scenes.Evaluation.Tools
{
    public class ScreenShotCapture : MonoBehaviour
    {
        [SerializeField]
        private string screenName;

        // Update is called once per frame
        void Update()
        {
            // Press Alt key to take a Screen Capture
            if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
            {
                ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), screenName + ".png"), 3);
                Debug.Log("Screenshot Captured");
            }
        }
    }
}

