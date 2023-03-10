using UnityEngine;
using System.Collections;

public abstract class Liveable : BaseObj {
	public enum StatusType {Live, Destroyed, Build, InActive, Repair}
	public System.Action<Hitable> OnHit;
	public System.Action OnDie;
	public System.Action<StatusType> OnStatusChange;
	public System.Action OnUpgraded;
	public System.Action<object> OnRepairStart;
	public System.Action<object> OnRepairEnd;
	public System.Action<object> OnHealthChanged;
	public System.Action OnSelected;
	[Header("Status")]
	public int health;
	public int level;
	public StatusType status;
	public bool IsSelectable;
	public float[] speedPerLevel = new float[]{0,2,1.5f,1.3f};
	protected float speed {get {return speedPerLevel[level];}}
	[Header("Sprites")]
	public GameObject[] gameObjectsPerLevel;
	public GameObject[] buildGameObjectsPerLevel;
	public Sprite[] spritesPerLevel; 
	[Header("Times")]
	public float[] buildTimePerLevel = new float[]{0,0,0,0};	
	[Header("Visual")]
	public bool showHealthBar = true;
	public float healthBarYMargin = 2;
	
	protected SpriteRenderer spriteRenderer;
	protected Animator animator;
	protected bool lookRight = true;
	HealthBarController healthBar;
	string dynamicName = "Dynamic GO";

	public int[] healthPerLevel; // {get { return _healthPerLevel; } set{ _healthPerLevel = value; health = healthPerLevel[level]; if (showHealthBar) healthBar.divider = health; }}

	protected new void Awake(){
		base.Awake();
		spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();//TODO: REMOVE AFTER CLEAN OLD SPRITES
	}

	protected new void Start(){
		base.Start();	

		status = StatusType.Live;

		if (showHealthBar) {
			var hb = Instantiate(Resources.Load(configs.prefabPaths.uiHealthbar),new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y),Quaternion.identity) as GameObject;
			hb.transform.parent = transform;
			healthBar = hb.GetComponent<HealthBarController>();
			healthBar.setHealth(health);
			healthBar.divider = health;
			healthBar.yMargin = healthBarYMargin;
		}

		OnHit += (o) => {
			if (o.hits!=null && System.Array.IndexOf(o.hits, this.GetType()) > -1 ){
				decHealth(o.damage);
				spriteRenderer.color = Color.red;
				CancelInvoke("switchBackToOriginalColor");
				Invoke("switchBackToOriginalColor", .2f);
			}
		};

		build();
	}

	void switchBackToOriginalColor(){
		spriteRenderer.color = Color.white;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		var hit = other.GetComponent<Hitable>();
		if (hit && hit.enabled && OnHit != null) OnHit(hit);
	}

	public void decHealth(int by){
		health -= by;
		if (OnHealthChanged!=null) OnHealthChanged(health); 
		healthBar.setHealth(health);
		if (health <= 0){ //DIE!!!
			if (OnDie!=null) OnDie();
			Destroy(gameObject);
		}
	}

	public void build(){
		if (buildGameObjectsPerLevel != null && buildGameObjectsPerLevel.Length > level) loadDynamicGO(buildGameObjectsPerLevel[level]);
		changeStatus(StatusType.Build);
		if (showHealthBar) healthBar.gameObject.SetActive(false);
		Invoke("upgradeDone", buildTimePerLevel[level]);
	}

	public virtual void upgradeDone(){
		level++;
		changeStatus(StatusType.Live);
		if (healthPerLevel!=null && healthPerLevel.Length > level) health = healthPerLevel[level];  
		if (gameObjectsPerLevel!=null && gameObjectsPerLevel.Length > level) loadDynamicGO(gameObjectsPerLevel[level]);
		else if (spritesPerLevel != null && spritesPerLevel.Length >= level) spriteRenderer.sprite = spritesPerLevel[level];

		if (showHealthBar) {
			healthBar.gameObject.SetActive(true);
			healthBar.setHealth(health);
			healthBar.divider = health;
		}
		if (OnUpgraded!=null) OnUpgraded();
	}

	public void setInactive(){
		changeStatus(StatusType.InActive);
	}

	void changeStatus(StatusType newStatus){
		status = newStatus;
		if (OnStatusChange != null) OnStatusChange(status);
	}

	void loadDynamicGO(GameObject go){
		if (!go) return;
		if (transform.Find(dynamicName)) Destroy(transform.Find(dynamicName).gameObject);
		go = Instantiate(go, transform.position, transform.rotation) as GameObject;
		animator = go.GetComponent<Animator>();
		go.name = dynamicName;
		go.transform.parent = gameObject.transform;
		spriteRenderer = go.GetComponent<SpriteRenderer>();
	}

	protected void setValForAnimator(string str, float val){
		if (null!=animator) {
			animator = GetComponentInChildren<Animator>();
			animator.SetFloat(str, val);
		}
		else transform.Rotate (new Vector3(0,0,180));
	}

	public void Select(){
		animator.SetTrigger("Selected");
		if (OnSelected!=null) OnSelected();
	}

	protected void Flip()
	{
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		lookRight = !lookRight;
	}
}
