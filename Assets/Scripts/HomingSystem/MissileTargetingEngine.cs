using UnityEngine;

// Token: 0x0200001A RID: 26
namespace HomingSystem
{
	public class MissileTargetingEngine : MissileEngine
	{
		// Token: 0x06000064 RID: 100 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		protected override void FixedUpdate()
		{
			if (!this.missile.Active)
			{
				return;
			}
			if (!this.active)
			{
				return;
			}
			base.FixedUpdate();
			var target = GameObject.Find("TestPlane").transform;
			this.angularSpeed += -this.angularSpeed * this.angularDrag * this.paramsProvider.Params.speedMultiplier * Time.fixedDeltaTime;
			Vector2 to = new Vector2(target.position.x - this.rb2d.position.x, target.position.y - this.rb2d.position.y);
		//	Debug.Log(to);
			//Debug.Log(Vector2.Angle(this.rb2d.velocity, to));
			
			if (Vector2.Angle(this.rb2d.velocity, to) > 360)
			{
				float num = Mathf.Atan2(this.rb2d.velocity.y, this.rb2d.velocity.x);
				float num2 = Mathf.Atan2(to.y, to.x);
				float num3 = num2 - num;
				if (num3 > 3.1415927f)
				{
					num3 -= 6.2831855f;
				}
				else if (num3 < -3.1415927f)
				{
					num3 = 6.2831855f + num3;
				}
				this.angularSpeed += Mathf.Sign(num3) * this.angularAcceleration * this.paramsProvider.Params.speedMultiplier * Time.fixedDeltaTime;
				if (this.angularSpeed > this.angularMaxSpeed)
				{
					this.angularSpeed = this.angularMaxSpeed;
				}
				else if (this.angularSpeed < -this.angularMaxSpeed)
				{
					this.angularSpeed = -this.angularMaxSpeed;
				}
				float angle = this.rotation + this.angularSpeed * this.paramsProvider.Params.speedMultiplier * Time.fixedDeltaTime;
				float num4 = num3 * this.turnAngleMultiplier;
				if (num4 < this.minTurnAngle && num4 > -this.minTurnAngle)
				{
					num4 = 0f;
				}
				Debug.Log("REWeqw");
				this.SetAngle(angle, Mathf.Clamp(num4, -this.maxTurnAngle, this.maxTurnAngle));
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000255C File Offset: 0x0000075C
		public override void SetAngle(float angle)
		{
			this.SetAngle(angle, 0f);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		private void SetAngle(float angle, float turnAngle)
		{
			Debug.Log("AM I called?");
			this.rotation = angle;
			this.rb2d.rotation = (angle + turnAngle) * 57.29578f;
			this.rb2d.velocity = transform.up * speed;
			this.rb2d.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * this.speed * this.paramsProvider.Params.speedMultiplier;
		}


		public float angularMaxSpeed = 1f;
		public float angularAcceleration = 1f;
		public float angularDrag = 0.5f;
		public float turnAngleMultiplier = 0.8f;
		public float maxTurnAngle = 0.47123894f;
		public float minTurnAngle = 0.03141593f;
		private float angularSpeed= 10;
	}
}
