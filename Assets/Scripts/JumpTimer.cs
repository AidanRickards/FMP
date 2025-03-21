using UnityEngine;
using System.Collections;
public class JumpTimer : MonoBehaviour
{
    public CarControl cc;


    public void StartJump()
    {
        StartCoroutine("JumpTime");
    }

    IEnumerator JumpTime()
    {
        cc.doubleJump = true;
        yield return new WaitForSecondsRealtime(1.5f);
        cc.doubleJump = false;
    }
}
