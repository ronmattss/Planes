using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
namespace HomingSystem
{
	[Serializable]
	public struct FloatRange
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x000050AB File Offset: 0x000032AB
		public FloatRange(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000050BB File Offset: 0x000032BB
		public float Spread()
		{
			return Mathf.Abs(this.max - this.min);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000050CF File Offset: 0x000032CF
		public static FloatRange operator *(FloatRange left, float right)
		{
			return new FloatRange(left.min * right, left.max * right);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000050E8 File Offset: 0x000032E8
		public static FloatRange operator +(FloatRange left, float right)
		{
			return new FloatRange(left.min + right, left.max + right);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00005101 File Offset: 0x00003301
		public static FloatRange operator /(FloatRange left, float right)
		{
			return new FloatRange(left.min / right, left.max / right);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000511A File Offset: 0x0000331A
		public static FloatRange operator -(FloatRange left, float right)
		{
			return new FloatRange(left.min - right, left.max - right);
		}

		// Token: 0x04000354 RID: 852
		public float min;

		// Token: 0x04000355 RID: 853
		public float max;
	}
}
