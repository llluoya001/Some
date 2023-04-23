using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlay : MonoBehaviour
{
    private Animator animator;
    float n;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Removal();
        PlayerContorl.TLplaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        n += Time.deltaTime;
        if (n > 8)
        {
            Destroy(this.gameObject);
            PlayerContorl.TLplaying = false;
        }

    }
    private void Removal()
    {
        animator.SetFloat("N", -1.0f);
        
    }
}
