using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject signSprite;
    private Animator m_Animator;
    private bool m_bCanPress;

    private void Awake()
    {
        m_Animator = signSprite.GetComponent<Animator>();
    }

	private void Update()
	{
        signSprite.SetActive(m_bCanPress);
	}

	private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactive"))
        {
            m_bCanPress = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        m_bCanPress = false;
    }
}
