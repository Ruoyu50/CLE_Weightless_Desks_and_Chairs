using UnityEngine;

public class BackToOrigin : MonoBehaviour
{
    public Transform boxOrigin;  // 空物体（目标位置和角度）
    private Rigidbody rb;

    private bool isReturning = false;  // 当前是否处于回归状态

    private float rotationSpeed = 30f;  // 每秒10度
    private float positionSpeed = 3f;  // 每秒3米

    private const float stopThreshold = 0.01f;  // 判断停止的速度阈值
    private const float angleThreshold = 0.1f;  // 旋转误差阈值

    private bool isStopped = false;  // 标记物体是否停止

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isReturning)
        {
            ReturnToOrigin();
        }
        else
        {
            CheckForStop();
        }
    }

    // 监测是否完全停止
    void CheckForStop()
    {
        if (rb.linearVelocity.magnitude < stopThreshold && rb.angularVelocity.magnitude < stopThreshold)
        {
            if (!isStopped)
            {
                isStopped = true;
                StartReturnToOrigin();
            }
        }
        else
        {
            isStopped = false;
        }
    }

    // 开始回归
    void StartReturnToOrigin()
    {
        isReturning = true;
        rb.isKinematic = true;  // 暂时关闭物理模拟
    }

    // 执行回归
    void ReturnToOrigin()
    {
        // 位置回归（匀速移动）
        Vector3 direction = (boxOrigin.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, boxOrigin.position);

        if (distance > 0.01f)  // 位置未到
        {
            transform.position = Vector3.MoveTowards(transform.position, boxOrigin.position, positionSpeed * Time.deltaTime);
        }

        // 旋转回正（匀速旋转）
        Quaternion targetRotation = boxOrigin.rotation;
        if (Quaternion.Angle(transform.rotation, targetRotation) > angleThreshold)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 判断是否完全回归
        if (distance <= 0.01f && Quaternion.Angle(transform.rotation, targetRotation) <= angleThreshold)
        {
            FinishReturnToOrigin();
        }
    }

    // 回归完成
    void FinishReturnToOrigin()
    {
        isReturning = false;
        rb.isKinematic = false;  // 恢复物理模拟
    }
}
