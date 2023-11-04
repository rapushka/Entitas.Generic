#if UNITY_EDITOR
using UnityEngine;

namespace Entitas.Generic
{
	public class BehavioursCollector : MonoBehaviour
	{
		[SerializeField] private EntityBehaviour[] _behaviours;

		public EntityBehaviour[] Behaviours => _behaviours;
	}
}
#endif