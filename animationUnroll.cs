using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTJ.Alembic;

public class animationUnroll : MonoBehaviour {

    public float dist = 0;
    public bool unrollActivated = false;
    public bool columnActivated = false;

    public GameObject columnCollider;
    public GameObject unrollCollider;
    public GameObject sandStorm;
    public GameObject lightning;

    public Animation cableAnimation;
    public Animation columnAnimation;

    public float unrollTimer = 0.0f;

    public GameObject Player;
    public float animTime;

    // Use this for initialization
    void Start () {

        columnActivated = false;
        unrollActivated = false;
        sandStorm.SetActive(false);
        lightning.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        float unrollDist = Vector3.Distance(Player.transform.position, unrollCollider.transform.position);
        float columnDist = Vector3.Distance(Player.transform.position, columnCollider.transform.position);

        // Activates unroll animation if within range

        if (unrollDist < dist)
        {
            unrollActivated = true;

            if (unrollTimer < 10.00f)

            {
                unrollTimer += Time.deltaTime;
                GetComponent<AlembicStreamPlayer>().m_PlaybackSettings.m_Time = unrollTimer;
            }                

        }
        
        // Plays unroll animation backwards if area is left

        if (unrollDist > dist && unrollActivated == true)
        {
            if (unrollTimer > 0)
            {
                unrollTimer -= Time.deltaTime;
                GetComponent<AlembicStreamPlayer>().m_PlaybackSettings.m_Time = unrollTimer;
            }
        }

        // Resets settings if animation returned to Origin

        if (unrollTimer <= 0 && unrollDist > dist)
        {
            unrollActivated = false;
        }

        // Activates column animation if within range *can only be activated once*

        if (columnDist < dist)
        {
            sandStorm.SetActive(true);

            if (columnActivated == false)  // Sets alembic to beginning point
            {
                columnActivated = true;
                GetComponent<AlembicStreamPlayer>().m_PlaybackSettings.m_Time = 20.00f;
                unrollTimer = 20.00f;
            }
        }

        if (columnActivated == true)
        {
            if (unrollTimer <= 30.00f && animTime < 4.00f)  //  Runs Alembic to 30
            {
                unrollTimer += Time.deltaTime*2;
                GetComponent<AlembicStreamPlayer>().m_PlaybackSettings.m_Time = unrollTimer;
            }

            if (unrollTimer >= 30.00f && animTime < 4.00f)  //  Runs column animation, sets timer
            {
                cableAnimation["Take 001"].speed = 2.00f;
                columnAnimation["Take 001"].speed = 2.00f;
                cableAnimation.Play();
                columnAnimation.Play();

                animTime += Time.deltaTime;
            }

            if (animTime >= 2.70f)
            {
                columnAnimation["Take 001"].speed = 0.00f;
                columnAnimation["Take 001"].time = 2.70f;
            }

            if (animTime >= 3.50f)
            {
                lightning.SetActive(true);
            }

            if (animTime >= 4.00f) {

                lightning.SetActive(false);

                if (unrollTimer >= 20.00f)  // Runs alembic backward after animation is done
                {
                    unrollTimer -= Time.deltaTime;
                    GetComponent<AlembicStreamPlayer>().m_PlaybackSettings.m_Time = unrollTimer;
                }

                if (unrollTimer <= 20.00f)  // Stops sandstorm & resets alembic
                {                    
                    GetComponent<AlembicStreamPlayer>().m_PlaybackSettings.m_Time = 0.00f;
                    unrollTimer = 0.00f;
                }
            }
        }        
    }
}
