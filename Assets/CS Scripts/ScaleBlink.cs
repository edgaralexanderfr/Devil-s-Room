using UnityEngine;
using System.Collections;

public class ScaleBlink : MonoBehaviour {
	public float MinX        = 0.0f;
	public float MaxX        = 1.0f;
	public float SpeedX      = 1.0f;
	public float MinY        = 0.0f;
	public float MaxY        = 1.0f;
	public float SpeedY      = 1.0f;
	public float MinZ        = 0.0f;
	public float MaxZ        = 1.0f;
	public float SpeedZ      = 1.0f;

	private sbyte xDirection = -1;
	private sbyte yDirection = -1;
	private sbyte zDirection = -1;

	void Start () {
		this.MinX = Mathf.Min(this.MinX, this.MaxX);
		this.MaxX = Mathf.Max(this.MinX, this.MaxX);
		this.MinY = Mathf.Min(this.MinY, this.MaxY);
		this.MaxY = Mathf.Max(this.MinY, this.MaxY);
		this.MinZ = Mathf.Min(this.MinZ, this.MaxZ);
		this.MaxZ = Mathf.Max(this.MinZ, this.MaxZ);
	}
	
	void Update () {
		var localScale = this.transform.localScale;

		if (this.SpeedX != 0.0f) {
			localScale.x += (this.SpeedX * this.xDirection * Time.deltaTime);

			if (localScale.x < this.MinX) {
				localScale.x    = this.MinX;
				this.xDirection = 1;
			} else 
			if (localScale.x > this.MaxX) {
				localScale.x    = this.MaxX;
				this.xDirection = -1;
			}
		}

		if (this.SpeedY != 0.0f) {
			localScale.y += (this.SpeedY * this.yDirection * Time.deltaTime);

			if (localScale.y < this.MinY) {
				localScale.y    = this.MinY;
				this.yDirection = 1;
			} else 
			if (localScale.y > this.MaxY) {
				localScale.y    = this.MaxY;
				this.yDirection = -1;
			}
		}

		if (this.SpeedZ != 0.0f) {
			localScale.z += (this.SpeedZ * this.zDirection * Time.deltaTime);

			if (localScale.z < this.MinZ) {
				localScale.z    = this.MinZ;
				this.zDirection = 1;
			} else 
			if (localScale.z > this.MaxZ) {
				localScale.z    = this.MaxZ;
				this.zDirection = -1;
			}
		}

		this.transform.localScale = localScale;
	}
}
