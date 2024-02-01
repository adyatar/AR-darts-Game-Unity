using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class collision2 : MonoBehaviour
{
    public Button returne;
    public Button retryy;
    public static int trh=0;
   static private int cpt = 0;
    public Transform newParent;

    public GameObject center;
    public GameObject right;
    public GameObject left;
    public GameObject top;
    public GameObject buttom;
    private float distance;

    public Text textScore;
    public Text textBestScr;
    public Text point;
   static private int scr=0;
    static private int bestScr = 0;
    public AudioSource collisionSound;
    public Image[] flechImages=new Image[6];

    private void Start()
    {
        Debug.Log("right  :" + Vector3.Distance(center.transform.position, right.transform.position));
        Debug.Log("left  :" + Vector3.Distance(center.transform.position, left.transform.position));
        Debug.Log("top  :" + Vector3.Distance(center.transform.position, top.transform.position));
        Debug.Log("buttom  :" + Vector3.Distance(center.transform.position, buttom.transform.position));
      
        textScore.text =  scr.ToString();
        textBestScr.text = bestScr.ToString();
    }
    //0.78--0.58--0.37--0.05
    private void OnCollisionEnter(Collision collision)
    {
      
        
        Debug.Log("-/-/-" + Vector3.Distance(transform.GetChild(0).position, center.transform.position));
        distance = Vector3.Distance(transform.GetChild(0).position, center.transform.position);
        if (distance<=0.78)
        {
            flechImages[cpt].gameObject.SetActive(false);
            collisionSound.Play();
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(newParent); 
            cpt++;
            if(distance<=0.78 && distance>0.58)
            {
                scr = scr + 1;
                point.text = "+1";
                point.gameObject.SetActive(true);
                Invoke("disable", 0.5f);
            }
            else if(distance <= 0.58 && distance > 0.37)
            {
                scr = scr + 2;
                point.text = "+2";
                point.gameObject.SetActive(true);
                Invoke("disable", 0.5f);
            }
            else if(distance<= 0.37 && distance>0.085)
            {
                scr = scr + 3;
                point.text = "+3";
                point.gameObject.SetActive(true);
                Invoke("disable", 0.5f);
            }
            else if(distance<=0.085)
            {
                scr = scr + 4;
                point.text = "+4";
                point.gameObject.SetActive(true);
                Invoke("disable", 0.5f);
            }

            //scr++;
            textScore.text =  scr.ToString();
        }
      
        if (cpt == 6)
        {
            if (bestScr < scr)
                bestScr = scr;

            scr = 0;
            textScore.text =  scr.ToString();
            textBestScr.text = bestScr.ToString();
            Invoke("clear",  1f);
            Invoke("showImg", 1f);
            returne.gameObject.SetActive(true);
            retryy.gameObject.SetActive(true);
            trh = 1;
        }
    }
    private void showImg()
    {
        for(int i=0;i<=5;i++)
        {
            flechImages[i].gameObject.SetActive(true);
        }
    }
    private void disable()
    {
        point.gameObject.SetActive(false);
    }
    private void clear()
    {
        Debug.Log("clear//////////////////////////");
        for (int i = newParent.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = newParent.transform.GetChild(i);
            // Destroy the child object
            if(child.gameObject.tag!="Player")
            Destroy(child.gameObject);
        }
        DestroyObjectsWithName();
        cpt = 0;
    }
    public void DestroyObjectsWithName()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "flech(Clone)")
            {
                Destroy(obj);
            }
        }
    }
    public void goanimation()
    {
        SceneManager.LoadScene("throw");
        trh = 0;
    }
    public void retry()
    {
        returne.gameObject.SetActive(false);
        retryy.gameObject.SetActive(false);
        Invoke("trhac", 0.05f);

    }
    public void trhac()
    {
        trh = 0;
    }
}
