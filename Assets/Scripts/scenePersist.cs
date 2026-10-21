using UnityEngine;

public class scenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersist = FindObjectsOfType<scenePersist>().Length;
        if (numScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void resetScenePersist()
    {
        Destroy(gameObject);
    }
}
