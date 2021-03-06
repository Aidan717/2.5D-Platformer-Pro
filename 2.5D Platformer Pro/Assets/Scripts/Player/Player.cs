using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    private CharacterController _controller;

    [SerializeField]
    private float _gravity = 1.0f;

    [SerializeField] 
    private float _jumpPower = 20f;

    [SerializeField]
    private float _doubleJumpPower = 20f;

    private float _yVelocity;

    private bool _canDoubleJump = false;

    private int _coins = 0;

    private UIManager _uiManager;

    [SerializeField]
    private int _lives = 3;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) {
            Debug.LogError("UIManager is NULL, player.cs");
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }


    #region "Player Move"
    private void MovePlayer() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _moveSpeed;

        ApplyGravity();
        ApplyJumping();

        velocity.y = _yVelocity;
        _controller.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region "Apply Jumping"
    private void ApplyJumping() {

        if(Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded) {
            _yVelocity = _jumpPower;
            _canDoubleJump = true;
        }
        if (_canDoubleJump && Input.GetKeyDown(KeyCode.Space) && !_controller.isGrounded) {
            _yVelocity += _doubleJumpPower;
            _canDoubleJump = false;
        }

    }
    #endregion

    #region "Apply Gravity"
    private void ApplyGravity() {
        if(!_controller.isGrounded) {
            _yVelocity = Mathf.Clamp((_yVelocity - _gravity), -20f, 100f);
        }
    }
    #endregion

    public void AddCoin() {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage() {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1) {
            SceneManager.LoadScene(0);
        }
    }
}
