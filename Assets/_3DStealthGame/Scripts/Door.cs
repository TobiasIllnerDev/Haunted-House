using UnityEngine;

public class Door : MonoBehaviour
{
    public string KeyName = "key1";

    void OnCollisionEnter(Collision other)
    {
        PlayerMovement player =
            other.gameObject.GetComponent<PlayerMovement>();


        if (player.OwnKey(KeyName))
        {
            Destroy(gameObject);
        }
    }

}