using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace RetroArsenal
{

	public class RetroPitchRandomizer : MonoBehaviour
	{
	
		public float randomPercent = 10;
	
		void Start ()
		{
        transform.GetComponent<AudioSource>().pitch *= 1 + Random.Range(-randomPercent / 100, randomPercent / 100);
		}

		private void OnBecameInvisible()
		{
			Destroy(gameObject);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Astro"))
			{
				other.gameObject.GetComponent<AstroObject>().TakeDamage(1);
				Destroy(gameObject);
			}
			else if (other.gameObject.CompareTag("Player"))
			{
				
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}