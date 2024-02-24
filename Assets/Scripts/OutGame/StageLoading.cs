using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoading : MonoBehaviour
{
    static public int loadingNumber;
    private float nextSceneTime;

    void Update()
    {
        nextSceneTime += Time.deltaTime;

        if(nextSceneTime > 2.0f)
        {
            switch (loadingNumber)
            {
                case 0:
                    SceneManager.LoadScene("Stage_01");
                    break;

                case 1:
                    SceneManager.LoadScene("Stage_02");
                    break;

                case 2:
                    SceneManager.LoadScene("Stage_03");
                    break;
            }
        }
    }
}