using System.Collections;
using UnityEngine;

// Token: 0x02000016 RID: 22
namespace HomingSystem
{
	public class MissileEngine : MonoBehaviour
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002425 File Offset: 0x00000625
		protected virtual void Awake()
		{
			this.missile = base.GetComponent<Missile>();
			this.rb2d = base.GetComponent<Rigidbody2D>();
			this.paramsProvider = GameParamsProvider.instance;
			working = true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000DD90 File Offset: 0x0000BF90
		protected virtual void FixedUpdate()
		{
			this.missile.smoke.startRotation = -Mathf.Atan(this.rb2d.velocity.y / this.rb2d.velocity.x);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public virtual void Reset()
		{
			this.active = true;
			this.working = true;
			this.dropping = false;
			if (this.usesFuel)
			{
				this.currentFuel = this.fuel * this.paramsProvider.Params.fuelMultiplier;
				this.lastFuelCheckTime = Time.time;
				this.missile.render.transform.localScale = Vector3.one;
				base.StartCoroutine("CheckFuel");
				base.CancelInvoke("Disable");
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000244A File Offset: 0x0000064A
		public virtual void Remove()
		{
			base.StopCoroutine("CheckFuel");
			base.CancelInvoke("Disable");
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002462 File Offset: 0x00000662
		public virtual void Stop()
		{
			base.StopCoroutine("CheckFuel");
			base.CancelInvoke("Disable");
			this.missile.smoke.Stop();
			this.rb2d.velocity = Vector2.zero;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000DE64 File Offset: 0x0000C064
		private IEnumerator CheckFuel()
		{
			while (this.active)
			{
				this.currentFuel -= Time.time - this.lastFuelCheckTime;
				this.lastFuelCheckTime = Time.time;
				float fuelPart = this.currentFuel / this.fuel;
				if (fuelPart < 0.1f)
				{
					this.working = (fuelPart % 0.05f < 0.02f);
				}
				else if (fuelPart < 0.25f)
				{
					this.working = (fuelPart % 0.1f < 0.095f);
				}
				else
				{
					this.working = true;
				}
				this.missile.smoke.enableEmission = this.working;
				if (this.currentFuel <= 0f)
				{
					this.missile.DisableCollision();
					this.missile.smoke.Stop();
					this.missile.render.color = new Color(1f, 1f, 1f, 0.5f);
					base.StartCoroutine("DropMissile");
					this.active = false;
					this.working = false;
					this.dropping = true;
					yield return null;
				}
				yield return new WaitForSeconds(this.fuelCheckInterval);
			}
			yield return null;
			yield break;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000DE80 File Offset: 0x0000C080
		private IEnumerator DropMissile()
		{
			float oneOverDropTime = 1f / this.dropTime;
			float currentDropTime = 0f;
			while (currentDropTime < this.dropTime)
			{
				currentDropTime += Time.deltaTime;
				float scale = 1f - Mathf.Pow(currentDropTime * oneOverDropTime, 2f);
				this.missile.render.transform.localScale = new Vector3(scale, scale, 1f);
				this.rb2d.velocity = this.rb2d.velocity.normalized * this.speed * this.paramsProvider.Params.speedMultiplier * scale;
				yield return new WaitForEndOfFrame();
			}
			this.missile.Disable();
			yield return null;
			yield break;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000DE9C File Offset: 0x0000C09C
		public virtual void SetAngle(float angle)
		{
			this.rotation = angle;
			this.rb2d.rotation = angle * 57.29578f;
			this.rb2d.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * this.speed * this.paramsProvider.Params.speedMultiplier;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000249A File Offset: 0x0000069A
		public bool Active
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000024A2 File Offset: 0x000006A2
		public bool Working
		{
			get
			{
				return this.working;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000024AA File Offset: 0x000006AA
		public bool Dropping
		{
			get
			{
				return this.dropping;
			}
		}

		// Token: 0x0400006A RID: 106
		public float speed = 1f;

		// Token: 0x0400006B RID: 107
		public float fuel = 20f;

		// Token: 0x0400006C RID: 108
		public bool usesFuel = true;

		// Token: 0x0400006D RID: 109
		public float fuelCheckInterval = 0.5f;

		// Token: 0x0400006E RID: 110
		public float dropTime = 3f;

		// Token: 0x0400006F RID: 111
		protected Missile missile;

		// Token: 0x04000070 RID: 112
		protected Rigidbody2D rb2d;

		// Token: 0x04000071 RID: 113
		protected bool active = true;

		// Token: 0x04000072 RID: 114
		protected float currentFuel = 20;

		// Token: 0x04000073 RID: 115
		protected float lastFuelCheckTime;

		// Token: 0x04000074 RID: 116
		protected bool working;

		// Token: 0x04000075 RID: 117
		protected float rotation;

		// Token: 0x04000076 RID: 118
		protected bool dropping;

		// Token: 0x04000077 RID: 119
		public GameParamsProvider paramsProvider;
	}
}
