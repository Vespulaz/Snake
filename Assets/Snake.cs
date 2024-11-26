using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private Vector2 _previousDirection = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;

    // Thêm particle effect cho death animation (tùy chọn)
    [SerializeField] private GameObject deathEffectPrefab;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        // Chỉ cho phép điều khiển khi game đang chạy và time scale > 0
        if (GameManager.Instance != null && !GameManager.Instance.isGameOver && Time.timeScale > 0)
        {
            Vector2 newDirection = _direction;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                newDirection = Vector2.up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                newDirection = Vector2.down;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                newDirection = Vector2.left;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                newDirection = Vector2.right;

            if (!IsOppositeDirection(newDirection))
            {
                _previousDirection = _direction;
                _direction = newDirection;
            }
        }
    }

    private bool IsOppositeDirection(Vector2 newDirection)
    {
        return (_direction == Vector2.up && newDirection == Vector2.down) ||
               (_direction == Vector2.down && newDirection == Vector2.up) ||
               (_direction == Vector2.left && newDirection == Vector2.right) ||
               (_direction == Vector2.right && newDirection == Vector2.left);
    }

    private void FixedUpdate()
    {
        // Chỉ di chuyển khi game đang chạy và time scale > 0
        if (GameManager.Instance != null && !GameManager.Instance.isGameOver && Time.timeScale > 0)
        {
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }
            
            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
            );
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    public void ResetState()
    {
        // Xóa tất cả các segments cũ trừ head
        for (int i = 1; i < _segments.Count; i++)
        {
            if (_segments[i] != null)
            {
                Destroy(_segments[i].gameObject);
            }
        }
        
        _segments.Clear();
        _segments.Add(this.transform);
        
        // Reset vị trí và hướng
        this.transform.position = Vector3.zero;
        _direction = Vector2.right;
        _previousDirection = Vector2.right;
        
        // Tạo segments mới
        for (int i = 1; i < this.initialSize; i++)
        {
            Transform segment = Instantiate(this.segmentPrefab);
            segment.position = transform.position - new Vector3(i, 0, 0); // Đặt segments theo hàng ngang
            _segments.Add(segment);
        }

        // Kích hoạt lại component
        this.enabled = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver) return;

        if (other.CompareTag("Food"))
        {
            Grow();
            GameManager.Instance.AddScore(1);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void Die()
    {
        // Gọi GameOver
        if (GameManager.Instance != null && !GameManager.Instance.isGameOver)
        {
            GameManager.Instance.GameOver();
        }
    }

    // Thêm method để vô hiệu hóa snake khi game over
    public void DisableSnake()
    {
        // Có thể thêm animation hoặc effect khi snake chết
        this.enabled = false;
    }

    // Thêm method để kích hoạt lại snake khi restart
    public void EnableSnake()
    {
        this.enabled = true;
        ResetState();
    }
}
