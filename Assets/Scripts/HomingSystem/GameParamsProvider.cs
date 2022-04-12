using UnityEngine;

// Token: 0x02000044 RID: 68
namespace HomingSystem
{
	public class GameParamsProvider : MonoBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00002F74 File Offset: 0x00001174
		private void Awake()
		{
			if (GameParamsProvider.instance == null)
			{
				GameParamsProvider.instance = this;
			}
			else if (GameParamsProvider.instance != this)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			this.SetGameMode(GameMode.Normal);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00002FB3 File Offset: 0x000011B3
		public void SetGameMode(GameMode gameMode)
		{
			if (gameMode == GameMode.Fast)
			{
				this.gameParams = this.fastGameParams;
			}
			else
			{
				this.gameParams = this.normalGameParams;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00002FD9 File Offset: 0x000011D9
		public GameParams Params
		{
			get
			{
				return this.gameParams;
			}
		}

		// Token: 0x04000160 RID: 352
		public static GameParamsProvider instance;

		// Token: 0x04000161 RID: 353
		public GameParams normalGameParams;

		// Token: 0x04000162 RID: 354
		public GameParams fastGameParams;

		// Token: 0x04000163 RID: 355
		public float playerBoostValue = 2f;

		// Token: 0x04000164 RID: 356
		public float playerBoostTime = 3f;

		// Token: 0x04000165 RID: 357
		private GameParams gameParams;
	}
}
