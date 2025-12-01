using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wood : MonoBehaviour
{
    void Start()
    {
        if (PlayerManager.Instance.ropeBreak)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}
