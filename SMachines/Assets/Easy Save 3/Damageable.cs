using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable {

        public float health;
        public Animator Anim;
        AudioSource audio;
        public AudioClip soundFX;
        public float invulnerabilityTimeRemaining;
        ParticleSystem particleFX;
        Light lightFX;
    
    

        public virtual void TakeDamage(int amt)
        {
            health -= amt;
            if (health <= 0)
            {
                Death();
            }
        }

        public virtual void Hit(int amt)
        {
                audio.PlayOneShot(soundFX, 0.7F);
                Anim.SetTrigger("TakeDamage");
        }

        public abstract void Death();
    
}
