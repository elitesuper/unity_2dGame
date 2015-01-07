﻿using UnityEngine;
using System.Collections;

public class Vars {
	public struct PrefabPaths{
		[System.NonSerialized]
		public static string cell = "Prefabs/Cell";
		[System.NonSerialized]
		public static string uiHealthbar = "Prefabs/UI/HealthBar";
	}

	public struct Balance{
		public struct Player{
			public struct PlayerCanon{
				[System.NonSerialized]
				public static int[] healthPerLevel = new int[]{200,500,10000,20000};
			}
			public struct AutoCanon{
				[System.NonSerialized]
				public static int[] buildCostPerLevel = new int[]{5,10,20,40,80};
				[System.NonSerialized]
				public static int[] refundPerLevel = new int[]{5,10,20,40,80};
				[System.NonSerialized]
				public static float[] shootSpeedPerLevel = new float[]{1, .7f, .3f, .2f};
				[System.NonSerialized]
				public static float[] upgradeTimePerLevel = new float[]{2,5,10,20};
				[System.NonSerialized]
				public static int[] healthPerLevel = new int[]{12,15,110,120};
			}

			public struct MineralMiner{
				[System.NonSerialized]
				public static int[] buildCostPerLevel = new int[]{10,20,40,80};
				[System.NonSerialized]
				public static int[] refundPerLevel = new int[]{5,10,20,40,80};
				[System.NonSerialized]
				public static float[] upgradeTimePerLevel = new float[]{2,5,10,20};
				[System.NonSerialized]
				public static float[] timeToMineralPerLevel = new float[]{2, 1, .7f, .5f};
				[System.NonSerialized]
				public static int[] healthPerLevel = new int[]{2,5,10,20};
			}
			public struct Cell{
				[System.NonSerialized]
				public static int[] expendCostPerLevel = new int[]{25,30,50,1};
				[System.NonSerialized]
				public static float[] upgradeTimePerLevel = new float[]{2,5,10,20};
			}

			public struct Building{
			}
		}

		public struct Enemy{
			public struct canonDestroyer{
				public static int[] healthPerLevel = new int[]{500,800, 1200};
			}
			public struct kamikazi{
				public static int[] healthPerLevel = new int[]{40,80,100};
			}
		}
	}
}