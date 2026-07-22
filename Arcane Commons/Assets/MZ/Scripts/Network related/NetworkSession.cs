using UnityEngine;

public class NetworkSession : MonoBehaviour
{
    public static NetworkSession Instance;

    public string hostIP = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}