/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using V_AnimationSystem;
using CodeMonkey.Utils;
using CodeMonkey.MonoBehaviours;

/*
 * Player movement with Arrow keys
 * Attack with Space
 * */
public class SkillTreePlayerSword : MonoBehaviour, Enemy.IEnemyTargetable {
    
    public static SkillTreePlayerSword instance;

    public event EventHandler OnEnemyKilled;

    public Transform pfEarthshatter;
    public Transform pfEarthshatterEffect;
    public Transform pfWhirlwind;

    private Player_Base playerBase;
    private SkillTreePlayer player;
    private State state;
    private float speed = 50f;
    private Material material;
    private Color materialTintColor;
    private HealthSystem healthSystem;
    private World_Bar healthBar;

    private enum State {
        Normal,
        Attacking,
        Busy,
    }

    private void Awake() {
        instance = this;
        playerBase = gameObject.GetComponent<Player_Base>();
        player = GetComponent<SkillTreePlayer>();
        material = transform.Find("Body").GetComponent<MeshRenderer>().material;
        materialTintColor = new Color(1, 0, 0, 0);
        healthSystem = new HealthSystem(8);
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnHealthMaxChanged += HealthSystem_OnHealthMaxChanged;
        healthBar = new World_Bar(transform, new Vector3(0, 10), new Vector3(10, 1), Color.grey, Color.red, 1f, 10000, new World_Bar.Outline { color = Color.black, size = .5f });
        UpdateHealthBarMax();
        healthBar.SetSortingLayerName("Top");

        SetStateNormal();
    }

    private void HealthSystem_OnHealthMaxChanged(object sender, EventArgs e) {
        UpdateHealthBarMax();
    }

    private void UpdateHealthBarMax() {
        healthBar.SetLocalScale(new Vector3(healthSystem.GetHealthMax() * .99f, 1.3f));
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e) {
        healthBar.SetSize(healthSystem.GetHealthNormalized());
    }
    
    public void SetHealthAmountMax(int healthAmountMax) {
        healthSystem.SetHealthMax(healthAmountMax, true);
    }

    public void SetMovementSpeed(float movementSpeed) {
        speed = movementSpeed;
    }

    private void Update() {
        switch (state) {
        case State.Normal:
            HandleMovement();
            HandleAttack();
            break;
        case State.Attacking:
            HandleAttack();
            break;
        case State.Busy:
            break;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (player.CanUseEarthshatter()) {
                state = State.Busy;
                CameraFollow.Instance.SetOverrideZoom(35f);
                playerBase.GetUnitAnimation().PlayAnimForced(UnitAnim.GetUnitAnim("dSwordTwoHandedBack_ComboEarthshatter"), 1f, (UnitAnim unitAnim) => {
                    state = State.Normal;
                }, (string trigger) => {
                    Transform earthshatterTransform = Instantiate(pfEarthshatter, GetPosition(), Quaternion.identity);
                    UtilsClass.ShakeCamera(5f, .1f);
                    Destroy(earthshatterTransform.gameObject, 3f);
                    Instantiate(pfEarthshatterEffect, GetPosition(), Quaternion.identity);
                    CameraFollow.Instance.SetOverrideZoom(null);
                    KillAllEnemiesWithinRange(80f);
                }, null);
            }
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            if (player.CanUseWhirlwind()) {
                state = State.Busy;
                CameraFollow.Instance.SetOverrideZoom(35f);
                playerBase.GetUnitAnimation().PlayAnimForced(UnitAnim.GetUnitAnim("dSwordTwoHandedBack_ComboWhirlwind"), 1f, (UnitAnim unitAnim) => {
                    state = State.Normal;
                }, (string trigger) => {
                    UtilsClass.ShakeCamera(1f, .1f);
                    if (trigger == "COMBO_WHIRLWIND_DOWN") {
                        Transform whirlwind = Instantiate(pfWhirlwind, GetPosition(), Quaternion.identity);
                        Destroy(whirlwind.gameObject, .3f);
                    }
                    if (trigger == "COMBO_WHIRLWIND_RIGHT") {
                        CameraFollow.Instance.SetOverrideZoom(null);
                        KillAllEnemiesWithinRange(80f);
                    }
                }, null);
            }
        }
    }
    
    private void SetStateNormal() {
        state = State.Normal;
    }

    private void SetStateAttacking() {
        state = State.Attacking;
    }

    private void HandleMovement() {
        float moveX = 0f;
        float moveY = 0f;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            moveX = +1f;
        }

        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle) {
            playerBase.PlayIdleAnim();
        } else {
            playerBase.PlayMoveAnim(moveDir);
            transform.position += moveDir * speed * Time.deltaTime;
        }
    }

    private void HandleAttack() {
        if (Input.GetMouseButtonDown(0) && !UtilsClass.IsPointerOverUI()) {
            // Attack
            SetStateAttacking();
            
            Vector3 attackDir = (UtilsClass.GetMouseWorldPosition() - GetPosition()).normalized;

            Enemy enemyHandler = Enemy.GetClosestEnemy(GetPosition() + attackDir * 4f, 20f);
            if (enemyHandler != null) {
                enemyHandler.Damage(this);
                if (enemyHandler.IsDead()) {
                    OnEnemyKilled?.Invoke(this, EventArgs.Empty);
                }
                attackDir = (enemyHandler.GetPosition() - GetPosition()).normalized;
                transform.position = enemyHandler.GetPosition() + attackDir * -12f;
            } else {
                transform.position = transform.position + attackDir * 4f;
            }

            Transform swordSlashTransform = Instantiate(GameAssets.i.pfSwordSlash, GetPosition() + attackDir * 13f, Quaternion.Euler(0, 0, UtilsClass.GetAngleFromVector(attackDir)));
            swordSlashTransform.GetComponent<SpriteAnimator>().onLoop = () => Destroy(swordSlashTransform.gameObject);

            UnitAnimType activeAnimType = playerBase.GetUnitAnimation().GetActiveAnimType();
            if (activeAnimType == GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Sword) {
                swordSlashTransform.localScale = new Vector3(swordSlashTransform.localScale.x, swordSlashTransform.localScale.y * -1, swordSlashTransform.localScale.z);
                playerBase.GetUnitAnimation().PlayAnimForced(GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Sword2, attackDir, 1f, (UnitAnim unitAnim) => SetStateNormal(), null, null);
            } else {
                playerBase.GetUnitAnimation().PlayAnimForced(GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Sword, attackDir, 1f, (UnitAnim unitAnim) => SetStateNormal(), null, null);
            }
        }
    }

    private void KillAllEnemiesWithinRange(float range) {
        List<Enemy> enemyList = Enemy.GetAllEnemiesWithinRange(GetPosition(), range);
        foreach (Enemy enemy in enemyList) {
            enemy.Kill(this);
            OnEnemyKilled?.Invoke(this, EventArgs.Empty);
        }
    }

    private void DamageFlash() {
        materialTintColor = new Color(1, 0, 0, 1f);
        material.SetColor("_Tint", materialTintColor);
    }

    public void DamageKnockback(Vector3 knockbackDir, float knockbackDistance) {
        transform.position += knockbackDir * knockbackDistance;
        DamageFlash();
    }
    public Vector3 GetPosition() {
        return transform.position;
    }

    public void Damage(Enemy attacker) {
        //DamageFlash();
        Vector3 bloodDir = (GetPosition() - attacker.GetPosition()).normalized;
        Blood_Handler.SpawnBlood(GetPosition(), bloodDir);
    }

}
