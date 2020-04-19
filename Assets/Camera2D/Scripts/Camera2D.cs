using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class Camera2D : MonoBehaviour {

	[SerializeField] private float smooth = 2.5f;
	public Vector3 min, max;
	public Vector2 offset;
	private Camera cam;
	private Transform player;
	private static Camera2D _internal;
	private SpriteRenderer bounds;

	void Awake()
	{
		_internal = this;
		cam = GetComponent<Camera>();
		FindPlayer_internal();
		Follow();
	}

	public static void FindPlayer()
	{
		_internal.FindPlayer_internal();
	}



	void FindPlayer_internal()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		//if(player != null) transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
	}

	void Follow()
	{
		Vector3 position = player.position;
		position.z = transform.position.z;
		transform.position = Vector3.Lerp(transform.position, new Vector3(position.x + offset.x, position.y +offset.y, position.z), smooth * Time.deltaTime);
	}

	void LateUpdate()
	{
		if(player == null)
		{
			return;
		}
		
		Follow();
	}

	
}
