using UnityEngine;

// Token: 0x02000015 RID: 21
namespace HomingSystem
{
	public class Missile : MonoBehaviour
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000023A5 File Offset: 0x000005A5
		public bool Active
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000023AD File Offset: 0x000005AD
		public Vector2 Velocity
		{
			get
			{
				return this.rb2d.velocity;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000023BA File Offset: 0x000005BA
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000023C2 File Offset: 0x000005C2
		public bool WasVisible { get; set; }

		// Token: 0x0600003B RID: 59 RVA: 0x0000DA54 File Offset: 0x0000BC54
		private void Awake()
		{
			this.rb2d = base.GetComponent<Rigidbody2D>();
			this.col2d = base.GetComponent<Collider2D>();
			this.engine = base.GetComponent<MissileEngine>();
		//	this.jetSound = base.GetComponent<MissileJetSound>();
			this.baseSmokeEmissionRate = this.smoke.emission.rateOverTime.constant;
			
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public void SetAngle(float angle)
		{
			angle += UnityEngine.Random.Range(this.angleDeviation.min, this.angleDeviation.max) * (float)((UnityEngine.Random.Range(0f, 1f) >= 0.5f) ? -1 : 1) * 0.017453292f;
			this.engine.SetAngle(angle);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000DB14 File Offset: 0x0000BD14
		public void Reset(Transform transform)
		{
			this.explotion.gameObject.SetActive(false);
			this.engine.Reset();
			this.target = transform;
			this.render.color = Color.white;
			this.WasVisible = false;
			this.active = true;
			this.exploded = false;
			this.col2d.enabled = true;
			ParticleSystem.EmissionModule emission = this.smoke.emission;
			ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
			rateOverTime.constant = this.baseSmokeEmissionRate * GameParamsProvider.instance.Params.smokeEmissionMultiplier;
			emission.rateOverTime = rateOverTime;
			this.smoke.Play();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000023CB File Offset: 0x000005CB
		public void Remove()
		{
			this.active = false;
			this.engine.Remove();
			base.CancelInvoke("Free");
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000023F6 File Offset: 0x000005F6
		private void OnDisable()
		{
			if (this.active)
			{
	//			GameController.instance.MissileDestroyed(this);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		public void OnTriggerEnter2D(Collider2D other)
		{
			this.Disable();
			int num = 0;
			RaycastHit2D[] array = Physics2D.CircleCastAll(this.rb2d.position, this.explotionRadius, Vector2.zero);
			foreach (RaycastHit2D raycastHit2D in array)
			{
				if (raycastHit2D.collider.CompareTag("Missile") && raycastHit2D.collider != other)
				{
					Missile component = raycastHit2D.collider.GetComponent<Missile>();
					component.Exploded();
					component.OnTriggerEnter2D(this.col2d);
					num++;
				}
			}
			this.explotion.transform.position = base.transform.position;
			this.explotion.gameObject.SetActive(true);
			this.explotion.Play();
			if (!this.exploded)
			{
				if (other.CompareTag("Missile"))
				{
					other.GetComponent<Missile>().Exploded();
					num++;
				}
				this.exploded = true;
	//			SoundController.instance.PlayMissileExplotion(this.jetSound.Distance);
			}
			if (num > 0)
			{
	//			GameController.instance.AdditionalMissilesDestroyed(num);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000240E File Offset: 0x0000060E
		public void DisableCollision()
		{
			this.col2d.enabled = false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		public void Disable()
		{
			this.active = false;
			this.engine.Stop();
			this.col2d.enabled = false;
			this.render.color = Color.clear;
			base.Invoke("Free", this.freeDelay);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000213B File Offset: 0x0000033B
		private void Free()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000241C File Offset: 0x0000061C
		internal void Exploded()
		{
			this.exploded = true;
		}

		// Token: 0x0400005A RID: 90
		public float freeDelay = 10f;

		// Token: 0x0400005B RID: 91
		public Transform target;

		// Token: 0x0400005C RID: 92
		public SpriteRenderer render;

		// Token: 0x0400005D RID: 93
		public ParticleSystem smoke;

		// Token: 0x0400005E RID: 94
		public ParticleSystem explotion;

		// Token: 0x0400005F RID: 95
		public float explotionRadius = 2f;

		// Token: 0x04000060 RID: 96
		public FloatRange angleDeviation = new FloatRange(0f, 0f);

		// Token: 0x04000061 RID: 97
		public string missileName = "Missile";

		// Token: 0x04000063 RID: 99
		private Rigidbody2D rb2d;

		// Token: 0x04000064 RID: 100
		private Collider2D col2d;

		// Token: 0x04000065 RID: 101
		private MissileEngine engine;

		// Token: 0x04000066 RID: 102
	//	private MissileJetSound jetSound;

		// Token: 0x04000067 RID: 103
		private bool active = true;

		// Token: 0x04000068 RID: 104
		private bool exploded;

		// Token: 0x04000069 RID: 105
		private float baseSmokeEmissionRate;
	}
}
