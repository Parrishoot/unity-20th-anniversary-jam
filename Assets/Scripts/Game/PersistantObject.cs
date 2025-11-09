using UnityEngine;

public class PersistantObject : MonoBehaviour
{
    void Awake()
    {
         PersistantObject[] objs = GameObject.FindObjectsByType<PersistantObject>(FindObjectsSortMode.None);

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
